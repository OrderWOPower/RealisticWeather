using TaleWorlds.Core.ViewModelCollection.Selector;

namespace RealisticWeather.ViewModels
{
	public class FogDensityItemVM : SelectorItemVM
	{
		public int FogDensity { get; set; }

		public FogDensityItemVM(string fogDensityName, int fogDensity) : base(fogDensityName) => FogDensity = fogDensity;
	}
}
