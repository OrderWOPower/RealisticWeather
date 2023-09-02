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
        private static readonly TextObject _dustText = new TextObject("{=RealisticWeather53}Dust storm", null);
        private static readonly TextObject _fogText = new TextObject("{=RealisticWeather19}Fog", null);

        private readonly MapVisibilityModel _model;

        public RealisticWeatherMapVisibilityModel(MapVisibilityModel model) => _model = model;

        public override ExplainedNumber GetPartySpottingRange(MobileParty party, bool includeDescriptions = false)
        {
            ExplainedNumber result = _model.GetPartySpottingRange(party, includeDescriptions);

            if (RealisticWeatherManager.Current.PrefabPositions != null)
            {
                // Find the position of the weather prefab within 25km of the main party.
                Vec3 prefabPosition = RealisticWeatherManager.Current.PrefabPositions.FirstOrDefault(p => p.AsVec2.Distance(MobileParty.MainParty.Position2D) <= 25f);

                // Decrease party visibility if the party is inside a dust storm or fog bank.
                if (prefabPosition.z == 2)
                {
                    result.AddFactor(-0.75f, _dustText);
                }
                else if (prefabPosition.z == 3)
                {
                    result.AddFactor(-0.5f, _fogText);
                }
            }

            return result;
        }

        public override float GetHideoutSpottingDistance() => _model.GetHideoutSpottingDistance();

        public override float GetPartyRelativeInspectionRange(IMapPoint party) => _model.GetPartyRelativeInspectionRange(party);

        public override float GetPartySpottingDifficulty(MobileParty spotterParty, MobileParty party) => _model.GetPartySpottingDifficulty(spotterParty, party);
    }
}
