using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;

namespace RealisticWeather
{
    public class RealisticWeatherSettings : AttributeGlobalSettings<RealisticWeatherSettings>
    {
        public override string Id => "RealisticWeather";

        public override string DisplayName => "Realistic Weather";

        public override string FolderName => "RealisticWeather";

        public override string FormatType => "json2";

        [SettingPropertyFloatingInteger("Chance of Rain", 0.00f, 1.00f, "#0%", Order = 0, RequireRestart = false, HintText = "Chance of rain in forests. Default is 20%.")]
        [SettingPropertyGroup("Terrain/Forests", GroupOrder = 0)]
        public float ForestRainChance { get; set; } = 0.2f;

        [SettingPropertyFloatingInteger("Chance of Fog", 0.00f, 1.00f, "#0%", Order = 1, RequireRestart = false, HintText = "Chance of fog in forests. Default is 10%.")]
        [SettingPropertyGroup("Terrain/Forests", GroupOrder = 0)]
        public float ForestFogChance { get; set; } = 0.1f;

        [SettingPropertyFloatingInteger("Chance of Dust Storms", 0.00f, 1.00f, "#0%", Order = 2, RequireRestart = false, HintText = "Chance of dust storms in forests. Default is 0%.")]
        [SettingPropertyGroup("Terrain/Forests", GroupOrder = 0)]
        public float ForestDustChance { get; set; } = 0f;

        [SettingPropertyFloatingInteger("Chance of Rain", 0.00f, 1.00f, "#0%", Order = 0, RequireRestart = false, HintText = "Chance of rain in steppes. Default is 5%.")]
        [SettingPropertyGroup("Terrain/Steppes", GroupOrder = 1)]
        public float SteppeRainChance { get; set; } = 0.05f;

        [SettingPropertyFloatingInteger("Chance of Fog", 0.00f, 1.00f, "#0%", Order = 1, RequireRestart = false, HintText = "Chance of fog in steppes. Default is 5%.")]
        [SettingPropertyGroup("Terrain/Steppes", GroupOrder = 1)]
        public float SteppeFogChance { get; set; } = 0.05f;

        [SettingPropertyFloatingInteger("Chance of Dust Storms", 0.00f, 1.00f, "#0%", Order = 2, RequireRestart = false, HintText = "Chance of dust storms in steppes. Default is 0%.")]
        [SettingPropertyGroup("Terrain/Steppes", GroupOrder = 1)]
        public float SteppeDustChance { get; set; } = 0f;

        [SettingPropertyFloatingInteger("Chance of Rain", 0.00f, 1.00f, "#0%", Order = 0, RequireRestart = false, HintText = "Chance of rain in deserts. Default is 1%.")]
        [SettingPropertyGroup("Terrain/Deserts", GroupOrder = 2)]
        public float DesertRainChance { get; set; } = 0.01f;

        [SettingPropertyFloatingInteger("Chance of Fog", 0.00f, 1.00f, "#0%", Order = 1, RequireRestart = false, HintText = "Chance of fog in deserts. Default is 0%.")]
        [SettingPropertyGroup("Terrain/Deserts", GroupOrder = 2)]
        public float DesertFogChance { get; set; } = 0f;

        [SettingPropertyFloatingInteger("Chance of Dust Storms", 0.00f, 1.00f, "#0%", Order = 2, RequireRestart = false, HintText = "Chance of dust storms in deserts. Default is 10%.")]
        [SettingPropertyGroup("Terrain/Deserts", GroupOrder = 2)]
        public float DesertDustChance { get; set; } = 0.1f;

        [SettingPropertyFloatingInteger("Chance of Rain", 0.00f, 1.00f, "#0%", Order = 0, RequireRestart = false, HintText = "Chance of rain near rivers. Default is 10%.")]
        [SettingPropertyGroup("Terrain/Rivers", GroupOrder = 3)]
        public float RiverRainChance { get; set; } = 0.1f;

        [SettingPropertyFloatingInteger("Chance of Fog", 0.00f, 1.00f, "#0%", Order = 1, RequireRestart = false, HintText = "Chance of fog near rivers. Default is 20%.")]
        [SettingPropertyGroup("Terrain/Rivers", GroupOrder = 3)]
        public float RiverFogChance { get; set; } = 0.2f;

