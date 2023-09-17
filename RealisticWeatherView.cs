using SandBox.View.Map;
using System.Collections.Generic;
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
            RealisticWeatherManager manager = RealisticWeatherManager.Current;
            List<Vec3> prefabPositions = manager.PrefabPositions;

            for (int i = 0; i < prefabPositions?.Count; i++)
            {
                Vec2 position = prefabPositions[i].AsVec2;

                switch (prefabPositions[i].z)
                {
                    case 0:
                        AttachNewDustPrefabToVisual(position);
                        manager.PrefabPositions[i] = position.ToVec3(2);

                        break;
                    case 1:
                        AttachNewFogPrefabToVisual(position);
                        manager.PrefabPositions[i] = position.ToVec3(3);

                        break;
                    case 4:
                        if (_prefabs.ContainsKey(position))
                        {
                            manager.ReleaseDustPrefab(_prefabs[position]);

                            _prefabs.Remove(position);
                        }

                        break;
                    case 5:
                        if (_prefabs.ContainsKey(position))
                        {
                            manager.ReleaseFogPrefab(_prefabs[position]);

                            _prefabs.Remove(position);
                        }

                        break;
                }
            }
        }

        private void AttachNewDustPrefabToVisual(Vec2 position)
        {
            MatrixFrame identity = MatrixFrame.Identity;
            GameEntity dustPrefabFromPool = RealisticWeatherManager.Current.GetDustPrefabFromPool();

            identity.origin = position.ToVec3();
            dustPrefabFromPool.SetVisibilityExcludeParents(true);
            dustPrefabFromPool.SetGlobalFrame(identity);

            _prefabs.Add(position, dustPrefabFromPool);
        }

        private void AttachNewFogPrefabToVisual(Vec2 position)
        {
            MatrixFrame identity = MatrixFrame.Identity;
            GameEntity fogPrefabFromPool = RealisticWeatherManager.Current.GetFogPrefabFromPool();

            identity.origin = position.ToVec3();
            fogPrefabFromPool.SetVisibilityExcludeParents(true);
            fogPrefabFromPool.SetGlobalFrame(identity);

            _prefabs.Add(position, fogPrefabFromPool);
        }
    }
}
