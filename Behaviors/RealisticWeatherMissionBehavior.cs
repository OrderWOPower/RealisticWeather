using HarmonyLib;
using SandBox.Missions.MissionLogics.Arena;
using System.Linq;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.CustomBattle;

namespace RealisticWeather.Behaviors
{
    public class RealisticWeatherMissionBehavior : MissionBehavior
    {
        private SoundEvent _rainSound, _dustSound;

        public override MissionBehaviorType BehaviorType => MissionBehaviorType.Other;

        public override void AfterStart()
        {
            Scene scene = Mission.Scene;

            RealisticWeatherManager.Current.SetDust(false);

            if (!scene.IsAtmosphereIndoor)
            {
                float rainDensity = -1f, fogDensity = 0f;
                bool hasDust = false;

                if (Game.Current.GameType is Campaign)
                {
                    MapWeatherModel.WeatherEvent weatherEventInPosition = Campaign.Current.Models.MapWeatherModel.GetWeatherEventInPosition(MobileParty.MainParty.GetPosition2D);
                    Vec3 weatherEventPosition = RealisticWeatherManager.Current.WeatherEventPositions.FirstOrDefault(p => p.AsVec2.Distance(MobileParty.MainParty.GetPosition2D) <= 20f);
                    RealisticWeatherSettings settings = RealisticWeatherSettings.Instance;

                    if (weatherEventInPosition == MapWeatherModel.WeatherEvent.LightRain)
                    {
                        rainDensity = MBRandom.RandomFloatRanged(0.7f, 0.85f);
                    }
                    else if (weatherEventInPosition == MapWeatherModel.WeatherEvent.Snowy)
                    {
                        rainDensity = MBRandom.RandomFloatRanged(0.55f, 0.85f);
                    }
                    else if (weatherEventInPosition == MapWeatherModel.WeatherEvent.HeavyRain || weatherEventInPosition == MapWeatherModel.WeatherEvent.Blizzard)
                    {
                        rainDensity = MBRandom.RandomFloatRanged(0.85f, 1f);
                    }

                    if (weatherEventPosition.z == 1)
                    {
                        // If the position has z as 1, spawn a dust storm in the mission.
                        hasDust = true;
                    }
                    else if (weatherEventPosition.z == 2)
                    {
                        // If the position has z as 2, spawn fog in the mission.
                        fogDensity = MBRandom.RandomInt(1, 32);
                    }

                    if (settings.ShouldOverrideRainDensity)
                    {
                        rainDensity = settings.OverriddenRainDensity;
                    }

                    if (settings.ShouldOverrideFogDensity)
                    {
                        fogDensity = settings.OverriddenFogDensity;
                    }

                    if (Mission.HasMissionBehavior<ArenaAgentStateDeciderLogic>())
                    {
                        if (!settings.CanArenaHaveRain)
                        {
                            rainDensity = 0f;
                        }

                        if (!settings.CanArenaHaveFog)
                        {
                            fogDensity = 1f;
                        }

                        if (!settings.CanArenaHaveDust)
                        {
                            hasDust = false;
                        }
                    }
                }
                else if (Game.Current.GameType is CustomGame)
                {
                    if (RealisticWeatherMixin.MixinWeakReference != null && RealisticWeatherMixin.MixinWeakReference.TryGetTarget(out RealisticWeatherMixin mixin))
                    {
                        if (mixin.SelectedRainDensity > 0f)
                        {
                            rainDensity = mixin.SelectedRainDensity;
                        }

                        if (mixin.SelectedFogDensity > 1f)
                        {
                            fogDensity = mixin.SelectedFogDensity;
                        }

                        hasDust = mixin.SelectedFogDensity == 0f;
                    }
                }

                if (rainDensity > -1f)
                {
                    Vec3 sunColor = new Vec3(255, 255, 255, 255);
                    float sunAltitude = (50 * MathF.Cos(MathF.PI * scene.TimeOfDay / 6)) + 50, sunIntensity = (1 - rainDensity) / 1000;

                    scene.SetRainDensity(rainDensity);
                    // Decrease the sun intensity according to rain density.
                    scene.SetSun(ref sunColor, sunAltitude, 0, sunIntensity);
                }

                if (fogDensity > 0f)
                {
                    Vec3 fogColor = new Vec3(1, 1, 1, 1);
                    float fogFalloff = 0.5f * MathF.Sin(MathF.PI * scene.TimeOfDay / 24);

                    scene.SetFog(fogDensity, ref fogColor, fogFalloff);
                    scene.SetFogAdvanced(0, 0, -40);
                }

                if (hasDust && rainDensity == -1f)
                {
                    Vec2 terrainSize = new Vec2(1f, 1f);

                    scene.GetTerrainData(out Vec2i nodeDimension, out float nodeSize, out _, out _);
                    terrainSize.x = nodeDimension.X * nodeSize;
                    terrainSize.y = nodeDimension.Y * nodeSize;
                    GameEntity.Instantiate(scene, "dust_prefab_entity", new MatrixFrame(Mat3.Identity, (terrainSize / 2).ToVec3()));
                    // Decrease the sky brightness.
                    scene.SetSkyBrightness((MathF.Pow(2, scene.TimeOfDay < 12 ? scene.TimeOfDay : 24 - scene.TimeOfDay) - 1) / 10);
                    // Apply the "Aserai Harsh" color grade.
                    scene.SetSceneColorGradeIndex(23);
                    RealisticWeatherManager.Current.SetDust(true);

                    _dustSound = SoundEvent.CreateEvent(SoundEvent.GetEventIdFromString("dust_storm"), scene);
                    // Play the dust storm ambient sound.
                    _dustSound.Play();
                }

                TickAfterStart();
            }
        }

