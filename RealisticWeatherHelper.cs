using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace RealisticWeather
{
    public static class RealisticWeatherHelper
    {
        public static float GetRainEffectOnMovementSpeed(float rainDensity) => 1 - (rainDensity * RealisticWeatherSettings.Instance.RainEffectOnMovementSpeedMultiplier / 4);

        public static float GetRainEffectOnProjectileSpeed(float rainDensity) => 1 - (rainDensity * RealisticWeatherSettings.Instance.RainEffectOnProjectileSpeedMultiplier / 4);

        public static float GetRainEffectOnMorale(float rainDensity) => MathF.Max(1 - (rainDensity * RealisticWeatherSettings.Instance.RainEffectOnMoraleMultiplier), 0f);

        public static float GetRainEffectOnPosture(float rainDensity) => 1 + (rainDensity * RealisticWeatherSettings.Instance.RainEffectOnPostureMultiplier);

        public static float GetFogEffectOnShootFreq(float fogDensity) => (MathF.Log(fogDensity, 2) * RealisticWeatherSettings.Instance.FogEffectOnShootFreqMultiplier) + 1;

        public static float GetFogEffectOnMorale(float fogDensity) => MathF.Max(1 - (fogDensity * RealisticWeatherSettings.Instance.FogEffectOnMoraleMultiplier / 64), 0f);

        public static float GetDustEffectOnMovementSpeed(bool hasDust) => hasDust ? 1 - (RealisticWeatherSettings.Instance.DustEffectOnMovementSpeedMultiplier / 4) : 1;

        public static float GetDustEffectOnProjectileSpeed(bool hasDust) => hasDust ? 1 - (RealisticWeatherSettings.Instance.DustEffectOProjectileSpeedMultiplier / 4) : 1;

        public static float GetDustEffectOnShootFreq(bool hasDust) => hasDust ? (MathF.Log(64, 2) * RealisticWeatherSettings.Instance.DustEffectOnShootFreqMultiplier) + 1 : 1;

        public static float GetDustEffectOnShooterError(bool hasDust) => hasDust ? 1 + RealisticWeatherSettings.Instance.DustEffectOnShooterErrorMultiplier : 1;

        public static float GetDustEffectOnMorale(bool hasDust) => hasDust ? MathF.Max(1 - RealisticWeatherSettings.Instance.DustEffectOnMoraleMultiplier, 0f) : 1;

        public static float GetDustEffectOnPosture(bool hasDust) => hasDust ? 1 + RealisticWeatherSettings.Instance.DustEffectOnPostureMultiplier : 1;

        // Decrease movement speed according to rain density and dust.
        // Decrease shoot frequency according to fog density and dust.
        // Increase shooter error according to dust.
        public static void SetWeatherEffectsOnAgent(Agent agent, AgentDrivenProperties agentDrivenProperties, float rainDensity, float fogDensity, bool hasDust)
        {
            if (!agent.IsHuman)
            {
                agentDrivenProperties.MountSpeed *= GetRainEffectOnMovementSpeed(rainDensity);
                agentDrivenProperties.MountSpeed *= GetDustEffectOnMovementSpeed(hasDust);
            }
            else
            {
                agentDrivenProperties.MaxSpeedMultiplier *= GetRainEffectOnMovementSpeed(rainDensity);
                agentDrivenProperties.MaxSpeedMultiplier *= GetDustEffectOnMovementSpeed(hasDust);
                agentDrivenProperties.AiShootFreq /= GetFogEffectOnShootFreq(fogDensity);
                agentDrivenProperties.AiShootFreq /= GetDustEffectOnShootFreq(hasDust);
                agentDrivenProperties.AiShooterError *= GetDustEffectOnShooterError(hasDust);
            }
        }

        public static bool HasTarget(out RealisticWeatherMixin mixin) => RealisticWeatherMixin.MixinWeakReference.TryGetTarget(out mixin);
    }
}
