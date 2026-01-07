using System.Collections.Generic;
using TaleWorlds.Library;

namespace RealisticWeather
{
    public class RealisticWeatherManager
    {
        private static readonly RealisticWeatherManager _realisticWeatherManager = new RealisticWeatherManager();

        public static RealisticWeatherManager Current => _realisticWeatherManager;

        public float RainDensity { get; set; }

        public float FogDensity { get; set; }

        public bool HasDust { get; set; }

        public List<Vec3> WeatherEventPositions { get; set; }

        public RealisticWeatherManager() => WeatherEventPositions = new List<Vec3>();

        public void SetRainDensity(float rainDensity) => RainDensity = rainDensity;

        public void SetFogDensity(float fogDensity) => FogDensity = fogDensity;

        public void SetDust(bool hasDust) => HasDust = hasDust;

        public void SetWeatherEventPositions(List<Vec3> weatherEventPositions) => WeatherEventPositions = weatherEventPositions;
    }
}
