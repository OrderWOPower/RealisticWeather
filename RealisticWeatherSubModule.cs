using Bannerlord.UIExtenderEx;
using HarmonyLib;
using RealisticWeather.GameModels;
using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.ComponentInterfaces;

namespace RealisticWeather
{
    // This mod adds rain/snow, fog and dust storms which affect movement speed, projectile speed, projectile accuracy and morale.
    public class RealisticWeatherSubModule : MBSubModuleBase
    {
        private Harmony _harmony;
        private Type _postureLogic;

        protected override void OnSubModuleLoad()
        {
            _harmony = new Harmony("mod.bannerlord.realisticweather");
            _harmony.PatchAll();
            UIExtender uiExtender = new UIExtender("RealisticWeather");
            uiExtender.Register(typeof(RealisticWeatherSubModule).Assembly);
            uiExtender.Enable();
        }

        // Check whether RBM is loaded.
        protected override void OnGameStart(Game game, IGameStarter gameStarter)
        {
            List<GameModel> models = gameStarter.Models.ToList();
            gameStarter.AddModel(new RealisticWeatherAgentStatCalculateModel((AgentStatCalculateModel)models.FindLast(model => model is AgentStatCalculateModel)));
            gameStarter.AddModel(new RealisticWeatherBattleMoraleModel((BattleMoraleModel)models.FindLast(model => model is BattleMoraleModel)));
            _postureLogic = AccessTools.TypeByName("RBMAI.PostureLogic+CreateMeleeBlowPatch");
            if (_postureLogic != null)
            {
                _harmony.Patch(AccessTools.Method(_postureLogic, "calculateDefenderPostureDamage"), postfix: new HarmonyMethod(AccessTools.Method(typeof(RealisticWeatherPostureLogic), "Postfix")));
                _harmony.Patch(AccessTools.Method(_postureLogic, "calculateAttackerPostureDamage"), postfix: new HarmonyMethod(AccessTools.Method(typeof(RealisticWeatherPostureLogic), "Postfix")));
            }
        }

        public override void OnGameEnd(Game game)
        {
            if (_postureLogic != null)
            {
                _harmony.Unpatch(AccessTools.Method(_postureLogic, "calculateDefenderPostureDamage"), AccessTools.Method(typeof(RealisticWeatherPostureLogic), "Postfix"));
                _harmony.Unpatch(AccessTools.Method(_postureLogic, "calculateAttackerPostureDamage"), AccessTools.Method(typeof(RealisticWeatherPostureLogic), "Postfix"));
            }
        }

        public override void OnBeforeMissionBehaviorInitialize(Mission mission) => mission.AddMissionBehavior(new RealisticWeatherMissionBehavior());
    }
}
