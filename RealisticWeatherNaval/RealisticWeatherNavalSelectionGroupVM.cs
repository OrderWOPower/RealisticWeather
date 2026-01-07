using HarmonyLib;
using NavalDLC.CustomBattle.CustomBattle;

namespace RealisticWeatherNaval
{
    [HarmonyPatch(typeof(NavalCustomBattleMapSelectionGroupVM), "RandomizeAll")]
    public class RealisticWeatherNavalSelectionGroupVM
    {
        public static void Postfix()
        {
            if (RealisticWeatherNavalMixin.MixinWeakReference != null && RealisticWeatherNavalMixin.MixinWeakReference.TryGetTarget(out RealisticWeatherNavalMixin mixin))
            {
                // Randomize the Rain Density and Fog Density selections.
                mixin.RainDensitySelection.ExecuteRandomize();
                mixin.FogDensitySelection.ExecuteRandomize();
            }
        }
    }
}
