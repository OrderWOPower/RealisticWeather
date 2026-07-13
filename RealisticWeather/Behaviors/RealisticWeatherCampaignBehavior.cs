using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Map;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace RealisticWeather.Behaviors
{
	public class RealisticWeatherCampaignBehavior : CampaignBehaviorBase
	{
		private readonly List<Vec2> _dustPositions, _fogPositions;
		private List<Vec3> _weatherEventPositions;
		private List<CampaignTime> _weatherEventTimes;

		public RealisticWeatherCampaignBehavior()
		{
			_dustPositions = new List<Vec2>();
			_fogPositions = new List<Vec2>();
			_weatherEventPositions = new List<Vec3>();
			_weatherEventTimes = new List<CampaignTime>();
		}

		public override void RegisterEvents()
		{
			CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(OnSessionLaunched));
			CampaignEvents.OnGameLoadedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(OnGameLoaded));
			CampaignEvents.DailyTickEvent.AddNonSerializedListener(this, new Action(OnDailyTick));
			CampaignEvents.HourlyTickEvent.AddNonSerializedListener(this, new Action(OnHourlyTick));
		}

		public override void SyncData(IDataStore dataStore)
		{
			try
			{
				dataStore.SyncData("_weatherEventPositions", ref _weatherEventPositions);
				dataStore.SyncData("_weatherEventTimes", ref _weatherEventTimes);
			}
			catch (Exception ex)
			{
				InformationManager.DisplayMessage(new InformationMessage(ex.ToString()));
			}
		}

		private void OnSessionLaunched(CampaignGameStarter campaignGameStarter)
		{
			IMapScene mapSceneWrapper = Campaign.Current.MapSceneWrapper;

			for (int i = 0; i < mapSceneWrapper.GetNumberOfNavigationMeshFaces(); i++)
			{
				Vec2 position = mapSceneWrapper.GetNavigationMeshCenterPosition(i);
				CampaignVec2 positionOnLand = new CampaignVec2(position, true), positionOnSea = new CampaignVec2(position, false);

				if (positionOnLand.IsValid())
				{
					if (mapSceneWrapper.GetTerrainTypeAtPosition(positionOnLand) == TerrainType.Desert)
					{
						_dustPositions.Add(position);
					}
					else if (mapSceneWrapper.GetTerrainTypeAtPosition(positionOnLand) == TerrainType.Forest)
					{
						_fogPositions.Add(position);
					}
				}
				else if (positionOnSea.IsValid() && mapSceneWrapper.GetTerrainTypeAtPosition(positionOnSea) == TerrainType.CoastalSea)
				{
					_fogPositions.Add(position);
				}
			}
		}

		private void OnGameLoaded(CampaignGameStarter campaignGameStarter) => RealisticWeatherManager.Current.SetWeatherEventPositions(_weatherEventPositions);

		private void OnDailyTick()
		{
			int count = _weatherEventPositions.Count(p => p.z >= 3);

			if (count > 5)
			{
				// If a position has z greater than or equal to 3, remove it from _weatherEventPositions.
				_weatherEventPositions.RemoveRange(0, count - 5);
				_weatherEventTimes.RemoveRange(0, count - 5);
			}
		}

		private void OnHourlyTick()
		{
			for (int i = 0; i < _weatherEventPositions.Count; i++)
			{
				// Despawn weather events on the campaign map with a 5% chance for every hour of the weather event's lifetime.
				if (MBRandom.RandomFloat < 0.05f * (CampaignTime.Now - _weatherEventTimes[i]).ToHours && _weatherEventPositions[i].z < 3)
				{
					// Increment the z of the position by 2.
					_weatherEventPositions[i] += new Vec3(0, 0, 2);
				}
			}

			// Spawn dust storms on the campaign map with a 10% chance.
			if (MBRandom.RandomFloat < 0.1f)
			{
				Vec2 position = _dustPositions.GetRandomElement();

				if (position != Vec2.Zero)
				{
					// For dust storms, add the position to _weatherEventPositions converted to Vec3 with z as 1.
					_weatherEventPositions.Add(position.ToVec3(1));
					_weatherEventTimes.Add(CampaignTime.Now);
				}
			}

			// Spawn fog banks on the campaign map with a 20% chance.
			if (MBRandom.RandomFloat < 0.2f)
			{
				Vec2 position = _fogPositions.GetRandomElement();

				if (position != Vec2.Zero)
				{
					// For fog banks, add the position to _weatherEventPositions converted to Vec3 with z as 2.
					_weatherEventPositions.Add(position.ToVec3(2));
					_weatherEventTimes.Add(CampaignTime.Now);
				}
			}

			RealisticWeatherManager.Current.SetWeatherEventPositions(_weatherEventPositions);
		}
	}
}
