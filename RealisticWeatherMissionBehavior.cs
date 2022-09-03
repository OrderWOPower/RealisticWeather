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
        private bool _hasSetSkyboxAndParticles;
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
            if (!scene.IsAtmosphereIndoor)
            {
                GameType gameType = Game.Current.GameType;
                float rainDensity = 0f;
                float fogDensity = 1f;
                bool hasDust = false;
                if (gameType is Campaign)
                {
                    float rainChance = 0.1f;
                    float fogChance = 0.1f;
                    float dustChance = 0f;
                    switch (Mission.TerrainType)
                    {
                        case TerrainType.Forest:
                            rainChance = 0.2f;
                            break;
                        case TerrainType.ShallowRiver:
                            fogChance = 0.2f;
                            break;
                        case TerrainType.Steppe:
                            rainChance = 0.05f;
                            fogChance = 0.05f;
                            break;
                        case TerrainType.Desert:
                            rainChance = 0.01f;
                            fogChance = 0f;
                            dustChance = 0.1f;
                            break;
                    }
                    if (rainChance > MBRandom.RandomFloat)
                    {
                        rainDensity = Mission.TerrainType != TerrainType.Desert ? MBRandom.RandomFloatRanged(0.25f, 1f) : 1f;
                    }
                    if (fogChance > MBRandom.RandomFloat)
                    {
                        fogDensity = MBRandom.RandomFloatRanged(0.125f, 1f) * 64;
                    }
                    hasDust = dustChance > MBRandom.RandomFloat;
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
                    scene.SetFogAdvanced(0, 0.1f, 0);
                }
                if (hasDust && rainDensity == 0f)
                {
                    try
                    {
                        GameEntity.Instantiate(scene, "dust_prefab_entity", Mission.GetSceneMiddleFrame().ToGroundMatrixFrame());
                        scene.SetSkyBrightness(scene.TimeOfDay < 12 ? ((MathF.Pow(2, scene.TimeOfDay) - 1) / 10) : ((MathF.Pow(2, 24 - scene.TimeOfDay) - 1) / 10));
                        _dustSound = SoundEvent.CreateEvent(SoundEvent.GetEventIdFromString("dust_storm"), scene);
                        _dustSound.Play();
                    }
                    catch (Exception) { }
                }
            }
            _hasSetSkyboxAndParticles = false;
        }

        // Change the skybox texture to an overcast sky.
        // Multiply the rain particle emission rate according to rain density.
        // Add copies of the background rain mesh to the scene according to rain density.
        public override void OnMissionTick(float dt)
        {
            if (!_hasSetSkyboxAndParticles)
            {
                Scene scene = Mission.Scene;
                Mesh skyboxMesh = scene.GetFirstEntityWithName("__skybox__").GetFirstMesh();
                Material skyboxMaterial = skyboxMesh.GetMaterial().CreateCopy();
                GameEntity rainPrefab = scene.GetFirstEntityWithName("rain_prefab_entity") ?? scene.GetFirstEntityWithName("snow_prefab_entity");
                float rainDensity = scene.GetRainDensity();
                if (rainDensity > 0f && rainPrefab != null)
                {
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
                            for (int i = 1; i < (4 * (rainDensity / 0.25f)) - 3; i++)
                            {
                                GameEntity rain = GameEntity.CopyFromPrefab(entity);
                                MatrixFrame rainFrame = new MatrixFrame(rain.GetFrame().rotation, rain.GetFrame().origin + new Vec3(MBRandom.RandomFloat * 10, MBRandom.RandomFloat * 10, MBRandom.RandomFloat * 10));
                                rain.SetFrame(ref rainFrame);
                                rainPrefab.AddChild(rain);
                            }
                        }
                    }
                    _hasSetSkyboxAndParticles = true;
                }
            }
        }

        // Stop the dust storm ambient sound.
        public override void HandleOnCloseMission() => _dustSound?.Stop();
    }
}
