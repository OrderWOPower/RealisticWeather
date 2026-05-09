using System;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
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
			ExplainedNumber result;
			Vec3 weatherEventPosition = RealisticWeatherManager.Current.WeatherEventPositions.FirstOrDefault(p => p.AsVec2.DistanceSquared(party.GetPosition2D) <= 400f);
			MapWeatherModel.WeatherEvent weatherEventInPosition = Campaign.Current.Models.MapWeatherModel.GetWeatherEventInPosition(party.GetPosition2D);

			try
			{
				result = _model.GetPartySpottingRange(party, includeDescriptions);
			}
			catch (Exception ex)
			{
				result = new ExplainedNumber(GetPartySeeingRangeBase(party), includeDescriptions, null);
				InformationManager.DisplayMessage(new InformationMessage(ex.ToString()));
			}

			// Decrease party visibility if the party is in light or heavy rain/snow.
			if (weatherEventInPosition == MapWeatherModel.WeatherEvent.LightRain || weatherEventInPosition == MapWeatherModel.WeatherEvent.Snowy)
			{
				result.AddFactor(-0.2f, _lightRainText);
			}
			else if (weatherEventInPosition == MapWeatherModel.WeatherEvent.HeavyRain || weatherEventInPosition == MapWeatherModel.WeatherEvent.Blizzard)
			{
				result.AddFactor(-0.3f, _heavyRainText);
			}
			else if (weatherEventInPosition == MapWeatherModel.WeatherEvent.Storm && !party.IsCurrentlyAtSea)
			{
				result.AddFactor(-0.4f, new TextObject("{=M6V6eCTg}Storm", null));
			}

			// Decrease party visibility if the party is inside a dust storm or fog bank.
			if (weatherEventPosition.z == 1)
			{
				result.AddFactor(-0.75f, _dustText);
			}
			else if (weatherEventPosition.z == 2)
			{
				result.AddFactor(-0.5f, _fogText);
			}

			return result;
		}

		public override float GetHideoutSpottingDistance() => _model.GetHideoutSpottingDistance();

		public override float MaximumSeeingRange() => _model.MaximumSeeingRange();

		public override float GetPartySeeingRangeBase(MobileParty party) => _model.GetPartySeeingRangeBase(party);

		public override float GetPartySpottingRatioForMainPartySeeingRange(MobileParty party) => _model.GetPartySpottingRatioForMainPartySeeingRange(party);
	}
}
