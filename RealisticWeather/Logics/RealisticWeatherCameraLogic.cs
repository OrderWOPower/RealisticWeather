using HarmonyLib;
using System.Reflection;
using TaleWorlds.Engine;
using TaleWorlds.MountAndBlade;

namespace RealisticWeather.Logics
{
	[HarmonyPatch]
	public class RealisticWeatherCameraLogic
	{
		private static MethodBase TargetMethod() => AccessTools.Method(AccessTools.TypeByName("SwitchFreeCameraLogic"), "SwitchCamera");

		// Check whether RTS Camera is loaded.
		private static bool Prepare() => TargetMethod() != null;

		public static void Postfix(object __instance, bool ___IsSpectatorCamera)
		{
			if (RealisticWeatherSettings.Instance.ShouldHideInFreeCamera)
			{
				Scene scene = Mission.Current.Scene;

				// Hide weather visuals in RTS Camera's free camera mode.
				(scene.GetFirstEntityWithName("rain_light_prefab_entity") ?? scene.GetFirstEntityWithName("rain_prefab_entity") ?? scene.GetFirstEntityWithName("snow_prefab_entity") ?? scene.GetFirstEntityWithName("dust_prefab_entity"))?.SetVisibilityExcludeParents(!___IsSpectatorCamera);
			}
		}
	}
}
