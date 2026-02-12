using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace RealisticWeather
{
	public static class RealisticWeatherHelper
	{
		public static float GetRainEffectOnMovementSpeed(float rainDensity) => MathF.Max(1 - (rainDensity / 4 * RealisticWeatherSettings.Instance.RainEffectOnMovementSpeedMultiplier), 0f);

		public static float GetRainEffectOnWeaponInaccuracy(float rainDensity) => 1 + (rainDensity * RealisticWeatherSettings.Instance.RainEffectOnWeaponInaccuracyMultiplier);

		public static float GetRainEffectOnShooterError(float rainDensity) => 1 + (rainDensity * RealisticWeatherSettings.Instance.RainEffectOnShooterErrorMultiplier);

		public static float GetRainEffectOnMorale(float rainDensity) => MathF.Max(1 - (rainDensity * RealisticWeatherSettings.Instance.RainEffectOnMoraleMultiplier), 0f);

		public static float GetRainEffectOnPosture(float rainDensity) => 1 + (rainDensity * RealisticWeatherSettings.Instance.RainEffectOnPostureMultiplier);

		public static float GetFogEffectOnShooterError(float fogDensity) => fogDensity > 1 ? 1 + (fogDensity / 16 * RealisticWeatherSettings.Instance.FogEffectOnShooterErrorMultiplier) : 1;

		public static float GetFogEffectOnMorale(float fogDensity) => fogDensity > 1 ? MathF.Max(1 - (fogDensity / 64 * RealisticWeatherSettings.Instance.FogEffectOnMoraleMultiplier), 0f) : 1;

		public static float GetDustEffectOnMovementSpeed(bool hasDust) => hasDust ? MathF.Max(1 - (0.25f * RealisticWeatherSettings.Instance.DustEffectOnMovementSpeedMultiplier), 0f) : 1;

		public static float GetDustEffectOnWeaponInaccuracy(bool hasDust) => hasDust ? 1 + RealisticWeatherSettings.Instance.DustEffectOnWeaponInaccuracyMultiplier : 1;

		public static float GetDustEffectOnShooterError(bool hasDust) => hasDust ? 1 + (4 * RealisticWeatherSettings.Instance.DustEffectOnShooterErrorMultiplier) : 1;

		public static float GetDustEffectOnMorale(bool hasDust) => hasDust ? MathF.Max(1 - RealisticWeatherSettings.Instance.DustEffectOnMoraleMultiplier, 0f) : 1;

		public static float GetDustEffectOnPosture(bool hasDust) => hasDust ? 1 + RealisticWeatherSettings.Instance.DustEffectOnPostureMultiplier : 1;

		public static void ApplyWeatherEffectsOnAgent(Agent agent, AgentDrivenProperties agentDrivenProperties, float rainDensity, float fogDensity, bool hasDust)
		{
			if (agent.IsHuman)
			{
				// Remove the vanilla rain effect on missile speed.
				agentDrivenProperties.MissileSpeedMultiplier = 1f;
				// Decrease movement speed according to rain density and dust.
				agentDrivenProperties.MaxSpeedMultiplier *= GetRainEffectOnMovementSpeed(rainDensity);
				agentDrivenProperties.MaxSpeedMultiplier *= GetDustEffectOnMovementSpeed(hasDust);
				// Increase weapon inaccuracy according to rain density and dust.
				agentDrivenProperties.WeaponInaccuracy *= GetRainEffectOnWeaponInaccuracy(rainDensity);
				agentDrivenProperties.WeaponInaccuracy *= GetDustEffectOnWeaponInaccuracy(hasDust);
				// Increase shooter error according to rain density, fog density and dust.
				agentDrivenProperties.AiShooterError *= MathF.Max(GetRainEffectOnShooterError(rainDensity), GetFogEffectOnShooterError(fogDensity));
				agentDrivenProperties.AiShooterError *= GetDustEffectOnShooterError(hasDust);
			}
			else
			{
				// Decrease movement speed according to rain density and dust.
				agentDrivenProperties.MountSpeed *= GetRainEffectOnMovementSpeed(rainDensity);
				agentDrivenProperties.MountSpeed *= GetDustEffectOnMovementSpeed(hasDust);
			}
		}
	}
}
