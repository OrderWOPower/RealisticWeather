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

            foreach (Vec3 prefabPosition in manager.PrefabPositions)
            {
                Vec2 position = prefabPosition.AsVec2;

                switch (prefabPosition.z)
                {
                    case 1:
                        if (!_prefabs.ContainsKey(position))
                        {
                            AttachNewDustPrefabToVisual(position, out GameEntity prefab);

                            _prefabs.Add(position, prefab);
                        }

                        break;
                    case 2:
                        if (!_prefabs.ContainsKey(position))
                        {
                            AttachNewFogPrefabToVisual(position, out GameEntity prefab);

                            _prefabs.Add(position, prefab);
                        }

                        break;
                    case 3:
                        if (_prefabs.ContainsKey(position))
                        {
                            manager.ReleaseDustPrefab(_prefabs[position]);

                            _prefabs.Remove(position);
                        }

                        break;
                    case 4:
                        if (_prefabs.ContainsKey(position))
                        {
                            manager.ReleaseFogPrefab(_prefabs[position]);

                            _prefabs.Remove(position);
                        }

                        break;
                }
            }
        }

        private void AttachNewDustPrefabToVisual(Vec2 position, out GameEntity dustPrefabFromPool)
        {
            MatrixFrame identity = MatrixFrame.Identity;
            dustPrefabFromPool = RealisticWeatherManager.Current.GetDustPrefabFromPool();

            identity.origin = position.ToVec3();
            dustPrefabFromPool.SetVisibilityExcludeParents(true);
            dustPrefabFromPool.SetGlobalFrame(identity);
        }

        private void AttachNewFogPrefabToVisual(Vec2 position, out GameEntity fogPrefabFromPool)
        {
            MatrixFrame identity = MatrixFrame.Identity;
            fogPrefabFromPool = RealisticWeatherManager.Current.GetFogPrefabFromPool();

            identity.origin = position.ToVec3();
            fogPrefabFromPool.SetVisibilityExcludeParents(true);
            fogPrefabFromPool.SetGlobalFrame(identity);
        }
    }
}
