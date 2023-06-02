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

        [SettingPropertyFloatingInteger("{=RealisticWeather10}Chance of Rain", 0.00f, 1.00f, "#0%", Order = 0, RequireRestart = false, HintText = "{=RealisticWeather14}Chance of rain in forests. Default is 20%.")]
        [SettingPropertyGroup("{=RealisticWeather09}Terrain" + "/" + "{=RealisticWeather13}Forests", GroupOrder = 0)]
        public float ForestRainChance { get; set; } = 0.2f;

        [SettingPropertyFloatingInteger("{=RealisticWeather11}Chance of Fog", 0.00f, 1.00f, "#0%", Order = 1, RequireRestart = false, HintText = "{=RealisticWeather15}Chance of fog in forests. Default is 10%.")]
        [SettingPropertyGroup("{=RealisticWeather09}Terrain" + "/" + "{=RealisticWeather13}Forests", GroupOrder = 0)]
        public float ForestFogChance { get; set; } = 0.1f;

        [SettingPropertyFloatingInteger("{=RealisticWeather12}Chance of Dust Storms", 0.00f, 1.00f, "#0%", Order = 2, RequireRestart = false, HintText = "{=RealisticWeather16}Chance of dust storms in forests. Default is 0%.")]
        [SettingPropertyGroup("{=RealisticWeather09}Terrain" + "/" + "{=RealisticWeather13}Forests", GroupOrder = 0)]
        public float ForestDustChance { get; set; } = 0f;

        [SettingPropertyFloatingInteger("{=RealisticWeather10}Chance of Rain", 0.00f, 1.00f, "#0%", Order = 0, RequireRestart = false, HintText = "{=RealisticWeather18}Chance of rain in steppes. Default is 5%.")]
        [SettingPropertyGroup("{=RealisticWeather09}Terrain" + "/" + "{=RealisticWeather17}Steppes", GroupOrder = 1)]
        public float SteppeRainChance { get; set; } = 0.05f;

        [SettingPropertyFloatingInteger("{=RealisticWeather11}Chance of Fog", 0.00f, 1.00f, "#0%", Order = 1, RequireRestart = false, HintText = "{=RealisticWeather19}Chance of fog in steppes. Default is 5%.")]
        [SettingPropertyGroup("{=RealisticWeather09}Terrain" + "/" + "{=RealisticWeather17}Steppes", GroupOrder = 1)]
        public float SteppeFogChance { get; set; } = 0.05f;

        [SettingPropertyFloatingInteger("{=RealisticWeather12}Chance of Dust Storms", 0.00f, 1.00f, "#0%", Order = 2, RequireRestart = false, HintText = "{=RealisticWeather20}Chance of dust storms in steppes. Default is 0%.")]
        [SettingPropertyGroup("{=RealisticWeather09}Terrain" + "/" + "{=RealisticWeather17}Steppes", GroupOrder = 1)]
        public float SteppeDustChance { get; set; } = 0f;

        [SettingPropertyFloatingInteger("{=RealisticWeather10}Chance of Rain", 0.00f, 1.00f, "#0%", Order = 0, RequireRestart = false, HintText = "{=RealisticWeather22}Chance of rain in deserts. Default is 1%.")]
        [SettingPropertyGroup("{=RealisticWeather09}Terrain" + "/" + "{=RealisticWeather21}Deserts", GroupOrder = 2)]
        public float DesertRainChance { get; set; } = 0.01f;

        [SettingPropertyFloatingInteger("{=RealisticWeather11}Chance of Fog", 0.00f, 1.00f, "#0%", Order = 1, RequireRestart = false, HintText = "{=RealisticWeather23}Chance of fog in deserts. Default is 0%.")]
        [SettingPropertyGroup("{=RealisticWeather09}Terrain" + "/" + "{=RealisticWeather21}Deserts", GroupOrder = 2)]
        public float DesertFogChance { get; set; } = 0f;

        [SettingPropertyFloatingInteger("{=RealisticWeather12}Chance of Dust Storms", 0.00f, 1.00f, "#0%", Order = 2, RequireRestart = false, HintText = "{=RealisticWeather24}Chance of dust storms in deserts. Default is 10%.")]
        [SettingPropertyGroup("{=RealisticWeather09}Terrain" + "/" + "{=RealisticWeather21}Deserts", GroupOrder = 2)]
        public float DesertDustChance { get; set; } = 0.1f;

        [SettingPropertyFloatingInteger("{=RealisticWeather10}Chance of Rain", 0.00f, 1.00f, "#0%", Order = 0, RequireRestart = false, HintText = "{=RealisticWeather26}Chance of rain near rivers. Default is 10%.")]
        [SettingPropertyGroup("{=RealisticWeather09}Terrain" + "/" + "{=RealisticWeather25}Rivers", GroupOrder = 3)]
        public float RiverRainChance { get; set; } = 0.1f;

        [SettingPropertyFloatingInteger("{=RealisticWeather11}Chance of Fog", 0.00f, 1.00f, "#0%", Order = 1, RequireRestart = false, HintText = "{=RealisticWeather27}Chance of fog near rivers. Default is 20%.")]
        [SettingPropertyGroup("{=RealisticWeather09}Terrain" + "/" + "{=RealisticWeather25}Rivers", GroupOrder = 3)]
        public float RiverFogChance { get; set; } = 0.2f;

        [SettingPropertyFloatingInteger("{=RealisticWeather12}Chance of Dust Storms", 0.00f, 1.00f, "#0%", Order = 2, RequireRestart = false, HintText = "{=RealisticWeather28}Chance of dust storms near rivers. Default is 0%.")]
        [SettingPropertyGroup("{=RealisticWeather09}Terrain" + "/" + "{=RealisticWeather25}Rivers", GroupOrder = 3)]
        public float RiverDustChance { get; set; } = 0f;

        [SettingPropertyFloatingInteger("{=RealisticWeather10}Chance of Rain", 0.00f, 1.00f, "#0%", Order = 0, RequireRestart = false, HintText = "{=RealisticWeather30}Chance of rain in other terrain. Default is 10%.")]
        [SettingPropertyGroup("{=RealisticWeather09}Terrain" + "/" + "{=RealisticWeather29}Other", GroupOrder = 4)]
        public float OtherRainChance { get; set; } = 0.1f;

        [SettingPropertyFloatingInteger("{=RealisticWeather11}Chance of Fog", 0.00f, 1.00f, "#0%", Order = 1, RequireRestart = false, HintText = "{=RealisticWeather31}Chance of fog in other terrain. Default is 10%.")]
        [SettingPropertyGroup("{=RealisticWeather09}Terrain" + "/" + "{=RealisticWeather29}Other", GroupOrder = 4)]
        public float OtherFogChance { get; set; } = 0.1f;

        [SettingPropertyFloatingInteger("{=RealisticWeather12}Chance of Dust Storms", 0.00f, 1.00f, "#0%", Order = 2, RequireRestart = false, HintText = "{=RealisticWeather32}Chance of dust storms in other terrain. Default is 0%.")]
        [SettingPropertyGroup("{=RealisticWeather09}Terrain" + "/" + "{=RealisticWeather29}Other", GroupOrder = 4)]
        public float OtherDustChance { get; set; } = 0f;

        [SettingPropertyBool("{=RealisticWeather34}Rain", Order = 0, RequireRestart = false, HintText = "{=RealisticWeather37}Arena can have rain. Disabled by default.")]
        [SettingPropertyGroup("{=RealisticWeather33}Arena", GroupOrder = 5)]
        public bool CanArenaHaveRain { get; set; } = false;

        [SettingPropertyBool("{=RealisticWeather35}Fog", Order = 1, RequireRestart = false, HintText = "{=RealisticWeather38}Arena can have fog. Disabled by default.")]
        [SettingPropertyGroup("{=RealisticWeather33}Arena", GroupOrder = 5)]
        public bool CanArenaHaveFog { get; set; } = false;

        [SettingPropertyBool("{=RealisticWeather36}Dust Storms", Order = 2, RequireRestart = false, HintText = "{=RealisticWeather39}Arena can have dust storms. Disabled by default.")]
        [SettingPropertyGroup("{=RealisticWeather33}Arena", GroupOrder = 5)]
        public bool CanArenaHaveDust { get; set; } = false;

        [SettingPropertyBool("{=RealisticWeather41}Toggle Override Rain Density", Order = 0, RequireRestart = false, HintText = "{=RealisticWeather42}Override rain density. Disabled by default.", IsToggle = true)]
        [SettingPropertyGroup("{=RealisticWeather40}Override Rain Density", GroupOrder = 6)]
        public bool ShouldOverrideRainDensity { get; set; } = false;

        [SettingPropertyFloatingInteger("{=RealisticWeather07}Rain/Snow Density", 0.00f, 1.00f, "0.00", Order = 1, RequireRestart = false, HintText = "{=RealisticWeather43}Overridden rain density. Default is 0.00.")]
        [SettingPropertyGroup("{=RealisticWeather40}Override Rain Density", GroupOrder = 6)]
        public float OverriddenRainDensity { get; set; } = 0;

        [SettingPropertyBool("{=RealisticWeather45}Toggle Override Fog Density", Order = 0, RequireRestart = false, HintText = "{=RealisticWeather46}Override fog density. Disabled by default.", IsToggle = true)]
        [SettingPropertyGroup("{=RealisticWeather44}Override Fog Density", GroupOrder = 7)]
        public bool ShouldOverrideFogDensity { get; set; } = false;

        [SettingPropertyFloatingInteger("{=RealisticWeather08}Fog Density", 1, 32, "0", Order = 1, RequireRestart = false, HintText = "{=RealisticWeather47}Overridden fog density. Default is 1.")]
        [SettingPropertyGroup("{=RealisticWeather44}Override Fog Density", GroupOrder = 7)]
        public float OverriddenFogDensity { get; set; } = 1;

        [SettingPropertyFloatingInteger("{=RealisticWeather49}Rain Effect on Movement Speed", 0.0f, 2.0f, "0.0", Order = 0, RequireRestart = false, HintText = "{=RealisticWeather50}Multiplier for rain effect on movement speed. Default is 1.0.")]
        [SettingPropertyGroup("{=RealisticWeather48}Multipliers", GroupOrder = 8)]
        public float RainEffectOnMovementSpeedMultiplier { get; set; } = 1;

        [SettingPropertyFloatingInteger("{=RealisticWeather51}Rain Effect on Weapon Inaccuracy", 0.0f, 2.0f, "0.0", Order = 1, RequireRestart = false, HintText = "{=RealisticWeather52}Multiplier for rain effect on ranged weapon inaccuracy. Default is 1.0.")]
        [SettingPropertyGroup("{=RealisticWeather48}Multipliers", GroupOrder = 8)]
        public float RainEffectOnWeaponInaccuracyMultiplier { get; set; } = 1;

        [SettingPropertyFloatingInteger("{=RealisticWeather53}Rain Effect on Shooter Error", 0.0f, 2.0f, "0.0", Order = 2, RequireRestart = false, HintText = "{=RealisticWeather54}Multiplier for rain effect on AI shooter error. Default is 1.0.")]
        [SettingPropertyGroup("{=RealisticWeather48}Multipliers", GroupOrder = 8)]
        public float RainEffectOnShooterErrorMultiplier { get; set; } = 1;

        [SettingPropertyFloatingInteger("{=RealisticWeather55}Rain Effect on Morale", 0.0f, 2.0f, "0.0", Order = 3, RequireRestart = false, HintText = "{=RealisticWeather56}Multiplier for rain effect on morale. Default is 1.0.")]
        [SettingPropertyGroup("{=RealisticWeather48}Multipliers", GroupOrder = 8)]
        public float RainEffectOnMoraleMultiplier { get; set; } = 1;

        [SettingPropertyFloatingInteger("{=RealisticWeather57}Rain Effect on Posture", 0.0f, 2.0f, "0.0", Order = 4, RequireRestart = false, HintText = "{=RealisticWeather58}Multiplier for rain effect on RBM posture. Default is 1.0.")]
        [SettingPropertyGroup("{=RealisticWeather48}Multipliers", GroupOrder = 8)]
        public float RainEffectOnPostureMultiplier { get; set; } = 1;

        [SettingPropertyFloatingInteger("{=RealisticWeather59}Fog Effect on Shooter Error", 0.0f, 2.0f, "0.0", Order = 5, RequireRestart = false, HintText = "{=RealisticWeather60}Multiplier for fog effect on AI shooter error. Default is 1.0.")]
        [SettingPropertyGroup("{=RealisticWeather48}Multipliers", GroupOrder = 8)]
        public float FogEffectOnShooterErrorMultiplier { get; set; } = 1;

        [SettingPropertyFloatingInteger("{=RealisticWeather61}Fog Effect on Morale", 0.0f, 2.0f, "0.0", Order = 6, RequireRestart = false, HintText = "{=RealisticWeather62}Multiplier for fog effect on morale. Default is 1.0.")]
        [SettingPropertyGroup("{=RealisticWeather48}Multipliers", GroupOrder = 8)]
        public float FogEffectOnMoraleMultiplier { get; set; } = 1;

        [SettingPropertyFloatingInteger("{=RealisticWeather63}Dust Effect on Movement Speed", 0.0f, 2.0f, "0.0", Order = 7, RequireRestart = false, HintText = "{=RealisticWeather64}Multiplier for dust effect on movement speed. Default is 1.0.")]
        [SettingPropertyGroup("{=RealisticWeather48}Multipliers", GroupOrder = 8)]
        public float DustEffectOnMovementSpeedMultiplier { get; set; } = 1;

        [SettingPropertyFloatingInteger("{=RealisticWeather65}Dust Effect on Weapon Inaccuracy", 0.0f, 2.0f, "0.0", Order = 8, RequireRestart = false, HintText = "{=RealisticWeather66}Multiplier for dust effect on ranged weapon inaccuracy. Default is 1.0.")]
        [SettingPropertyGroup("{=RealisticWeather48}Multipliers", GroupOrder = 8)]
        public float DustEffectOnWeaponInaccuracyMultiplier { get; set; } = 1;

        [SettingPropertyFloatingInteger("{=RealisticWeather67}Dust Effect on Shooter Error", 0.0f, 2.0f, "0.0", Order = 9, RequireRestart = false, HintText = "{=RealisticWeather68}Multiplier for dust effect on AI shooter error. Default is 1.0.")]
        [SettingPropertyGroup("{=RealisticWeather48}Multipliers", GroupOrder = 8)]
        public float DustEffectOnShooterErrorMultiplier { get; set; } = 1;

        [SettingPropertyFloatingInteger("{=RealisticWeather69}Dust Effect on Morale", 0.0f, 2.0f, "0.0", Order = 10, RequireRestart = false, HintText = "{=RealisticWeather70}Multiplier for dust effect on morale. Default is 1.0.")]
        [SettingPropertyGroup("{=RealisticWeather48}Multipliers", GroupOrder = 8)]
        public float DustEffectOnMoraleMultiplier { get; set; } = 1;

        [SettingPropertyFloatingInteger("{=RealisticWeather71}Dust Effect on Posture", 0.0f, 2.0f, "0.0", Order = 11, RequireRestart = false, HintText = "{=RealisticWeather72}Multiplier for dust effect on RBM posture. Default is 1.0.")]
        [SettingPropertyGroup("{=RealisticWeather48}Multipliers", GroupOrder = 8)]
        public float DustEffectOnPostureMultiplier { get; set; } = 1;

        [SettingPropertyBool("{=RealisticWeather73}Hide in Free Camera Mode", Order = 0, RequireRestart = false, HintText = "{=RealisticWeather74}Hide rain, snow and dust storms in RTS Camera's free camera mode. Weather effects will still apply. Disabled by default.")]
        [SettingPropertyGroup("RTS Camera", GroupOrder = 9)]
        public bool ShouldHideInFreeCamera { get; set; } = false;
    }
}
