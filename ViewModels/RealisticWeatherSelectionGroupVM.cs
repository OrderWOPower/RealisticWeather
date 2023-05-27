using HarmonyLib;
using TaleWorlds.MountAndBlade.CustomBattle.CustomBattle;

namespace RealisticWeather.ViewModels
{
    [HarmonyPatch(typeof(MapSelectionGroupVM), "RandomizeAll")]
    public class RealisticWeatherSelectionGroupVM
    {
        public static void Postfix()
        {
            if (RealisticWeatherHelper.HasTarget(out RealisticWeatherMixin mixin))
            {
                // Randomize the Rain Density and Fog Density selections.
                mixin.RainDensitySelection.ExecuteRandomize();
                mixin.FogDensitySelection.ExecuteRandomize();
            }
        }
    }
}
