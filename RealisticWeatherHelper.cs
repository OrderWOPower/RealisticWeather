﻿using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace RealisticWeather
{
    public static class RealisticWeatherHelper
    {
        public static float GetRainEffectOnSpeed(float rainDensity) => 1 - (rainDensity * RealisticWeatherSettings.Instance.RainEffectOnSpeedMultiplier / 4);

        public static float GetRainEffectOnMorale(float rainDensity) => MathF.Max(1 - (rainDensity * RealisticWeatherSettings.Instance.RainEffectOnMoraleMultiplier), 0f);

        public static float GetRainEffectOnPosture(float rainDensity) => 1 + (rainDensity * RealisticWeatherSettings.Instance.RainEffectOnPostureMultiplier);

        public static float GetFogEffectOnShootFreq(float fogDensity) => 1 / (fogDensity * RealisticWeatherSettings.Instance.FogEffectOnShootFreqMultiplier);

        public static float GetFogEffectOnMorale(float fogDensity) => MathF.Max(1 - (fogDensity * RealisticWeatherSettings.Instance.FogEffectOnMoraleMultiplier / 64), 0f);

        public static float GetDustEffectOnSpeed(bool hasDust) => hasDust ? 1 - (RealisticWeatherSettings.Instance.DustEffectOnSpeedMultiplier / 4) : 1;

        public static float GetDustEffectOnShootFreq(bool hasDust) => hasDust ? 1 / (64 * RealisticWeatherSettings.Instance.DustEffectOnShootFreqMultiplier) : 1;

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
                agentDrivenProperties.MountSpeed *= GetRainEffectOnSpeed(rainDensity);
                agentDrivenProperties.MountSpeed *= GetDustEffectOnSpeed(hasDust);
            }
            else
            {
                agentDrivenProperties.MaxSpeedMultiplier *= GetRainEffectOnSpeed(rainDensity);
                agentDrivenProperties.MaxSpeedMultiplier *= GetDustEffectOnSpeed(hasDust);
                agentDrivenProperties.AiShootFreq *= GetFogEffectOnShootFreq(fogDensity);
                agentDrivenProperties.AiShootFreq *= GetDustEffectOnShootFreq(hasDust);
                agentDrivenProperties.AiShooterError *= GetDustEffectOnShooterError(hasDust);
            }
        }

        public static bool HasTarget(out RealisticWeatherMixin mixin) => RealisticWeatherMixin.MixinWeakReference.TryGetTarget(out mixin);
    }
}
