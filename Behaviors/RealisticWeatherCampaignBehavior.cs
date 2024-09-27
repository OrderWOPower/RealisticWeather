using System;
using System.Collections.Generic;
using System.Linq;
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

        private void OnGameLoaded(CampaignGameStarter campaignGameStarter) => RealisticWeatherManager.Current.SetPrefabPositions(_prefabPositions);

        private void OnDailyTick()
        {
            int count = _prefabPositions.Count(p => p.z >= 3);

            if (count > 5)
            {
                // If a position has z greater than or equal to 3, remove it from _prefabPositions.
                _prefabPositions.RemoveRange(0, count - 5);
            }
        }

        private void OnHourlyTick()
        {
            // Despawn dust storms and fog banks on the campaign map with a 10% chance.
            if (MBRandom.RandomFloat < 0.1f)
            {
                int index = _prefabPositions.FindIndex(p => p.z < 3);

                if (index > -1)
                {
                    // Increment the z of the first position by 2.
                    _prefabPositions[index] += new Vec3(0, 0, 2);
                }
            }

            // Spawn dust storms and fog banks on the campaign map with a 5% chance.
            if (MBRandom.RandomFloat < 0.05f)
            {
                IMapScene mapSceneWrapper = Campaign.Current.MapSceneWrapper;
                List<Vec2> positions = new List<Vec2>();
                Vec2 position;

                for (int i = 0; i < mapSceneWrapper.GetNumberOfNavigationMeshFaces(); i++)
                {
                    positions.Add(mapSceneWrapper.GetNavigationMeshCenterPosition(new PathFaceRecord(i, -1, -1)));
                }

                position = positions.GetRandomElementWithPredicate(p => mapSceneWrapper.GetTerrainTypeAtPosition(p) == TerrainType.Desert || mapSceneWrapper.GetTerrainTypeAtPosition(p) == TerrainType.Forest);

                if (position != Vec2.Zero)
                {
                    if (mapSceneWrapper.GetTerrainTypeAtPosition(position) == TerrainType.Desert)
                    {
                        // For dust storms, add the position to _prefabPositions converted to Vec3 with z as 1.
                        _prefabPositions.Add(position.ToVec3(1));
                    }
                    else if (mapSceneWrapper.GetTerrainTypeAtPosition(position) == TerrainType.Forest)
                    {
                        // For fog banks, add the position to _prefabPositions converted to Vec3 with z as 2.
                        _prefabPositions.Add(position.ToVec3(2));
                    }
                }
            }

            RealisticWeatherManager.Current.SetPrefabPositions(_prefabPositions);
        }
    }
}
