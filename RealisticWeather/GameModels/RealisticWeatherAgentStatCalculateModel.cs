using HarmonyLib;
using SandBox.GameComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using TaleWorlds.MountAndBlade;

namespace RealisticWeather.GameModels
{
    [HarmonyPatch]
    public class RealisticWeatherAgentStatCalculateModel
    {
        private static IEnumerable<MethodBase> TargetMethods()
        {
            yield return AccessTools.Method(typeof(CustomBattleAgentStatCalculateModel), "UpdateHumanStats");
            yield return AccessTools.Method(typeof(CustomBattleAgentStatCalculateModel), "UpdateHorseStats");
            yield return AccessTools.Method(typeof(SandboxAgentStatCalculateModel), "UpdateHumanStats");
            yield return AccessTools.Method(typeof(SandboxAgentStatCalculateModel), "UpdateHorseStats");
        }

        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> codes = instructions.ToList();
            CodeInstruction code = null;
            int index = 0, startIndex = 0, endIndex = 0;

            for (int i = 0; i < codes.Count; i++)
            {
                if (codes[i].operand is MethodInfo method && method == AccessTools.Method(typeof(AgentStatCalculateModel), "GetEnvironmentSpeedFactor"))
                {
                    startIndex = i - 2;
                    endIndex = i;
                    index = i + 1;
                }
            }

            // Remove the vanilla rain effect on movement speed.
            codes.Insert(index, new CodeInstruction(OpCodes.Ldc_R4, 1f));
            codes.RemoveRange(startIndex, endIndex - startIndex + 1);

            for (int i = 0; i < codes.Count; i++)
            {
                if (codes[i].operand is MethodInfo method && method == AccessTools.PropertyGetter(typeof(AgentDrivenProperties), "AiShooterError"))
                {
                    codes[i - 2].opcode = OpCodes.Nop;
                    code = codes[i];
                    startIndex = i - 1;
                    endIndex = i + 3;
                }
            }

            if (code != null)
            {
                // Remove the vanilla rain and fog effects on shooter error.
                codes.RemoveRange(startIndex, endIndex - startIndex + 1);
            }

            return codes;
        }

        [HarmonyAfter(new string[] { "com.rbmcombat" })]
        private static void Postfix(Agent agent, AgentDrivenProperties agentDrivenProperties) => RealisticWeatherHelper.ApplyWeatherEffectsOnAgent(agent, agentDrivenProperties, Mission.Current.Scene.GetRainDensity(), Mission.Current.Scene.GetFog(), RealisticWeatherManager.Current.HasDust);

        private static Exception Finalizer() => null;
    }
}
