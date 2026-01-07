using TaleWorlds.Core.ViewModelCollection.Selector;

namespace RealisticWeather.ViewModels
{
    public class RainDensityItemVM : SelectorItemVM
    {
        public float RainDensity { get; set; }

        public RainDensityItemVM(string rainDensityName, float rainDensity) : base(rainDensityName) => RainDensity = rainDensity;
    }
}
