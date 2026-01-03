//using Bannerlord.UIExtenderEx.Attributes;
//using Bannerlord.UIExtenderEx.ViewModels;
//using NavalDLC.CustomBattle.CustomBattle;
using RealisticWeather.ViewModels;
using System;
using System.Collections.Generic;
using TaleWorlds.Core.ViewModelCollection.Selector;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace RealisticWeather
{
    //[ViewModelMixin("RefreshValues")]
    public class RealisticWeatherNavalMixin// : BaseViewModelMixin<NavalCustomBattleMapSelectionGroupVM>
    {
        private SelectorVM<RainDensityItemVM> _rainDensitySelection;
        private SelectorVM<FogDensityItemVM> _fogDensitySelection;
        private string _rainDensityText, _fogDensityText;

        public static WeakReference<RealisticWeatherNavalMixin> MixinWeakReference { get; set; }

        public float SelectedRainDensity { get; set; }

        public float SelectedFogDensity { get; set; }

        public IEnumerable<(string, float)> RainDensities
        {
            get
            {
                yield return (new TextObject("{=RealisticWeather01}None").ToString(), 0f);
                yield return (new TextObject("{=RealisticWeather02}Light").ToString(), 0.701f);
                yield return (new TextObject("{=RealisticWeather03}Moderate").ToString(), 0.85f);
                yield return (new TextObject("{=RealisticWeather04}Heavy").ToString(), 1f);
            }
        }

        public IEnumerable<(string, float)> FogDensities
        {
            get
            {
                yield return (new TextObject("{=RealisticWeather01}None").ToString(), 1);
                yield return (new TextObject("{=RealisticWeather02}Light").ToString(), 4);
                yield return (new TextObject("{=RealisticWeather03}Moderate").ToString(), 8);
                yield return (new TextObject("{=RealisticWeather04}Heavy").ToString(), 16);
                yield return (new TextObject("{=RealisticWeather05}Very Heavy").ToString(), 32);
                yield return (new TextObject("{=RealisticWeather06}Dust Storm (Special)").ToString(), 0);
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

                    //ViewModel?.OnPropertyChangedWithValue(value, "RainDensity");
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

                    //ViewModel?.OnPropertyChangedWithValue(value, "FogDensity");
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

                    //ViewModel?.OnPropertyChangedWithValue(value, "RainDensityText");
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

                    //ViewModel?.OnPropertyChangedWithValue(value, "FogDensityText");
                }
            }
        }

        public RealisticWeatherNavalMixin(/*NavalCustomBattleMapSelectionGroupVM mapSelectionGroupVM*/)// : base(mapSelectionGroupVM)
        {
            MixinWeakReference = new WeakReference<RealisticWeatherNavalMixin>(this);
            RainDensitySelection = new SelectorVM<RainDensityItemVM>(0, new Action<SelectorVM<RainDensityItemVM>>(OnRainDensitySelection));
            FogDensitySelection = new SelectorVM<FogDensityItemVM>(0, new Action<SelectorVM<FogDensityItemVM>>(OnFogDensitySelection));
        }

        /*public override void OnRefresh()
        {
            RainDensityText = new TextObject("{=RealisticWeather07}Rain/Snow Density").ToString();
            FogDensityText = new TextObject("{=RealisticWeather08}Fog Density").ToString();
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
        }*/

        private void OnRainDensitySelection(SelectorVM<RainDensityItemVM> selector) => SelectedRainDensity = selector.SelectedItem.RainDensity;

        private void OnFogDensitySelection(SelectorVM<FogDensityItemVM> selector) => SelectedFogDensity = selector.SelectedItem.FogDensity;
    }
}
