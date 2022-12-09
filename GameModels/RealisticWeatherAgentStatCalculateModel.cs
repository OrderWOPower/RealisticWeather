using SandBox.GameComponents;
using TaleWorlds.Engine;
using TaleWorlds.MountAndBlade;

namespace RealisticWeather.GameModels
{
    public class RealisticWeatherAgentStatCalculateModel
    {
        public class RealisticWeatherCustomBattleAgentStatCalculateModel : CustomBattleAgentStatCalculateModel
        {
            private AgentStatCalculateModel _model;

            public RealisticWeatherCustomBattleAgentStatCalculateModel(AgentStatCalculateModel model) => _model = model;

            public override void UpdateAgentStats(Agent agent, AgentDrivenProperties agentDrivenProperties)
            {
                Scene scene = Mission.Current.Scene;
                base.UpdateAgentStats(agent, agentDrivenProperties);
                _model.UpdateAgentStats(agent, agentDrivenProperties);
                RealisticWeatherHelper.SetWeatherEffectsOnAgent(agent, agentDrivenProperties, scene.GetRainDensity(), scene.GetFog(), RealisticWeatherManager.Current.HasDust);
            }
        }

        public class RealisticWeatherSandboxAgentStatCalculateModel : SandboxAgentStatCalculateModel
        {
            private AgentStatCalculateModel _model;

            public RealisticWeatherSandboxAgentStatCalculateModel(AgentStatCalculateModel model) => _model = model;

            public override void UpdateAgentStats(Agent agent, AgentDrivenProperties agentDrivenProperties)
            {
                Scene scene = Mission.Current.Scene;
                base.UpdateAgentStats(agent, agentDrivenProperties);
                _model.UpdateAgentStats(agent, agentDrivenProperties);
                RealisticWeatherHelper.SetWeatherEffectsOnAgent(agent, agentDrivenProperties, scene.GetRainDensity(), scene.GetFog(), RealisticWeatherManager.Current.HasDust);
            }
        }
    }
}
