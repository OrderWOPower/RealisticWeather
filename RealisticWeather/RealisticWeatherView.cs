using SandBox;
using SandBox.View.Map;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace RealisticWeather
{
	public class RealisticWeatherView : MapView
	{
		private readonly Dictionary<Vec2, GameEntity> _prefabs;

		public RealisticWeatherView() => _prefabs = new Dictionary<Vec2, GameEntity>();

		protected override void OnMapScreenUpdate(float dt)
		{
			GameEntity prefab;

			foreach (Vec3 weatherEventPosition in RealisticWeatherManager.Current.WeatherEventPositions)
			{
				Vec2 position = weatherEventPosition.AsVec2;

				switch (weatherEventPosition.z)
				{
					case 1:
						if (!_prefabs.TryGetValue(position, out _))
						{
							prefab = GameEntity.Instantiate(((MapScene)Campaign.Current.MapSceneWrapper).Scene, "campaign_dust_prefab", new MatrixFrame(Mat3.Identity, position.ToVec3()));

							_prefabs.Add(position, prefab);
						}

						break;
					case 2:
						if (!_prefabs.TryGetValue(position, out _))
						{
							prefab = GameEntity.Instantiate(((MapScene)Campaign.Current.MapSceneWrapper).Scene, "campaign_fog_prefab", new MatrixFrame(Mat3.Identity, position.ToVec3()));

							_prefabs.Add(position, prefab);
						}

						break;
					case 3:
						if (_prefabs.TryGetValue(position, out prefab))
						{
							prefab.Remove(0);

							_prefabs.Remove(position);
						}

						break;
					case 4:
						if (_prefabs.TryGetValue(position, out prefab))
						{
							prefab.Remove(0);

							_prefabs.Remove(position);
						}

						break;
				}
			}
		}
	}
}
