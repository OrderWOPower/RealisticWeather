using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.ViewModels;
using RealisticWeather.ViewModels;
using System;
using System.Collections.Generic;
using TaleWorlds.Core.ViewModelCollection.Selector;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.CustomBattle.CustomBattle;

namespace RealisticWeather
{
    [ViewModelMixin("RefreshValues")]
    public class RealisticWeatherMixin : BaseViewModelMixin<MapSelectionGroupVM>
    {
        private SelectorVM<RainDensityItemVM> _rainDensitySelection;
        private SelectorVM<FogDensityItemVM> _fogDensitySelection;
        private string _rainDensityText;
        private string _fogDensityText;

        public static WeakReference<RealisticWeatherMixin> MixinWeakReference { get; set; }

        public float SelectedRainDensity { get; set; }

        public float SelectedFogDensity { get; set; }

        public IEnumerable<(string, float)> RainDensities
        {
            get
            {
                yield return ("None", 0f);
                yield return ("Light", 0.25f);
                yield return ("Moderate", 0.5f);
                yield return ("Heavy", 0.75f);
                yield return ("Very Heavy", 1f);
            }
        }

        public IEnumerable<(string, float)> FogDensities
        {
            get
            {
                yield return ("None", 1);
                yield return ("Light", 8);
                yield return ("Moderate", 16);
                yield return ("Heavy", 32);
                yield return ("Very Heavy", 64);
                yield return ("Dust Storm (Special)", 0);
            }
        }

        [DataSourceProperty]
        public SelectorVM<RainDensityItemVM> RainDensitySelection
        {
            get => _rainDensitySelection;
            set
            {
                if (value != _rainDensitySelection)
                {
                    _rainDensitySelection = value;
                    ViewModel?.OnPropertyChangedWithValue(value, "RainDensity");
                }
            }
        }

        [DataSourceProperty]
        public SelectorVM<FogDensityItemVM> FogDensitySelection
        {
            get => _fogDensitySelection;
            set
            {
                if (value != _fogDensitySelection)
                {
                    _fogDensitySelection = value;
                    ViewModel?.OnPropertyChangedWithValue(value, "FogDensity");
                }
            }
        }

        [DataSourceProperty]
        public string RainDensityText
        {
            get => _rainDensityText;
            set
            {
                if (value != _rainDensityText)
                {
                    _rainDensityText = value;
                    ViewModel?.OnPropertyChangedWithValue(value, "RainDensityText");
                }
            }
        }

        [DataSourceProperty]
        public string FogDensityText
        {
            get => _fogDensityText;
            set
            {
                if (value != _fogDensityText)
                {
                    _fogDensityText = value;
                    ViewModel?.OnPropertyChangedWithValue(value, "FogDensityText");
                }
            }
        }

        public RealisticWeatherMixin(MapSelectionGroupVM mapSelectionGroupVM) : base(mapSelectionGroupVM)
        {
            MixinWeakReference = new WeakReference<RealisticWeatherMixin>(this);
            RainDensitySelection = new SelectorVM<RainDensityItemVM>(0, new Action<SelectorVM<RainDensityItemVM>>(OnRainDensitySelection));
            FogDensitySelection = new SelectorVM<FogDensityItemVM>(0, new Action<SelectorVM<FogDensityItemVM>>(OnFogDensitySelection));
        }

        public override void OnRefresh()
        {
            RainDensityText = "Rain/Snow Density";
            FogDensityText = "Fog Density";
            RainDensitySelection.ItemList.Clear();
            FogDensitySelection.ItemList.Clear();
            foreach ((string, float) rainDensity in RainDensities)
            {
                RainDensitySelection.AddItem(new RainDensityItemVM(rainDensity.Item1, rainDensity.Item2));
            }
            foreach ((string, float) fogDensity in FogDensities)
            {
                FogDensitySelection.AddItem(new FogDensityItemVM(fogDensity.Item1, fogDensity.Item2));
            }
            RainDensitySelection.SelectedIndex = 0;
            FogDensitySelection.SelectedIndex = 0;
        }

        private void OnRainDensitySelection(SelectorVM<RainDensityItemVM> selector) => SelectedRainDensity = selector.SelectedItem.RainDensity;

        private void OnFogDensitySelection(SelectorVM<FogDensityItemVM> selector) => SelectedFogDensity = selector.SelectedItem.FogDensity;
    }
}
