using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.MountAndBlade;

namespace RealisticWeather.GameModels
{
    public class RealisticWeatherAgentStatCalculateModel : AgentStatCalculateModel
    {
        private readonly AgentStatCalculateModel _model;

        public RealisticWeatherAgentStatCalculateModel(AgentStatCalculateModel model) => _model = model;

        public override void UpdateAgentStats(Agent agent, AgentDrivenProperties agentDrivenProperties)
        {
            Scene scene = Mission.Current.Scene;
            _model.UpdateAgentStats(agent, agentDrivenProperties);
            RealisticWeatherHelper.SetWeatherEffectsOnAgent(agent, agentDrivenProperties, scene.GetRainDensity(), scene.GetFog(), RealisticWeatherManager.Current.HasDust);
        }

        public override bool CanAgentRideMount(Agent agent, Agent targetMount) => _model.CanAgentRideMount(agent, targetMount);

        public override float GetDifficultyModifier() => _model.GetDifficultyModifier();

        public override float GetDismountResistance(Agent agent) => _model.GetDismountResistance(agent);

        public override float GetKnockBackResistance(Agent agent) => _model.GetKnockBackResistance(agent);

        public override float GetKnockDownResistance(Agent agent, StrikeType strikeType = StrikeType.Invalid) => _model.GetKnockDownResistance(agent, strikeType);

        public override float GetWeaponDamageMultiplier(BasicCharacterObject agentCharacter, IAgentOriginBase agentOrigin, Formation agentFormation, WeaponComponentData weapon) => _model.GetWeaponDamageMultiplier(agentCharacter, agentOrigin, agentFormation, weapon);

        public override void InitializeAgentStats(Agent agent, Equipment spawnEquipment, AgentDrivenProperties agentDrivenProperties, AgentBuildData agentBuildData) => _model.InitializeAgentStats(agent, spawnEquipment, agentDrivenProperties, agentBuildData);
    }
}
