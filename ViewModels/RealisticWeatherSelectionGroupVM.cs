using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using TaleWorlds.MountAndBlade.CustomBattle.CustomBattle;

namespace RealisticWeather.ViewModels
{
    [HarmonyPatch]
    public class RealisticWeatherSelectionGroupVM
    {
        private static IEnumerable<MethodBase> TargetMethods()
        {
            yield return AccessTools.Method(typeof(MapSelectionGroupVM), "RandomizeAll");
            yield return AccessTools.Method(AccessTools.TypeByName("NavalCustomBattleMapSelectionGroupVM"), "RandomizeAll");
        }

        public static void Postfix()
        {
            if (RealisticWeatherMixin.MixinWeakReference != null && RealisticWeatherMixin.MixinWeakReference.TryGetTarget(out RealisticWeatherMixin mixin))
            {
                // Randomize the Rain Density and Fog Density selections.
                mixin.RainDensitySelection.ExecuteRandomize();
                mixin.FogDensitySelection.ExecuteRandomize();
            }
            else if (RealisticWeatherNavalMixin.MixinWeakReference != null && RealisticWeatherNavalMixin.MixinWeakReference.TryGetTarget(out RealisticWeatherNavalMixin navalMixin))
            {
                // Randomize the Rain Density and Fog Density selections.
                navalMixin.RainDensitySelection.ExecuteRandomize();
                navalMixin.FogDensitySelection.ExecuteRandomize();
            }
        }
    }
}
