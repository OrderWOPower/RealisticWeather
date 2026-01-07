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

        [SettingPropertyBool("{=RealisticWeather10}Toggle Override Rain Density", Order = 0, RequireRestart = false, HintText = "{=RealisticWeather11}Override rain density. Disabled by default.", IsToggle = true)]
        [SettingPropertyGroup("{=RealisticWeather09}Override Rain Density", GroupOrder = 0)]
        public bool ShouldOverrideRainDensity { get; set; } = false;

        [SettingPropertyFloatingInteger("{=RealisticWeather07}Rain/Snow Density", 0.00f, 1.00f, "0.00", Order = 1, RequireRestart = false, HintText = "{=RealisticWeather12}Overridden rain density. Default is 0.00.")]
        [SettingPropertyGroup("{=RealisticWeather09}Override Rain Density", GroupOrder = 0)]
        public float OverriddenRainDensity { get; set; } = 0.00f;

        [SettingPropertyBool("{=RealisticWeather14}Toggle Override Fog Density", Order = 0, RequireRestart = false, HintText = "{=RealisticWeather15}Override fog density. Disabled by default.", IsToggle = true)]
        [SettingPropertyGroup("{=RealisticWeather13}Override Fog Density", GroupOrder = 1)]
        public bool ShouldOverrideFogDensity { get; set; } = false;

        [SettingPropertyInteger("{=RealisticWeather08}Fog Density", 1, 32, "0", Order = 1, RequireRestart = false, HintText = "{=RealisticWeather16}Overridden fog density. Default is 1.")]
        [SettingPropertyGroup("{=RealisticWeather13}Override Fog Density", GroupOrder = 1)]
        public int OverriddenFogDensity { get; set; } = 1;


        [SettingPropertyBool("{=RealisticWeather18}Rain", Order = 0, RequireRestart = false, HintText = "{=RealisticWeather21}Arena can have rain. Enabled by default.")]
        [SettingPropertyGroup("{=RealisticWeather17}Arena", GroupOrder = 2)]
        public bool CanArenaHaveRain { get; set; } = true;

        [SettingPropertyBool("{=RealisticWeather19}Fog", Order = 1, RequireRestart = false, HintText = "{=RealisticWeather22}Arena can have fog. Enabled by default.")]
        [SettingPropertyGroup("{=RealisticWeather17}Arena", GroupOrder = 2)]
        public bool CanArenaHaveFog { get; set; } = true;

        [SettingPropertyBool("{=RealisticWeather20}Dust Storms", Order = 2, RequireRestart = false, HintText = "{=RealisticWeather23}Arena can have dust storms. Enabled by default.")]
        [SettingPropertyGroup("{=RealisticWeather17}Arena", GroupOrder = 2)]
        public bool CanArenaHaveDust { get; set; } = true;

        [SettingPropertyFloatingInteger("{=RealisticWeather25}Rain Effect on Movement Speed", 0.0f, 2.0f, "0.0", Order = 0, RequireRestart = false, HintText = "{=RealisticWeather26}Multiplier for rain effect on movement speed. Default is 1.0.")]
        [SettingPropertyGroup("{=RealisticWeather24}Multipliers", GroupOrder = 3)]
        public float RainEffectOnMovementSpeedMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=RealisticWeather27}Rain Effect on Weapon Inaccuracy", 0.0f, 2.0f, "0.0", Order = 1, RequireRestart = false, HintText = "{=RealisticWeather28}Multiplier for rain effect on ranged weapon inaccuracy. Default is 1.0.")]
        [SettingPropertyGroup("{=RealisticWeather24}Multipliers", GroupOrder = 3)]
        public float RainEffectOnWeaponInaccuracyMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=RealisticWeather29}Rain Effect on Shooter Error", 0.0f, 2.0f, "0.0", Order = 2, RequireRestart = false, HintText = "{=RealisticWeather30}Multiplier for rain effect on AI shooter error. Default is 1.0.")]
        [SettingPropertyGroup("{=RealisticWeather24}Multipliers", GroupOrder = 3)]
        public float RainEffectOnShooterErrorMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=RealisticWeather31}Rain Effect on Morale", 0.0f, 2.0f, "0.0", Order = 3, RequireRestart = false, HintText = "{=RealisticWeather32}Multiplier for rain effect on morale. Default is 1.0.")]
        [SettingPropertyGroup("{=RealisticWeather24}Multipliers", GroupOrder = 3)]
        public float RainEffectOnMoraleMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=RealisticWeather33}Rain Effect on Posture", 0.0f, 2.0f, "0.0", Order = 4, RequireRestart = false, HintText = "{=RealisticWeather34}Multiplier for rain effect on RBM posture. Default is 1.0.")]
        [SettingPropertyGroup("{=RealisticWeather24}Multipliers", GroupOrder = 3)]
        public float RainEffectOnPostureMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=RealisticWeather35}Fog Effect on Shooter Error", 0.0f, 2.0f, "0.0", Order = 5, RequireRestart = false, HintText = "{=RealisticWeather36}Multiplier for fog effect on AI shooter error. Default is 1.0.")]
        [SettingPropertyGroup("{=RealisticWeather24}Multipliers", GroupOrder = 3)]
        public float FogEffectOnShooterErrorMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=RealisticWeather37}Fog Effect on Morale", 0.0f, 2.0f, "0.0", Order = 6, RequireRestart = false, HintText = "{=RealisticWeather38}Multiplier for fog effect on morale. Default is 1.0.")]
        [SettingPropertyGroup("{=RealisticWeather24}Multipliers", GroupOrder = 3)]
        public float FogEffectOnMoraleMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=RealisticWeather39}Dust Effect on Movement Speed", 0.0f, 2.0f, "0.0", Order = 7, RequireRestart = false, HintText = "{=RealisticWeather40}Multiplier for dust effect on movement speed. Default is 1.0.")]
        [SettingPropertyGroup("{=RealisticWeather24}Multipliers", GroupOrder = 3)]
        public float DustEffectOnMovementSpeedMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=RealisticWeather41}Dust Effect on Weapon Inaccuracy", 0.0f, 2.0f, "0.0", Order = 8, RequireRestart = false, HintText = "{=RealisticWeather42}Multiplier for dust effect on ranged weapon inaccuracy. Default is 1.0.")]
        [SettingPropertyGroup("{=RealisticWeather24}Multipliers", GroupOrder = 3)]
        public float DustEffectOnWeaponInaccuracyMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=RealisticWeather43}Dust Effect on Shooter Error", 0.0f, 2.0f, "0.0", Order = 9, RequireRestart = false, HintText = "{=RealisticWeather44}Multiplier for dust effect on AI shooter error. Default is 1.0.")]
        [SettingPropertyGroup("{=RealisticWeather24}Multipliers", GroupOrder = 3)]
        public float DustEffectOnShooterErrorMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=RealisticWeather45}Dust Effect on Morale", 0.0f, 2.0f, "0.0", Order = 10, RequireRestart = false, HintText = "{=RealisticWeather46}Multiplier for dust effect on morale. Default is 1.0.")]
        [SettingPropertyGroup("{=RealisticWeather24}Multipliers", GroupOrder = 3)]
        public float DustEffectOnMoraleMultiplier { get; set; } = 1.0f;

        [SettingPropertyFloatingInteger("{=RealisticWeather47}Dust Effect on Posture", 0.0f, 2.0f, "0.0", Order = 11, RequireRestart = false, HintText = "{=RealisticWeather48}Multiplier for dust effect on RBM posture. Default is 1.0.")]
        [SettingPropertyGroup("{=RealisticWeather24}Multipliers", GroupOrder = 3)]
        public float DustEffectOnPostureMultiplier { get; set; } = 1.0f;

        [SettingPropertyBool("{=RealisticWeather49}Hide in Free Camera Mode", Order = 0, RequireRestart = false, HintText = "{=RealisticWeather50}Hide rain, snow and dust storms in RTS Camera's free camera mode. Weather effects will still apply. Disabled by default.")]
        [SettingPropertyGroup("RTS Camera", GroupOrder = 4)]
        public bool ShouldHideInFreeCamera { get; set; } = false;
    }
}
