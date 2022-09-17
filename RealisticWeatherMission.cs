using HarmonyLib;
using TaleWorlds.MountAndBlade;

namespace RealisticWeather
{
    [HarmonyPatch(typeof(Mission), "AddMissileAux")]
    public class RealisticWeatherMission
    {
        // Decrease projectile speed according to rain density and dust.
        private static void Prefix(Mission __instance, ref float speed) => speed *= RealisticWeatherHelper.GetRainEffectOnMovementSpeed(__instance.Scene.GetRainDensity() * RealisticWeatherHelper.GetDustEffectOnMovementSpeed(__instance.Scene.GetFirstEntityWithName("dust_prefab_entity") != null));
    }
}
