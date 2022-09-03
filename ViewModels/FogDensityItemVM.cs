using TaleWorlds.Core.ViewModelCollection.Selector;

namespace RealisticWeather.ViewModels
{
    public class FogDensityItemVM : SelectorItemVM
    {
        public float FogDensity { get; set; }

        public FogDensityItemVM(string fogDensityName, float fogDensity) : base(fogDensityName) => FogDensity = fogDensity;
    }
}
