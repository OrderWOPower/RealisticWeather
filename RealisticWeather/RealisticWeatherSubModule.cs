using Bannerlord.UIExtenderEx;
using HarmonyLib;
using RealisticWeather.Behaviors;
using RealisticWeather.GameModels;
using RealisticWeather.Logics;
using SandBox.View.Map;
using System;
using System.Linq;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.ComponentInterfaces;
using TaleWorlds.ScreenSystem;
using Path = System.IO.Path;

namespace RealisticWeather
{
    // This mod adds rain/snow, fog and dust storms which affect movement speed, ranged weapon accuracy, AI shooter error and morale.
    public class RealisticWeatherSubModule : MBSubModuleBase
    {
        private Harmony _harmony;
        private Type _typeofPostureLogic;

        protected override void OnSubModuleLoad()
        {
            UIExtender uiExtender = UIExtender.Create("RealisticWeather");

            uiExtender.Register(typeof(RealisticWeatherSubModule).Assembly);
            uiExtender.Enable();

            _harmony = new Harmony("mod.bannerlord.realisticweather");
            _harmony.PatchAll();

            // Check whether War Sails is loaded.
            if (Utilities.GetModulesNames().Contains("NavalDLC"))
            {
                Assembly realisticWeatherNaval = Assembly.LoadFrom(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "RealisticWeatherNaval.dll"));
                UIExtender navalUiExtender = UIExtender.Create("RealisticWeatherNaval");

                navalUiExtender.Register(realisticWeatherNaval);
                navalUiExtender.Enable();

                _harmony.PatchAll(realisticWeatherNaval);
            }
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            gameStarterObject.AddModel(new RealisticWeatherBattleMoraleModel((BattleMoraleModel)gameStarterObject.Models.Last(model => model is BattleMoraleModel)));

            if (game.GameType is Campaign)
            {
                CampaignGameStarter campaignGameStarter = (CampaignGameStarter)gameStarterObject;

                campaignGameStarter.AddModel(new RealisticWeatherPartyMoraleModel(campaignGameStarter.GetModel<PartyMoraleModel>()));
                campaignGameStarter.AddModel(new RealisticWeatherPartySpeedModel(campaignGameStarter.GetModel<PartySpeedModel>()));
                campaignGameStarter.AddModel(new RealisticWeatherMapVisibilityModel(campaignGameStarter.GetModel<MapVisibilityModel>()));
                campaignGameStarter.AddBehavior(new RealisticWeatherCampaignBehavior());
                ScreenManager.OnPushScreen += OnScreenManagerPushScreen;
            }

            _typeofPostureLogic = AccessTools.TypeByName("RBMAI.PostureLogic+CreateMeleeBlowPatch");

            // Check whether RBM is loaded.
            if (_typeofPostureLogic != null)
            {
                _harmony.Patch(AccessTools.Method(_typeofPostureLogic, "calculateDefenderPostureDamage"), postfix: new HarmonyMethod(AccessTools.Method(typeof(RealisticWeatherPostureLogic), "Postfix")));
                _harmony.Patch(AccessTools.Method(_typeofPostureLogic, "calculateAttackerPostureDamage"), postfix: new HarmonyMethod(AccessTools.Method(typeof(RealisticWeatherPostureLogic), "Postfix")));
            }
        }

        public override void OnBeforeMissionBehaviorInitialize(Mission mission) => mission.AddMissionBehavior(new RealisticWeatherMissionBehavior());

        public override void OnGameEnd(Game game)
        {
            if (_typeofPostureLogic != null)
            {
                _harmony.Unpatch(AccessTools.Method(_typeofPostureLogic, "calculateDefenderPostureDamage"), AccessTools.Method(typeof(RealisticWeatherPostureLogic), "Postfix"));
                _harmony.Unpatch(AccessTools.Method(_typeofPostureLogic, "calculateAttackerPostureDamage"), AccessTools.Method(typeof(RealisticWeatherPostureLogic), "Postfix"));
            }
        }

        public void OnScreenManagerPushScreen(ScreenBase pushedScreen)
        {
            if (pushedScreen is MapScreen mapScreen)
            {
                mapScreen.AddMapView<RealisticWeatherView>();
            }
        }
    }
}
