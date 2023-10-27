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
            GameEntity prefab = null;
            RealisticWeatherManager manager = RealisticWeatherManager.Current;

            foreach (Vec3 prefabPosition in manager.PrefabPositions)
            {
                Vec2 position = prefabPosition.AsVec2;

                switch (prefabPosition.z)
                {
                    case 1:
                        if (!_prefabs.TryGetValue(position, out _))
                        {
                            AttachNewDustPrefabToVisual(position, ref prefab);

                            _prefabs.Add(position, prefab);
                        }

                        break;
                    case 2:
                        if (!_prefabs.TryGetValue(position, out _))
                        {
                            AttachNewFogPrefabToVisual(position, ref prefab);

                            _prefabs.Add(position, prefab);
                        }

                        break;
                    case 3:
                        if (_prefabs.TryGetValue(position, out prefab))
                        {
                            manager.ReleaseDustPrefab(prefab);

                            _prefabs.Remove(position);
                        }

                        break;
                    case 4:
                        if (_prefabs.TryGetValue(position, out prefab))
                        {
                            manager.ReleaseFogPrefab(prefab);

                            _prefabs.Remove(position);
                        }

                        break;
                }
            }
        }

        private void AttachNewDustPrefabToVisual(Vec2 position, ref GameEntity dustPrefabFromPool)
        {
            MatrixFrame identity = MatrixFrame.Identity;
            dustPrefabFromPool = RealisticWeatherManager.Current.GetDustPrefabFromPool();

            identity.origin = position.ToVec3();
            dustPrefabFromPool.SetVisibilityExcludeParents(true);
            dustPrefabFromPool.SetGlobalFrame(identity);
        }

        private void AttachNewFogPrefabToVisual(Vec2 position, ref GameEntity fogPrefabFromPool)
        {
            MatrixFrame identity = MatrixFrame.Identity;
            fogPrefabFromPool = RealisticWeatherManager.Current.GetFogPrefabFromPool();

            identity.origin = position.ToVec3();
            fogPrefabFromPool.SetVisibilityExcludeParents(true);
            fogPrefabFromPool.SetGlobalFrame(identity);
        }
    }
}
