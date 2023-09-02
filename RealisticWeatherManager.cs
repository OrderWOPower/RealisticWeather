using HarmonyLib;
using SandBox.View.Map;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace RealisticWeather
{
    public class RealisticWeatherManager
    {
        private static readonly RealisticWeatherManager _realisticWeatherManager = new RealisticWeatherManager();

        private readonly List<GameEntity> _unusedDustPrefabEntityPool, _unusedFogPrefabEntityPool;

        public static RealisticWeatherManager Current => _realisticWeatherManager;

        public bool HasDust { get; set; }

        public List<Vec3> PrefabPositions { get; set; }

        public RealisticWeatherManager()
        {
            _unusedDustPrefabEntityPool = new List<GameEntity>();
            _unusedFogPrefabEntityPool = new List<GameEntity>();
        }

        public void SetDust(bool hasDust) => HasDust = hasDust;

        public void SetPrefabPositions(List<Vec3> prefabPositions) => PrefabPositions = prefabPositions;

        public GameEntity GetDustPrefabFromPool()
        {
            GameEntity gameEntity;

            if (_unusedDustPrefabEntityPool.IsEmpty())
            {
                _unusedDustPrefabEntityPool.AddRange((IEnumerable<GameEntity>)AccessTools.Method(typeof(MapWeatherVisualManager), "CreateNewWeatherPrefabPoolElements").Invoke(MapWeatherVisualManager.Current, new object[] { "campaign_dust_prefab", 5 }));
            }

            gameEntity = _unusedDustPrefabEntityPool[0];

            _unusedDustPrefabEntityPool.Remove(gameEntity);

            return gameEntity;
        }

        public GameEntity GetFogPrefabFromPool()
        {
            GameEntity gameEntity;

            if (_unusedFogPrefabEntityPool.IsEmpty())
            {
                _unusedFogPrefabEntityPool.AddRange((IEnumerable<GameEntity>)AccessTools.Method(typeof(MapWeatherVisualManager), "CreateNewWeatherPrefabPoolElements").Invoke(MapWeatherVisualManager.Current, new object[] { "campaign_fog_prefab", 5 }));
            }

            gameEntity = _unusedFogPrefabEntityPool[0];

            _unusedFogPrefabEntityPool.Remove(gameEntity);

            return gameEntity;
        }

        public void ReleaseDustPrefab(GameEntity prefab)
        {
            _unusedDustPrefabEntityPool.Add(prefab);

            prefab.SetVisibilityExcludeParents(false);
        }

        public void ReleaseFogPrefab(GameEntity prefab)
        {
            _unusedFogPrefabEntityPool.Add(prefab);

            prefab.SetVisibilityExcludeParents(false);
        }
    }
}
