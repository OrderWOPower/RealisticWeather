using TaleWorlds.Engine;
using TaleWorlds.MountAndBlade;

namespace RealisticWeather
{
    public class RealisticWeatherPostureLogic
    {
        // Increase the drain rate of posture according to rain density and dust.
        private static void Postfix(ref float __result)
        {
            Scene scene = Mission.Current.Scene;
            __result *= RealisticWeatherHelper.GetRainEffectOnPosture(scene.GetRainDensity()) * RealisticWeatherHelper.GetDustEffectOnPosture(RealisticWeatherMission.HasDust);
        }
    }
}
