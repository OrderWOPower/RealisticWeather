using HarmonyLib;
using SandBox.GameComponents;
using System.Collections.Generic;
using System.Reflection;
using TaleWorlds.Engine;
using TaleWorlds.MountAndBlade;

namespace RealisticWeather.GameModels
{
    [HarmonyPatch]
    public class RealisticWeatherAgentStatCalculateModel
    {
        private static IEnumerable<MethodBase> TargetMethods()
        {
            yield return AccessTools.Method(typeof(CustomBattleAgentStatCalculateModel), "UpdateAgentStats");
            yield return AccessTools.Method(typeof(SandboxAgentStatCalculateModel), "UpdateAgentStats");
        }

        public static void Postfix(Agent agent, AgentDrivenProperties agentDrivenProperties)
        {
            Scene scene = Mission.Current.Scene;
            RealisticWeatherHelper.SetWeatherEffectsOnAgent(agent, agentDrivenProperties, scene.GetRainDensity(), scene.GetFog(), RealisticWeatherManager.Current.HasDust);
        }
    }
}
