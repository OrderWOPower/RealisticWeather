using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Map;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace RealisticWeather.GameModels
{
    public class RealisticWeatherMapVisibilityModel : MapVisibilityModel
    {
        private static readonly TextObject _lightRainText = new TextObject("{=RealisticWeather51}Light rain/snow", null), _heavyRainText = new TextObject("{=RealisticWeather52}Heavy rain/snow", null), _dustText = new TextObject("{=RealisticWeather53}Dust storm", null), _fogText = new TextObject("{=RealisticWeather19}Fog", null);

        private readonly MapVisibilityModel _model;

        public RealisticWeatherMapVisibilityModel(MapVisibilityModel model) => _model = model;

        public override ExplainedNumber GetPartySpottingRange(MobileParty party, bool includeDescriptions = false)
        {
            ExplainedNumber result = _model.GetPartySpottingRange(party, includeDescriptions);
            Vec3 prefabPosition = RealisticWeatherManager.Current.PrefabPositions.FirstOrDefault(p => p.AsVec2.Distance(party.Position2D) <= 25f);
            MapWeatherModel.WeatherEvent weatherEventInPosition = Campaign.Current.Models.MapWeatherModel.GetWeatherEventInPosition(party.Position2D);

            // Decrease party visibility if the party is in light or heavy rain/snow.
            if (weatherEventInPosition == MapWeatherModel.WeatherEvent.LightRain || weatherEventInPosition == MapWeatherModel.WeatherEvent.Snowy)
            {
                result.Add(-0.125f, _lightRainText);
            }
            else if (weatherEventInPosition == MapWeatherModel.WeatherEvent.HeavyRain || weatherEventInPosition == MapWeatherModel.WeatherEvent.Blizzard)
            {
                result.Add(-0.25f, _heavyRainText);
            }

            // Decrease party visibility if the party is inside a dust storm or fog bank.
            if (prefabPosition.z == 1)
            {
                result.AddFactor(-0.75f, _dustText);
            }
            else if (prefabPosition.z == 2)
            {
                result.AddFactor(-0.5f, _fogText);
            }

            return result;
        }

        public override float GetHideoutSpottingDistance() => _model.GetHideoutSpottingDistance();

        public override float GetPartyRelativeInspectionRange(IMapPoint party) => _model.GetPartyRelativeInspectionRange(party);

        public override float GetPartySpottingDifficulty(MobileParty spotterParty, MobileParty party) => _model.GetPartySpottingDifficulty(spotterParty, party);
    }
}
