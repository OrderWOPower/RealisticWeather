using HarmonyLib;
using System.Reflection;

namespace RealisticWeather.ViewModels
{
    [HarmonyPatch]
    public class RealisticWeatherNavalSelectionGroupVM
    {
        private static MethodBase TargetMethod() => AccessTools.Method(AccessTools.TypeByName("NavalCustomBattleMapSelectionGroupVM"), "RandomizeAll");

        // Check whether War Sails is loaded.
        private static bool Prepare() => TargetMethod() != null;

        public static void Postfix()
        {
            if (RealisticWeatherNavalMixin.MixinWeakReference != null && RealisticWeatherNavalMixin.MixinWeakReference.TryGetTarget(out RealisticWeatherNavalMixin navalMixin))
            {
                // Randomize the Rain Density and Fog Density selections.
                navalMixin.RainDensitySelection.ExecuteRandomize();
                navalMixin.FogDensitySelection.ExecuteRandomize();
            }
        }
    }
}
