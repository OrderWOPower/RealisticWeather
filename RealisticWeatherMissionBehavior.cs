using SandBox.Missions.MissionLogics.Arena;
using SandBox.Tournaments.MissionLogics;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.CustomBattle;

namespace RealisticWeather
{
    public class RealisticWeatherMissionBehavior : MissionBehavior
    {
        private bool _hasTicked;
        private SoundEvent _rainSound;
        private SoundEvent _dustSound;

        public override MissionBehaviorType BehaviorType => MissionBehaviorType.Other;

        // Add rain of a random density to a campaign mission with a 10% chance by default.
        // Add fog of a random density to a campaign mission with a 10% chance by default.
        // Add a dust storm to a campaign mission with a 10% chance by default.
        // Add rain of a selected density to a custom battle mission.
        // Add fog of a selected density to a custom battle mission.
        // Add a dust storm to a custom battle mission.
        // Decrease the sun intensity according to rain density.
        // Decrease the sky brightness if there is a dust storm.
        // Play the dust storm ambient sound if there is a dust storm.
        public override void AfterStart()
        {
            Scene scene = Mission.Scene;
            RealisticWeatherManager manager = RealisticWeatherManager.Current;
            manager.SetDust(false);
            _hasTicked = false;
            if (!scene.IsAtmosphereIndoor)
            {
                GameType gameType = Game.Current.GameType;
                float rainDensity = 0f;
                float fogDensity = 1f;
                bool hasDust = false;
                bool isArenaMission = Mission.HasMissionBehavior<TournamentBehavior>() || Mission.HasMissionBehavior<ArenaPracticeFightMissionController>();
                if (gameType is Campaign)
                {
                    RealisticWeatherSettings settings = RealisticWeatherSettings.Instance;
                    float rainChance;
                    float fogChance;
                    float dustChance;
                    switch (Mission.TerrainType)
                    {
                        case TerrainType.Forest:
                            rainChance = settings.ForestRainChance;
                            fogChance = settings.ForestFogChance;
                            dustChance = settings.ForestDustChance;
                            break;
                        case TerrainType.Steppe:
                            rainChance = settings.SteppeRainChance;
                            fogChance = settings.SteppeFogChance;
                            dustChance = settings.SteppeDustChance;
                            break;
                        case TerrainType.Desert:
                            rainChance = settings.DesertRainChance;
                            fogChance = settings.DesertFogChance;
                            dustChance = settings.DesertDustChance;
                            break;
                        case TerrainType.ShallowRiver:
                            rainChance = settings.RiverRainChance;
                            fogChance = settings.RiverFogChance;
                            dustChance = settings.RiverDustChance;
                            break;
                        default:
                            rainChance = settings.OtherRainChance;
                            fogChance = settings.OtherFogChance;
                            dustChance = settings.OtherDustChance;
                            break;
                    }
                    if (rainChance > MBRandom.RandomFloat && (!isArenaMission || (isArenaMission && settings.CanArenaHaveRain)))
                    {
                        rainDensity = !settings.ShouldOverrideRainDensity ? (Mission.TerrainType != TerrainType.Desert ? MBRandom.RandomFloatRanged(0.25f, 1f) : 1f) : settings.OverriddenRainDensity;
                    }
                    if (fogChance > MBRandom.RandomFloat && (!isArenaMission || (isArenaMission && settings.CanArenaHaveFog)))
                    {
                        fogDensity = !settings.ShouldOverrideFogDensity ? MBRandom.RandomFloatRanged(0.125f, 1f) * 32 : settings.OverriddenFogDensity;
                    }
                    hasDust = dustChance > MBRandom.RandomFloat && (!isArenaMission || (isArenaMission && settings.CanArenaHaveDust));
                }
                else if (gameType is CustomGame)
                {
                    if (RealisticWeatherHelper.HasTarget(out RealisticWeatherMixin mixin))
                    {
                        rainDensity = mixin.SelectedRainDensity;
                        fogDensity = mixin.SelectedFogDensity;
                        hasDust = fogDensity == 0f;
                    }
                }
                if (rainDensity > 0f)
                {
                    Vec3 sunColor = new Vec3(255, 255, 255, 255);
                    float sunAltitude = (50 * MathF.Cos(MathF.PI * scene.TimeOfDay / 6)) + 50;
                    float sunIntensity = (1 - rainDensity) / 1000;
                    scene.SetRainDensity(rainDensity);
                    scene.SetSun(ref sunColor, sunAltitude, 0, sunIntensity);
                }
                if (fogDensity > 1f)
                {
                    Vec3 fogColor = new Vec3(1, 1, 1, 1);
                    float fogFalloff = 0.5f * MathF.Sin(MathF.PI * scene.TimeOfDay / 24);
                    scene.SetFog(fogDensity, ref fogColor, fogFalloff);
                    scene.SetFogAdvanced(0, 0, -50);
                }
                if (hasDust && rainDensity == 0f)
                {
                    try
                    {
                        GameEntity.Instantiate(scene, "dust_prefab_entity", Mission.GetSceneMiddleFrame().ToGroundMatrixFrame());
                        scene.SetSkyBrightness(scene.TimeOfDay < 12 ? ((MathF.Pow(2, scene.TimeOfDay) - 1) / 10) : ((MathF.Pow(2, 24 - scene.TimeOfDay) - 1) / 10));
                        _dustSound = SoundEvent.CreateEvent(SoundEvent.GetEventIdFromString("dust_storm"), scene);
                        _dustSound.Play();
                        manager.SetDust(true);
                    }
                    catch (Exception)
                    {
                        InformationManager.DisplayMessage(new InformationMessage("Error generating dust storm!"));
                    }
                }
            }
        }

