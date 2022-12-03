namespace RealisticWeather
{
    public class RealisticWeatherManager
    {
        private static readonly RealisticWeatherManager _realisticWeatherManager = new RealisticWeatherManager();

        public static RealisticWeatherManager Current => _realisticWeatherManager;

        public float FogDensity { get; set; }

        public bool HasDust { get; set; }

        public void SetFogDensity(float fogDensity) => FogDensity = fogDensity;

        public void SetDust(bool hasDust) => HasDust = hasDust;
    }
}
