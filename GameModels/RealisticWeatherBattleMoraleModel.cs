using SandBox.GameComponents;
using TaleWorlds.MountAndBlade;

namespace RealisticWeather.GameModels
{
    public class RealisticWeatherBattleMoraleModel
    {
        public class CustomBattleModel : CustomBattleMoraleModel
        {
            // Decrease morale in Custom Battle missions according to rain density, fog density and dust.
            public override float GetEffectiveInitialMorale(Agent agent, float baseMorale) => base.GetEffectiveInitialMorale(agent, baseMorale) * RealisticWeatherHelper.GetRainEffectOnMorale(Mission.Current.Scene.GetRainDensity()) * RealisticWeatherHelper.GetFogEffectOnMorale(RealisticWeatherManager.Current.FogDensity) * RealisticWeatherHelper.GetDustEffectOnMorale(RealisticWeatherManager.Current.HasDust);
        }

        public class SandboxModel : SandboxBattleMoraleModel
        {
            // Decrease morale in Sandbox missions according to rain density, fog density and dust.
            public override float GetEffectiveInitialMorale(Agent agent, float baseMorale) => base.GetEffectiveInitialMorale(agent, baseMorale) * RealisticWeatherHelper.GetRainEffectOnMorale(Mission.Current.Scene.GetRainDensity()) * RealisticWeatherHelper.GetFogEffectOnMorale(RealisticWeatherManager.Current.FogDensity) * RealisticWeatherHelper.GetDustEffectOnMorale(RealisticWeatherManager.Current.HasDust);
        }
    }
}
