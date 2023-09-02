using System;
using System.Collections.Generic;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Map;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace RealisticWeather.Behaviors
{
    public class RealisticWeatherCampaignBehavior : CampaignBehaviorBase
    {
        private List<Vec3> _prefabPositions;

        public RealisticWeatherCampaignBehavior() => _prefabPositions = new List<Vec3>();

        public override void RegisterEvents()
        {
            CampaignEvents.OnGameLoadedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(OnGameLoaded));
            CampaignEvents.DailyTickEvent.AddNonSerializedListener(this, new Action(OnDailyTick));
            CampaignEvents.HourlyTickEvent.AddNonSerializedListener(this, new Action(OnHourlyTick));
            CampaignEvents.TickEvent.AddNonSerializedListener(this, new Action<float>(OnTick));
        }

        public override void SyncData(IDataStore dataStore)
        {
            try
            {
                dataStore.SyncData("_prefabPositions", ref _prefabPositions);
            }
            catch (Exception)
            {
                if (dataStore.IsLoading)
                {
                    InformationManager.DisplayMessage(new InformationMessage(MethodBase.GetCurrentMethod().DeclaringType.FullName + "." + MethodBase.GetCurrentMethod().Name + ": Error loading save file!"));
                }
            }
        }

        private void OnGameLoaded(CampaignGameStarter campaignGameStarter)
        {
            for (int i = 0; i < _prefabPositions.Count; i++)
            {
                if (_prefabPositions[i].z == 2 || _prefabPositions[i].z == 3)
                {
                    // Reset the z of each position to 0 and 1.
                    _prefabPositions[i] -= new Vec3(0, 0, 2);
                }
            }
        }

        private void OnDailyTick()
        {
            foreach (Vec3 prefabPosition in _prefabPositions.FindAll(p => p.z > 3))
            {
                // If a position has z greater than 3, remove it from _prefabPositions.
                _prefabPositions.Remove(prefabPosition);
            }

            for (int i = 0; i < _prefabPositions.Count; i++)
            {
                if (_prefabPositions[i].z == 2 || _prefabPositions[i].z == 3)
                {
                    // Increment the z of each position by 2.
                    _prefabPositions[i] += new Vec3(0, 0, 2);
                }
            }
        }

        private void OnHourlyTick()
        {
            // Find a random position on the campaign map.
            IMapScene mapSceneWrapper = Campaign.Current.MapSceneWrapper;
            Vec2 terrainSize = mapSceneWrapper.GetTerrainSize();
            Vec2 position = new Vec2(MBRandom.RandomFloatRanged(terrainSize.X), MBRandom.RandomFloatRanged(terrainSize.Y));
            TerrainType terrainType = mapSceneWrapper.GetTerrainTypeAtPosition(position);
            float z = 0f;

            mapSceneWrapper.GetHeightAtPoint(position, ref z);

            // Spawn dust storms and fog banks on the campaign map with a 10% chance.
            if (MBRandom.RandomFloat < 0.1f)
            {
                if (terrainType == TerrainType.Desert)
                {
                    // For dust storms, add the position to _prefabPositions converted to Vec3 with z as 0.
                    _prefabPositions.Add(position.ToVec3(0));
                }
                else if (z >= 10f)
                {
                    // For fog banks, add the position to _prefabPositions converted to Vec3 with z as 1.
                    _prefabPositions.Add(position.ToVec3(1));
                }
            }
        }

        private void OnTick(float dt) => RealisticWeatherManager.Current.SetPrefabPositions(_prefabPositions);
    }
}