        [SettingPropertyFloatingInteger("Chance of Dust Storms", 0.00f, 1.00f, "#0%", Order = 2, RequireRestart = false, HintText = "Chance of dust storms near rivers. Default is 0%.")]
        [SettingPropertyGroup("Terrain/Rivers", GroupOrder = 3)]
        public float RiverDustChance { get; set; } = 0f;

        [SettingPropertyFloatingInteger("Chance of Rain", 0.00f, 1.00f, "#0%", Order = 0, RequireRestart = false, HintText = "Chance of rain in other terrain. Default is 10%.")]
        [SettingPropertyGroup("Terrain/Other", GroupOrder = 4)]
        public float OtherRainChance { get; set; } = 0.1f;

        [SettingPropertyFloatingInteger("Chance of Fog", 0.00f, 1.00f, "#0%", Order = 1, RequireRestart = false, HintText = "Chance of fog in other terrain. Default is 10%.")]
        [SettingPropertyGroup("Terrain/Other", GroupOrder = 4)]
        public float OtherFogChance { get; set; } = 0.1f;

        [SettingPropertyFloatingInteger("Chance of Dust Storms", 0.00f, 1.00f, "#0%", Order = 2, RequireRestart = false, HintText = "Chance of dust storms in other terrain. Default is 0%.")]
        [SettingPropertyGroup("Terrain/Other", GroupOrder = 4)]
        public float OtherDustChance { get; set; } = 0f;

        [SettingPropertyBool("Rain", Order = 0, RequireRestart = false, HintText = "Arena can have rain. Disabled by default.")]
        [SettingPropertyGroup("Arena", GroupOrder = 5)]
        public bool CanArenaHaveRain { get; set; } = false;

        [SettingPropertyBool("Fog", Order = 1, RequireRestart = false, HintText = "Arena can have fog. Disabled by default.")]
        [SettingPropertyGroup("Arena", GroupOrder = 5)]
        public bool CanArenaHaveFog { get; set; } = false;

        [SettingPropertyBool("Dust Storms", Order = 2, RequireRestart = false, HintText = "Arena can have dust storms. Disabled by default.")]
        [SettingPropertyGroup("Arena", GroupOrder = 5)]
        public bool CanArenaHaveDust { get; set; } = false;

        [SettingPropertyBool("Toggle Override Rain Density", Order = 0, RequireRestart = false, HintText = "Override rain density. Disabled by default.", IsToggle = true)]
        [SettingPropertyGroup("Override Rain Density", GroupOrder = 6)]
        public bool ShouldOverrideRainDensity { get; set; } = false;

        [SettingPropertyFloatingInteger("Rain Density", 0.00f, 1.00f, "0.00", Order = 1, RequireRestart = false, HintText = "Overridden rain density. Default is 0.00.")]
        [SettingPropertyGroup("Override Rain Density", GroupOrder = 6)]
        public float OverriddenRainDensity { get; set; } = 0;

        [SettingPropertyBool("Toggle Override Fog Density", Order = 0, RequireRestart = false, HintText = "Override fog density. Disabled by default.", IsToggle = true)]
        [SettingPropertyGroup("Override Fog Density", GroupOrder = 7)]
        public bool ShouldOverrideFogDensity { get; set; } = false;

        [SettingPropertyFloatingInteger("Fog Density", 1, 32, "0", Order = 1, RequireRestart = false, HintText = "Overridden fog density. Default is 1.")]
        [SettingPropertyGroup("Override Fog Density", GroupOrder = 7)]
        public float OverriddenFogDensity { get; set; } = 1;

        [SettingPropertyFloatingInteger("Rain Effect on Movement Speed", 0.0f, 2.0f, "0.0", Order = 0, RequireRestart = false, HintText = "Multiplier for rain effect on movement speed. Default is 1.0.")]
        [SettingPropertyGroup("Multipliers", GroupOrder = 8)]
        public float RainEffectOnMovementSpeedMultiplier { get; set; } = 1;

        [SettingPropertyFloatingInteger("Rain Effect on Projectile Speed", 0.0f, 2.0f, "0.0", Order = 1, RequireRestart = false, HintText = "Multiplier for rain effect on projectile speed. Default is 1.0.")]
        [SettingPropertyGroup("Multipliers", GroupOrder = 8)]
        public float RainEffectOnProjectileSpeedMultiplier { get; set; } = 1;

