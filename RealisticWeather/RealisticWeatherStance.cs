using TaleWorlds.MountAndBlade;

namespace RealisticWeather
{
	public class RealisticWeatherStance
	{
		// Modify stamina reduction according to rain density and dust.
		private static void Prefix(ref float value) => value *= RealisticWeatherHelper.GetRainEffectOnStamina(Mission.Current.Scene.GetRainDensity()) * RealisticWeatherHelper.GetDustEffectOnStamina(RealisticWeatherManager.Current.HasDust);
	}
}
