using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.ComponentInterfaces;

namespace RealisticWeather.GameModels
{
	public class RealisticWeatherBattleMoraleModel : BattleMoraleModel
	{
		private readonly BattleMoraleModel _model;

		public RealisticWeatherBattleMoraleModel(BattleMoraleModel model) => _model = model;

		// Decrease morale according to rain density, fog density and dust.
		public override float GetEffectiveInitialMorale(Agent agent, float baseMorale) => _model.GetEffectiveInitialMorale(agent, baseMorale) * RealisticWeatherHelper.GetRainEffectOnMorale(Mission.Current.Scene.GetRainDensity()) * RealisticWeatherHelper.GetFogEffectOnMorale(Mission.Current.Scene.GetFog()) * RealisticWeatherHelper.GetDustEffectOnMorale(RealisticWeatherManager.Current.HasDust);

		public override float CalculateCasualtiesFactor(BattleSideEnum battleSide) => _model.CalculateCasualtiesFactor(battleSide);

		public override (float affectedSideMaxMoraleLoss, float affectorSideMaxMoraleGain) CalculateMaxMoraleChangeDueToAgentIncapacitated(Agent affectedAgent, AgentState affectedAgentState, Agent affectorAgent, in KillingBlow killingBlow) => _model.CalculateMaxMoraleChangeDueToAgentIncapacitated(affectedAgent, affectedAgentState, affectorAgent, killingBlow);

		public override (float affectedSideMaxMoraleLoss, float affectorSideMaxMoraleGain) CalculateMaxMoraleChangeDueToAgentPanicked(Agent agent) => _model.CalculateMaxMoraleChangeDueToAgentPanicked(agent);

		public override float CalculateMoraleChangeToCharacter(Agent agent, float maxMoraleChange) => _model.CalculateMoraleChangeToCharacter(agent, maxMoraleChange);

		public override bool CanPanicDueToMorale(Agent agent) => _model.CanPanicDueToMorale(agent);

		public override float GetAverageMorale(Formation formation) => _model.GetAverageMorale(formation);

		public override float CalculateMoraleChangeOnShipSunk(IShipOrigin shipOrigin) => _model.CalculateMoraleChangeOnShipSunk(shipOrigin);

		public override float CalculateMoraleOnRamming(Agent agent, IShipOrigin rammingShip, IShipOrigin rammedShip) => _model.CalculateMoraleOnRamming(agent, rammingShip, rammedShip);

		public override float CalculateMoraleOnShipsConnected(Agent agent, IShipOrigin ownerShip, IShipOrigin targetShip) => _model.CalculateMoraleOnShipsConnected(agent, ownerShip, targetShip);
	}
}
