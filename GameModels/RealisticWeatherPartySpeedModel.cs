using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Localization;

namespace RealisticWeather.GameModels
{
    public class RealisticWeatherPartySpeedModel : PartySpeedModel
    {
        private static readonly TextObject _dustText = new TextObject("{=RealisticWeather53}Dust storm", null);

        private readonly PartySpeedModel _model;

        public RealisticWeatherPartySpeedModel(PartySpeedModel model) => _model = model;

        public override float BaseSpeed => _model.BaseSpeed;

        public override float MinimumSpeed => _model.MinimumSpeed;

        public override ExplainedNumber CalculateFinalSpeed(MobileParty mobileParty, ExplainedNumber finalSpeed)
        {
            finalSpeed = _model.CalculateFinalSpeed(mobileParty, finalSpeed);

            if (RealisticWeatherManager.Current.PrefabPositions.FirstOrDefault(p => p.AsVec2.Distance(mobileParty.Position2D) <= 25f).z == 1)
            {
                // Decrease party speed if the party is inside a dust storm.
                finalSpeed.AddFactor(-0.25f, _dustText);
            }

            return finalSpeed;
        }

        public override ExplainedNumber CalculateBaseSpeed(MobileParty party, bool includeDescriptions = false, int additionalTroopOnFootCount = 0, int additionalTroopOnHorseCount = 0) => _model.CalculateBaseSpeed(party, includeDescriptions, additionalTroopOnFootCount, additionalTroopOnHorseCount);
    }
}