        // Change the skybox texture to an overcast sky.
        // Multiply the rain particle emission rate according to rain density.
        // Add copies of the background rain mesh to the scene according to rain density.
        // Play the rain ambient sound if there is rain.
        public override void OnMissionTick(float dt)
        {
            if (!_hasTicked)
            {
                Scene scene = Mission.Scene;
                Mesh skyboxMesh = scene.GetSkyboxMesh();
                Material skyboxMaterial = skyboxMesh.GetMaterial().CreateCopy();
                GameEntity rainPrefab = scene.GetFirstEntityWithName("rain_prefab_entity") ?? scene.GetFirstEntityWithName("snow_prefab_entity");
                float rainDensity = scene.GetRainDensity();
                if (rainDensity > 0f && rainPrefab != null)
                {
                    bool isWinter = rainPrefab.Name == "snow_prefab_entity";
                    skyboxMaterial.SetTexture(Material.MBTextureType.DiffuseMap, Texture.GetFromResource("sky_photo_overcast_01"));
                    skyboxMesh.SetMaterial(skyboxMaterial);
                    foreach (GameEntity entity in rainPrefab.GetChildren())
                    {
                        if (entity.Name != "rain_far")
                        {
                            entity.SetRuntimeEmissionRateMultiplier((2 * (rainDensity / 0.25f)) - 1);
                        }
                        else
                        {
                            for (int i = 1; i < (2 * (rainDensity / 0.25f)) - 1; i++)
                            {
                                Mesh rainMesh = entity.GetFirstMesh().CreateCopy();
                                MatrixFrame rainFrame = rainMesh.GetLocalFrame();
                                rainFrame.Advance(MBRandom.RandomFloat * 10);
                                rainFrame.Elevate(MBRandom.RandomFloat * 10);
                                rainFrame.Strafe(MBRandom.RandomFloat * 10);
                                rainMesh.SetLocalFrame(rainFrame);
                                entity.AddMesh(rainMesh);
                            }
                        }
                    }
                    if (rainDensity >= 0.25f && rainDensity < 0.5f)
                    {
                        _rainSound = SoundEvent.CreateEvent(SoundEvent.GetEventIdFromString(!isWinter ? "rain_light" : "snow_light"), scene);
                    }
                    else if (rainDensity >= 0.5f && rainDensity < 0.75f)
                    {
                        _rainSound = SoundEvent.CreateEvent(SoundEvent.GetEventIdFromString(!isWinter ? "rain_moderate" : "snow_moderate"), scene);
                    }
                    else if (rainDensity >= 0.75f)
                    {
                        _rainSound = SoundEvent.CreateEvent(SoundEvent.GetEventIdFromString(!isWinter ? "rain_heavy" : "snow_heavy"), scene);
                    }
                    _rainSound?.Play();
                }
                _hasTicked = true;
            }
        }

        // Stop the ambient sounds.
        protected override void OnEndMission()
        {
            _rainSound?.Stop();
            _dustSound?.Stop();
        }
    }
}