        private async void TickAfterStart()
        {
            await Task.Delay(1);

            if (Mission != null)
            {
                Scene scene = Mission.Scene;
                float rainDensity = scene.GetRainDensity();

                if (rainDensity > 0f)
                {
                    Mesh skyboxMesh = scene.GetSkyboxMesh();
                    Material skyboxMaterial = skyboxMesh.GetMaterial().CreateCopy();
                    GameEntity rainPrefab = scene.GetFirstEntityWithName("rain_prefab_entity") ?? scene.GetFirstEntityWithName("snow_prefab_entity");
                    bool isWinter = ((MissionInitializerRecord)AccessTools.Property(typeof(Mission), "InitializerRecord").GetValue(Mission)).AtmosphereOnCampaign.TimeInfo.Season == 3;

                    // Change the skybox texture to an overcast sky.
                    skyboxMaterial.SetTexture(Material.MBTextureType.DiffuseMap, Texture.GetFromResource("sky_photo_overcast_01"));
                    skyboxMesh.SetMaterial(skyboxMaterial);

                    if (rainPrefab != null)
                    {
                        foreach (GameEntity entity in rainPrefab.GetChildren())
                        {
                            MatrixFrame rainFrame = entity.GetFrame();

                            rainFrame.Scale(rainFrame.GetScale() * 2);
                            entity.SetFrame(ref rainFrame);
                            // Multiply the rain particle emission rate according to rain density.
                            entity.SetRuntimeEmissionRateMultiplier((20 * MathF.Max(rainDensity - 0.7f, 0f)) + 2);
                        }
                    }

                    if (rainDensity >= 0.7f && rainDensity < 0.8f)
                    {
                        _rainSound = SoundEvent.CreateEvent(SoundEvent.GetEventIdFromString(!isWinter ? "rain_light" : "snow_light"), scene);
                    }
                    else if (rainDensity >= 0.8f && rainDensity < 0.9f)
                    {
                        _rainSound = SoundEvent.CreateEvent(SoundEvent.GetEventIdFromString(!isWinter ? "rain_moderate" : "snow_moderate"), scene);
                    }
                    else if (rainDensity >= 0.9f)
                    {
                        _rainSound = SoundEvent.CreateEvent(SoundEvent.GetEventIdFromString(!isWinter ? "rain_heavy" : "snow_heavy"), scene);
                    }

                    // Play the rain ambient sound.
                    _rainSound?.Play();
                }
            }
        }

        protected override void OnEndMission()
        {
            // Stop the ambient sounds.
            _rainSound?.Stop();
            _dustSound?.Stop();
        }
    }
}
