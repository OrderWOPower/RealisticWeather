using TaleWorlds.MountAndBlade;

namespace RealisticWeather
{
    public static class RealisticWeatherHelper
    {
        public static float GetRainEffectOnSpeed(float rainDensity) => 1 - (rainDensity / 4);

        public static float GetRainEffectOnMorale(float rainDensity) => 1 - rainDensity;

        public static float GetRainEffectOnPosture(float rainDensity) => 1 + rainDensity;

        public static float GetFogEffectOnShootFreq(float fogDensity) => 1 / fogDensity;

        public static float GetFogEffectOnMorale(float fogDensity) => 1 - (fogDensity / 64);

        public static float GetDustEffectOnSpeed(bool hasDust) => hasDust ? 0.75f : 1;

        public static float GetDustEffectOnShootFreq(bool hasDust) => hasDust ? 0.0078125f : 1;

        public static float GetDustEffectOnShooterError(bool hasDust) => hasDust ? 2 : 1;

        public static float GetDustEffectOnMorale(bool hasDust) => hasDust ? 0 : 1;

        public static float GetDustEffectOnPosture(bool hasDust) => hasDust ? 2 : 1;

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