        [SettingPropertyFloatingInteger("Rain Effect on Morale", 0.0f, 2.0f, "0.0", Order = 2, RequireRestart = false, HintText = "Multiplier for rain effect on morale. Default is 1.0.")]
        [SettingPropertyGroup("Multipliers", GroupOrder = 8)]
        public float RainEffectOnMoraleMultiplier { get; set; } = 1;

        [SettingPropertyFloatingInteger("Rain Effect on Posture", 0.0f, 2.0f, "0.0", Order = 3, RequireRestart = false, HintText = "Multiplier for rain effect on RBM posture. Default is 1.0.")]
        [SettingPropertyGroup("Multipliers", GroupOrder = 8)]
        public float RainEffectOnPostureMultiplier { get; set; } = 1;

        [SettingPropertyFloatingInteger("Fog Effect on Shoot Frequency", 0.0f, 2.0f, "0.0", Order = 4, RequireRestart = false, HintText = "Multiplier for fog effect on AI shoot frequency. Default is 1.0.")]
        [SettingPropertyGroup("Multipliers", GroupOrder = 8)]
        public float FogEffectOnShootFreqMultiplier { get; set; } = 1;

        [SettingPropertyFloatingInteger("Fog Effect on Morale", 0.0f, 2.0f, "0.0", Order = 5, RequireRestart = false, HintText = "Multiplier for fog effect on morale. Default is 1.0.")]
        [SettingPropertyGroup("Multipliers", GroupOrder = 8)]
        public float FogEffectOnMoraleMultiplier { get; set; } = 1;

        [SettingPropertyFloatingInteger("Dust Effect on Movement Speed", 0.0f, 2.0f, "0.0", Order = 6, RequireRestart = false, HintText = "Multiplier for dust effect on movement speed. Default is 1.0.")]
        [SettingPropertyGroup("Multipliers", GroupOrder = 8)]
        public float DustEffectOnMovementSpeedMultiplier { get; set; } = 1;

        [SettingPropertyFloatingInteger("Dust Effect on Projectile Speed", 0.0f, 2.0f, "0.0", Order = 7, RequireRestart = false, HintText = "Multiplier for dust effect on projectile speed. Default is 1.0.")]
        [SettingPropertyGroup("Multipliers", GroupOrder = 8)]
        public float DustEffectOProjectileSpeedMultiplier { get; set; } = 1;

        [SettingPropertyFloatingInteger("Dust Effect on Shoot Frequency", 0.0f, 2.0f, "0.0", Order = 8, RequireRestart = false, HintText = "Multiplier for dust effect on AI shoot frequency. Default is 1.0.")]
        [SettingPropertyGroup("Multipliers", GroupOrder = 8)]
        public float DustEffectOnShootFreqMultiplier { get; set; } = 1;

        [SettingPropertyFloatingInteger("Dust Effect on Shooter Error", 0.0f, 2.0f, "0.0", Order = 9, RequireRestart = false, HintText = "Multiplier for dust effect on AI shooter error. Default is 1.0.")]
        [SettingPropertyGroup("Multipliers", GroupOrder = 8)]
        public float DustEffectOnShooterErrorMultiplier { get; set; } = 1;

        [SettingPropertyFloatingInteger("Dust Effect on Morale", 0.0f, 2.0f, "0.0", Order = 10, RequireRestart = false, HintText = "Multiplier for dust effect on morale. Default is 1.0.")]
        [SettingPropertyGroup("Multipliers", GroupOrder = 8)]
        public float DustEffectOnMoraleMultiplier { get; set; } = 1;

        [SettingPropertyFloatingInteger("Dust Effect on Posture", 0.0f, 2.0f, "0.0", Order = 11, RequireRestart = false, HintText = "Multiplier for dust effect on RBM posture. Default is 1.0.")]
        [SettingPropertyGroup("Multipliers", GroupOrder = 8)]
        public float DustEffectOnPostureMultiplier { get; set; } = 1;

        [SettingPropertyBool("Hide in Free Camera", Order = 0, RequireRestart = false, HintText = "Hide rain, snow and dust in RTS Camera's free camera mode. Weather effects will still apply. Disabled by default.")]
        [SettingPropertyGroup("RTS Camera", GroupOrder = 9)]
        public bool ShouldHideInFreeCamera { get; set; } = false;
    }
}
