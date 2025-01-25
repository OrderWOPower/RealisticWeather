using System.Collections.Generic;
using TaleWorlds.Library;

namespace RealisticWeather
{
    public class RealisticWeatherManager
    {
        private static readonly RealisticWeatherManager _realisticWeatherManager = new RealisticWeatherManager();

        public static RealisticWeatherManager Current => _realisticWeatherManager;

        public bool HasDust { get; set; }

        public List<Vec3> WeatherEventPositions { get; set; }

        public RealisticWeatherManager() => WeatherEventPositions = new List<Vec3>();

        public void SetDust(bool hasDust) => HasDust = hasDust;

        public void SetWeatherEventPositions(List<Vec3> weatherEventPositions) => WeatherEventPositions = weatherEventPositions;
    }
}
