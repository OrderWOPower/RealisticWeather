using HarmonyLib;
using SandBox.GameComponents;
using System.Collections.Generic;
using System.Reflection;
using TaleWorlds.MountAndBlade;

namespace RealisticWeather.GameModels
{
    [HarmonyPatch]
    public class RealisticWeatherAgentStatCalculateModel
    {
        private static IEnumerable<MethodBase> TargetMethods()
        {
            yield return AccessTools.Method(typeof(CustomBattleAgentStatCalculateModel), "UpdateHumanStats");
            yield return AccessTools.Method(typeof(SandboxAgentStatCalculateModel), "UpdateHumanStats");
            yield return AccessTools.Method(typeof(CustomBattleAgentStatCalculateModel), "UpdateHorseStats");
            yield return AccessTools.Method(typeof(SandboxAgentStatCalculateModel), "UpdateHorseStats");
        }

        [HarmonyPriority(Priority.Last)]
        private static void Postfix(Agent agent, AgentDrivenProperties agentDrivenProperties) => RealisticWeatherHelper.SetWeatherEffectsOnAgent(agent, agentDrivenProperties, Mission.Current.Scene.GetRainDensity(), Mission.Current.Scene.GetFog(), RealisticWeatherManager.Current.HasDust);
    }
}
