namespace RealisticWeather
{
    public class RealisticWeatherManager
    {
        private static readonly RealisticWeatherManager _realisticWeatherManager = new RealisticWeatherManager();

        public static RealisticWeatherManager Current => _realisticWeatherManager;

        public bool HasDust { get; set; }

        public void SetDust(bool hasDust) => HasDust = hasDust;
    }
}
