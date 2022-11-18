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
                base.UpdateAgentStats(agent, agentDrivenProperties);
                Scene scene = Mission.Current.Scene;
                RealisticWeatherHelper.SetWeatherEffectsOnAgent(agent, agentDrivenProperties, scene.GetRainDensity(), scene.GetFog(), RealisticWeatherMission.HasDust);
            }
        }

        public class SandboxModel : SandboxAgentStatCalculateModel
        {
            public override void UpdateAgentStats(Agent agent, AgentDrivenProperties agentDrivenProperties)
            {
                base.UpdateAgentStats(agent, agentDrivenProperties);
                Scene scene = Mission.Current.Scene;
                RealisticWeatherHelper.SetWeatherEffectsOnAgent(agent, agentDrivenProperties, scene.GetRainDensity(), scene.GetFog(), RealisticWeatherMission.HasDust);
            }
        }
    }
}
