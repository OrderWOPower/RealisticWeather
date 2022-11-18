using SandBox.GameComponents;
using TaleWorlds.Engine;
using TaleWorlds.MountAndBlade;

namespace RealisticWeather.GameModels
{
    public class RealisticWeatherBattleMoraleModel
    {
        public class CustomBattleModel : CustomBattleMoraleModel
        {
            // Decrease morale in Custom Battle missions according to rain density, fog density and dust.
            public override float GetEffectiveInitialMorale(Agent agent, float baseMorale)
            {
                Scene scene = Mission.Current.Scene;
                return base.GetEffectiveInitialMorale(agent, baseMorale) * RealisticWeatherHelper.GetRainEffectOnMorale(scene.GetRainDensity()) * RealisticWeatherHelper.GetFogEffectOnMorale(scene.GetFog() * RealisticWeatherHelper.GetDustEffectOnMorale(RealisticWeatherMission.HasDust));
            }
        }

        public class SandboxModel : SandboxBattleMoraleModel
        {
            // Decrease morale in Sandbox missions according to rain density, fog density and dust.
            public override float GetEffectiveInitialMorale(Agent agent, float baseMorale)
            {
                Scene scene = Mission.Current.Scene;
                return base.GetEffectiveInitialMorale(agent, baseMorale) * RealisticWeatherHelper.GetRainEffectOnMorale(scene.GetRainDensity()) * RealisticWeatherHelper.GetFogEffectOnMorale(scene.GetFog() * RealisticWeatherHelper.GetDustEffectOnMorale(RealisticWeatherMission.HasDust));
            }
        }
    }
}
