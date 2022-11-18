using HarmonyLib;
using TaleWorlds.MountAndBlade;

namespace RealisticWeather
{
    [HarmonyPatch(typeof(Mission))]
    public class RealisticWeatherMission
    {
        public static bool HasDust { get; set; }

        [HarmonyPatch("AfterStart")]
        public static void Postfix(Mission __instance) => HasDust = __instance.Scene.GetFirstEntityWithName("dust_prefab_entity") != null;

        // Decrease projectile speed according to rain density and dust.
        [HarmonyPatch("AddMissileAux")]
        private static void Prefix(Mission __instance, ref float speed) => speed *= RealisticWeatherHelper.GetRainEffectOnProjectileSpeed(__instance.Scene.GetRainDensity() * RealisticWeatherHelper.GetDustEffectOnProjectileSpeed(HasDust));
    }
}
