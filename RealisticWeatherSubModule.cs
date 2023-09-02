using Bannerlord.UIExtenderEx;
using HarmonyLib;
using RealisticWeather.Behaviors;
using RealisticWeather.GameModels;
using RealisticWeather.Logics;
using SandBox.View.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.ComponentInterfaces;
using TaleWorlds.ScreenSystem;

namespace RealisticWeather
{
    // This mod adds rain/snow, fog and dust storms which affect movement speed, ranged weapon accuracy, AI shooter error and morale.
    public class RealisticWeatherSubModule : MBSubModuleBase
    {
        private Harmony _harmony;
        private Type _postureLogic;

        protected override void OnSubModuleLoad()
        {
            UIExtender uiExtender = new UIExtender("RealisticWeather");

            uiExtender.Register(typeof(RealisticWeatherSubModule).Assembly);
            uiExtender.Enable();

            _harmony = new Harmony("mod.bannerlord.realisticweather");
            _harmony.PatchAll();
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            IEnumerable<GameModel> models = gameStarterObject.Models;

            gameStarterObject.AddModel(new RealisticWeatherBattleMoraleModel((BattleMoraleModel)models.Last(model => model is BattleMoraleModel)));

            if (game.GameType is Campaign)
            {
                CampaignGameStarter campaignGameStarter = (CampaignGameStarter)gameStarterObject;

                campaignGameStarter.AddModel(new RealisticWeatherPartyMoraleModel((PartyMoraleModel)models.Last(model => model is PartyMoraleModel)));
                campaignGameStarter.AddModel(new RealisticWeatherPartySpeedModel((PartySpeedModel)models.Last(model => model is PartySpeedModel)));
                campaignGameStarter.AddModel(new RealisticWeatherMapVisibilityModel((MapVisibilityModel)models.Last(model => model is MapVisibilityModel)));
                campaignGameStarter.AddBehavior(new RealisticWeatherCampaignBehavior());
                ScreenManager.OnPushScreen += OnScreenManagerPushScreen;
            }

            _postureLogic = AccessTools.TypeByName("RBMAI.PostureLogic+CreateMeleeBlowPatch");

            // Check whether RBM is loaded.
            if (_postureLogic != null)
            {
                _harmony.Patch(AccessTools.Method(_postureLogic, "calculateDefenderPostureDamage"), postfix: new HarmonyMethod(AccessTools.Method(typeof(RealisticWeatherPostureLogic), "Postfix")));
                _harmony.Patch(AccessTools.Method(_postureLogic, "calculateAttackerPostureDamage"), postfix: new HarmonyMethod(AccessTools.Method(typeof(RealisticWeatherPostureLogic), "Postfix")));
            }
        }

        public override void OnBeforeMissionBehaviorInitialize(Mission mission) => mission.AddMissionBehavior(new RealisticWeatherMissionBehavior());

        public override void OnGameEnd(Game game)
        {
            if (_postureLogic != null)
            {
                _harmony.Unpatch(AccessTools.Method(_postureLogic, "calculateDefenderPostureDamage"), AccessTools.Method(typeof(RealisticWeatherPostureLogic), "Postfix"));
                _harmony.Unpatch(AccessTools.Method(_postureLogic, "calculateAttackerPostureDamage"), AccessTools.Method(typeof(RealisticWeatherPostureLogic), "Postfix"));
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
