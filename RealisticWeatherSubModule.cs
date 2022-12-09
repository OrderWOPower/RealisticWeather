using Bannerlord.UIExtenderEx;
using HarmonyLib;
using RealisticWeather.GameModels;
using System;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.CustomBattle;

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
            AgentStatCalculateModel agentStatCalculateModel = (AgentStatCalculateModel)gameStarter.Models.ToList().FindLast(model => model is AgentStatCalculateModel);
            _postureLogic = AccessTools.TypeByName("RBMAI.PostureLogic+CreateMeleeBlowPatch");
            if (_postureLogic != null)
            {
                _harmony.Patch(AccessTools.Method(_postureLogic, "calculateDefenderPostureDamage"), postfix: new HarmonyMethod(AccessTools.Method(typeof(RealisticWeatherPostureLogic), "Postfix")));
                _harmony.Patch(AccessTools.Method(_postureLogic, "calculateAttackerPostureDamage"), postfix: new HarmonyMethod(AccessTools.Method(typeof(RealisticWeatherPostureLogic), "Postfix")));
            }
            if (game.GameType is CustomGame)
            {
                gameStarter.AddModel(new RealisticWeatherAgentStatCalculateModel.RealisticWeatherCustomBattleAgentStatCalculateModel(agentStatCalculateModel));
                gameStarter.AddModel(new RealisticWeatherBattleMoraleModel.RealisticWeatherCustomBattleMoraleModel());
            }
            else if (game.GameType is Campaign)
            {
                gameStarter.AddModel(new RealisticWeatherAgentStatCalculateModel.RealisticWeatherSandboxAgentStatCalculateModel(agentStatCalculateModel));
                gameStarter.AddModel(new RealisticWeatherBattleMoraleModel.RealisticWeatherSandboxBattleMoraleModel());
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
