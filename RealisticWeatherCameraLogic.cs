using HarmonyLib;
using System;
using System.Reflection;
using TaleWorlds.Engine;
using TaleWorlds.MountAndBlade;

namespace RealisticWeather
{
    [HarmonyPatch]
    public class RealisticWeatherCameraLogic
    {
        private static readonly Type _logic = AccessTools.TypeByName("SwitchFreeCameraLogic");

        private static MethodBase TargetMethod() => AccessTools.Method(_logic, "SwitchCamera");

        // Check whether RTS Camera is loaded.
        private static bool Prepare() => TargetMethod() != null;

        // Hide weather visuals in RTS Camera's free camera mode.
        public static void Postfix(object __instance, bool ___IsSpectatorCamera)
        {
            if (RealisticWeatherSettings.Instance.ShouldHideInFreeCamera)
            {
                Scene scene = ((Mission)AccessTools.Property(_logic, "Mission").GetValue(__instance)).Scene;
                (scene.GetFirstEntityWithName("rain_prefab_entity") ?? scene.GetFirstEntityWithName("snow_prefab_entity") ?? scene.GetFirstEntityWithName("dust_prefab_entity"))?.SetVisibilityExcludeParents(!___IsSpectatorCamera);
            }
        }
    }
}
