using SandBox.GameComponents;
using TaleWorlds.Engine;
using TaleWorlds.MountAndBlade;

namespace RealisticWeather.GameModels
{
    public class RealisticWeatherAgentStatCalculateModel
    {
        public class CustomBattleModel : CustomBattleAgentStatCalculateModel
        {
            public override void UpdateAgentStats(Agent agent, AgentDrivenProperties agentDrivenProperties)
            {
                Scene scene = Mission.Current.Scene;
                base.UpdateAgentStats(agent, agentDrivenProperties);
                RealisticWeatherHelper.SetWeatherEffectsOnAgent(agent, agentDrivenProperties, scene.GetRainDensity(), scene.GetFog(), RealisticWeatherManager.Current.HasDust);
            }
        }

        public class SandboxModel : SandboxAgentStatCalculateModel
        {
            public override void UpdateAgentStats(Agent agent, AgentDrivenProperties agentDrivenProperties)
            {
                Scene scene = Mission.Current.Scene;
                base.UpdateAgentStats(agent, agentDrivenProperties);
                RealisticWeatherHelper.SetWeatherEffectsOnAgent(agent, agentDrivenProperties, scene.GetRainDensity(), scene.GetFog(), RealisticWeatherManager.Current.HasDust);
            }
        }
    }
}
