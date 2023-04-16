using Bannerlord.UIExtenderEx;
using HarmonyLib;
using RealisticWeather.GameModels;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.ComponentInterfaces;

namespace RealisticWeather
{
    // This mod adds rain/snow, fog and dust storms which affect movement speed, projectile speed, projectile accuracy and morale.
    public class RealisticWeatherSubModule : MBSubModuleBase
    {
        public static Harmony HarmonyInstance { get; set; }

        protected override void OnSubModuleLoad()
        {
            UIExtender uiExtender = new UIExtender("RealisticWeather");
            HarmonyInstance = new Harmony("mod.bannerlord.realisticweather");
            HarmonyInstance.PatchAll();
            uiExtender.Register(typeof(RealisticWeatherSubModule).Assembly);
            uiExtender.Enable();
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarter) => gameStarter.AddModel(new RealisticWeatherBattleMoraleModel((BattleMoraleModel)gameStarter.Models.ToList().FindLast(model => model is BattleMoraleModel)));

        public override void OnBeforeMissionBehaviorInitialize(Mission mission) => mission.AddMissionBehavior(new RealisticWeatherMissionBehavior());
    }
}
