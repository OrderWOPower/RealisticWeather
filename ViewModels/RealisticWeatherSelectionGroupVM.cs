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
            MethodInfo method = AccessTools.Method(AccessTools.TypeByName("NavalCustomBattleMapSelectionGroupVM"), "RandomizeAll");

            yield return AccessTools.Method(typeof(MapSelectionGroupVM), "RandomizeAll");

            if (method != null)
            {
                yield return method;
            }
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
