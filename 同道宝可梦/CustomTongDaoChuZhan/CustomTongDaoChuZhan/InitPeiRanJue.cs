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

    
    [HarmonyPatch(typeof(CombatCharacter))]
    [HarmonyPatch("ResetTeammateCommandLeftTime")]
    public class CombatCharacterPatch
    {
        [HarmonyPrefix]
        public static bool Prefix(CombatCharacter __instance, DataContext context)
        {
            // 检查是否是同道出战命令
            var implement = __instance.ExecutingTeammateCommandConfig.Implement;
            bool isCompanionCommand = implement == ETeammateCommandImplement.Fight || 
                                      implement == ETeammateCommandImplement.StopEnemy;
        
            if (isCompanionCommand)
            {
                // 设置最大short值
                __instance.TeammateCommandLeftFrame = short.MaxValue; // 32767
                __instance.TeammateCommandTotalFrame = short.MaxValue;
            
                // 保持百分比显示为100%
                __instance.SetTeammateCommandTimePercent(100, context);
            
                // 跳过原始方法
                return false;
            }
        
            // 对于其他命令，执行原始逻辑
            return true;
        }
    }
    
}