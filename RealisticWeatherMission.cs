using HarmonyLib;
using TaleWorlds.MountAndBlade;

namespace RealisticWeather
{
    [HarmonyPatch(typeof(Mission), "AddMissileAux")]
    public class RealisticWeatherMission
    {
        // Decrease projectile speed according to rain density and dust.
        private static void Prefix(Mission __instance, ref float speed) => speed *= RealisticWeatherHelper.GetRainEffectOnProjectileSpeed(__instance.Scene.GetRainDensity()) * RealisticWeatherHelper.GetDustEffectOnProjectileSpeed(RealisticWeatherManager.Current.HasDust);
    }
}
