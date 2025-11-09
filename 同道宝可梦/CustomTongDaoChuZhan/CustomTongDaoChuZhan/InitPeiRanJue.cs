using System.Collections;
using System.Reflection;
using Config.EventConfig;
using GameData.Combat.Math;
using GameData.Common;
using GameData.Domains;
using GameData.Domains.Character;
using GameData.Domains.Item;
using GameData.Domains.Mod;
using GameData.Domains.SpecialEffect;
using GameData.Domains.SpecialEffect.CombatSkill.NoSect.Neigong;
using GameData.Domains.TaiwuEvent;
using GameData.Domains.TaiwuEvent.EventHelper;
using GameData.Domains.TaiwuEvent.EventManager;
using GameData.GameDataBridge;
using GameData.Utilities;
using HarmonyLib;
using HarmonyLib.Public.Patching;
using TaiwuModdingLib.Core.Plugin;
using GameData.Domains.CombatSkill;
using System.Collections.Generic;
using System.Linq;
using Config;
using GameData.Domains.Combat;
using TaiwuModdingLib.Core.Utils;
using Character = GameData.Domains.Character.Character;
using CombatSkill = GameData.Domains.CombatSkill.CombatSkill;

namespace CustomTongDaoChuZhan;

[PluginConfig("CustomTongDaoChuZhan", "超级马桶", "0.1")]
public class InitTongDaoChuZhan : TaiwuRemakeHarmonyPlugin
{
    Harmony harm;

    public override void Dispose()
    {
        if (this.harm != null)
        {
            this.harm.UnpatchSelf();
            AdaptableLog.Info("Harmony 补丁已卸载");
        }
    }

    public override void Initialize()
    {
        AdaptableLog.Info("=== 开始 初始化宝可梦 Mod ===");
        this.harm = new Harmony("Custom_TongDaoChuZhan");

        try
        {
            harm.PatchAll();
            AdaptableLog.Info("Harmony 同道补丁完成");
        }
        catch (Exception e)
        {
            AdaptableLog.Error($"Harmony 补丁失败: {e}");
        }

        AdaptableLog.Info("=== 同 Mod 初始化完成 ===");
    }


    [HarmonyPatch]
    public class CombatCharacterPatch
    {
        [HarmonyPatch(typeof(CombatCharacter), "ReduceTeammateCommandLeftTime")]
        [HarmonyPrefix]
        public static bool ReduceTeammateCommandLeftTime_Prefix(CombatCharacter __instance, DataContext context)
        {


            try
            {
                ETeammateCommandImplement implement = __instance.ExecutingTeammateCommandConfig.Implement;
                bool flag2 = implement == ETeammateCommandImplement.Fight ;
                if (flag2)
                {
                    __instance.TeammateCommandLeftFrame = short.MaxValue; // 32767
                    __instance.TeammateCommandTotalFrame = short.MaxValue;
            
                    // 保持百分比显示为100%
                    __instance.SetTeammateCommandTimePercent(100, context);
            
                    AdaptableLog.Info("ReduceTeammateCommandLeftTime被拦截，设置最大帧数");
                }

            }
            catch (Exception ex)
            {
                AdaptableLog.Error($"ReduceTeammateCommandLeftTime补丁错误: {ex}");
            }
            
            return true;
        }
    }

    [HarmonyPatch(typeof(CombatDomain))]
    public class CombatDomainPatch
    {
        [HarmonyPatch("StartCombat")]
        [HarmonyPostfix]
        public static void StartCombat_Postfix(CombatDomain __instance, DataContext context, bool __result)
        {
            try
            {
                AdaptableLog.Info("=== 战斗开始，开始初始化同道CD ===");

                // 如果战斗启动失败，直接返回
                if (!__result) return;

                // 获取所有友方角色（包括主控角色和同道）
                var allyCharacters = __instance.GetCharacters(true);

                foreach (CombatCharacter character in allyCharacters)
                {
                    // 跳过主控角色，只处理同道
                    if (__instance.IsMainCharacter(character))
                    {
                        AdaptableLog.Info($"跳过主控角色: {character.GetId()}");
                        continue;
                    }

                    AdaptableLog.Info($"处理同道角色: {character.GetId()}");

                    // 初始化同道指令CD
                    InitializeTeammateCommandCD(character, context);
                }

                AdaptableLog.Info("=== 同道CD初始化完成 ===");
            }
            catch (Exception ex)
            {
                AdaptableLog.Error($"战斗开始时初始化同道CD失败: {ex}");
            }
        }

        private static void InitializeTeammateCommandCD(CombatCharacter teammate, DataContext context)
        {
            try
            {
                // 获取同道指令列表
                List<sbyte> cmdList = teammate.GetCurrTeammateCommands();
                List<byte> cdPercentList = teammate.GetTeammateCommandCdPercent();

                if (cmdList == null || cdPercentList == null)
                {
                    AdaptableLog.Warning($"同道 {teammate.GetId()} 的指令列表为空");
                    return;
                }

                AdaptableLog.Info($"同道 {teammate.GetId()} 有 {cmdList.Count} 个指令");

                // 重置所有指令CD
                for (int i = 0; i < cmdList.Count; i++)
                {
                    if (cmdList[i] >= 0) // 有效指令
                    {
                        // 设置CD计数为0
                        teammate.TeammateCommandCdCurrentCount[i] = 0;
                        teammate.TeammateCommandCdTotalCount[i] = 0;

                        // 设置CD百分比为0（立即可用）
                        cdPercentList[i] = 0;

                        AdaptableLog.Info($"指令 {i} (类型: {cmdList[i]}) CD已重置为0");
                    }
                }

                // 更新CD百分比到角色
                teammate.SetTeammateCommandCdPercent(cdPercentList, context);

                // 更新指令可用性
                for (int i = 0; i < cmdList.Count; i++)
                {
                    if (cmdList[i] >= 0)
                    {
                        DomainManager.Combat.UpdateTeammateCommandUsable(context, teammate, cmdList[i]);
                    }
                }

                AdaptableLog.Info($"同道 {teammate.GetId()} 的所有指令CD已初始化完成");
            }
            catch (Exception ex)
            {
                AdaptableLog.Error($"初始化同道 {teammate.GetId()} CD失败: {ex}");
            }
        }
    }
}