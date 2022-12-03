using SandBox.GameComponents;
using TaleWorlds.MountAndBlade;

namespace RealisticWeather.GameModels
{
    public class RealisticWeatherAgentStatCalculateModel
    {
        public class CustomBattleModel : CustomBattleAgentStatCalculateModel
        {
            public override void UpdateAgentStats(Agent agent, AgentDrivenProperties agentDrivenProperties)
            {
                base.UpdateAgentStats(agent, agentDrivenProperties);
                RealisticWeatherHelper.SetWeatherEffectsOnAgent(agent, agentDrivenProperties, Mission.Current.Scene.GetRainDensity(), RealisticWeatherManager.Current.FogDensity, RealisticWeatherManager.Current.HasDust);
            }
        }

        public class SandboxModel : SandboxAgentStatCalculateModel
        {
            public override void UpdateAgentStats(Agent agent, AgentDrivenProperties agentDrivenProperties)
            {
                base.UpdateAgentStats(agent, agentDrivenProperties);
                RealisticWeatherHelper.SetWeatherEffectsOnAgent(agent, agentDrivenProperties, Mission.Current.Scene.GetRainDensity(), RealisticWeatherManager.Current.FogDensity, RealisticWeatherManager.Current.HasDust);
            }
        }
    }
}
