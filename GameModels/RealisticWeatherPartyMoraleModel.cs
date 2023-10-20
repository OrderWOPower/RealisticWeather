using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace RealisticWeather.GameModels
{
    public class RealisticWeatherPartyMoraleModel : PartyMoraleModel
    {
        private static readonly TextObject _lightRainText = new TextObject("{=RealisticWeather51}Light rain/snow", null), _heavyRainText = new TextObject("{=RealisticWeather52}Heavy rain/snow", null), _dustText = new TextObject("{=RealisticWeather53}Dust storm", null), _fogText = new TextObject("{=RealisticWeather19}Fog", null);

        private readonly PartyMoraleModel _model;

        public RealisticWeatherPartyMoraleModel(PartyMoraleModel model) => _model = model;

        public override float HighMoraleValue => _model.HighMoraleValue;

        public override ExplainedNumber GetEffectivePartyMorale(MobileParty party, bool includeDescription = false)
        {
            ExplainedNumber result = _model.GetEffectivePartyMorale(party, includeDescription);
            Vec3 prefabPosition = RealisticWeatherManager.Current.PrefabPositions.FirstOrDefault(p => p.AsVec2.Distance(party.Position2D) <= 25f);
            MapWeatherModel.WeatherEvent weatherEventInPosition = Campaign.Current.Models.MapWeatherModel.GetWeatherEventInPosition(party.Position2D);

            // Decrease party morale if the party is in light or heavy rain/snow.
            if (weatherEventInPosition == MapWeatherModel.WeatherEvent.LightRain || weatherEventInPosition == MapWeatherModel.WeatherEvent.Snowy)
            {
                result.Add(-5f, _lightRainText);
            }
            else if (weatherEventInPosition == MapWeatherModel.WeatherEvent.HeavyRain || weatherEventInPosition == MapWeatherModel.WeatherEvent.Blizzard)
            {
                result.Add(-10f, _heavyRainText);
            }

            // Decrease party morale if the party is inside a dust storm or fog bank.
            if (prefabPosition.z == 1)
            {
                result.Add(-10f, _dustText);
            }
            else if (prefabPosition.z == 2)
            {
                result.Add(-5f, _fogText);
            }

            return result;
        }

        public override int GetDailyNoWageMoralePenalty(MobileParty party) => _model.GetDailyNoWageMoralePenalty(party);

        public override int GetDailyStarvationMoralePenalty(PartyBase party) => _model.GetDailyStarvationMoralePenalty(party);

        public override float GetDefeatMoraleChange(PartyBase party) => _model.GetDefeatMoraleChange(party);

        public override float GetStandardBaseMorale(PartyBase party) => _model.GetStandardBaseMorale(party);

        public override float GetVictoryMoraleChange(PartyBase party) => _model.GetVictoryMoraleChange(party);
    }
}
