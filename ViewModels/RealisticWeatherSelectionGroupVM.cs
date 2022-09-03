using HarmonyLib;
using TaleWorlds.MountAndBlade.CustomBattle.CustomBattle;

namespace RealisticWeather.ViewModels
{
    [HarmonyPatch(typeof(MapSelectionGroupVM), "RandomizeAll")]
    public class RealisticWeatherSelectionGroupVM
    {
        // Randomize the Rain Density and Fog Density selections.
        public static void Postfix()
        {
            if (RealisticWeatherHelper.HasTarget(out RealisticWeatherMixin mixin))
            {
                mixin.RainDensitySelection.ExecuteRandomize();
                mixin.FogDensitySelection.ExecuteRandomize();
            }
        }
    }
}
