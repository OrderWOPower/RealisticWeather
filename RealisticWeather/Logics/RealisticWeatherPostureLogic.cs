using TaleWorlds.MountAndBlade;

namespace RealisticWeather.Logics
{
    public class RealisticWeatherPostureLogic
    {
        // Increase the drain rate of posture according to rain density and dust.
        private static void Postfix(ref float __result) => __result *= RealisticWeatherHelper.GetRainEffectOnPosture(Mission.Current.Scene.GetRainDensity()) * RealisticWeatherHelper.GetDustEffectOnPosture(RealisticWeatherManager.Current.HasDust);
    }
}
