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
        // 前缀补丁 - 在原始方法执行前运行
        [HarmonyPrefix]
        // public static bool Prefix(CombatCharacter __instance)
        // {
        //     // 你的修改代码
        //     // 返回 true 继续执行原始方法，false 跳过原始方法
        //     return true;
        // }

        // 后缀补丁 - 在原始方法执行后运行
        [HarmonyPostfix]
        public static void Postfix(CombatCharacter __instance)
        {
            // 设置一个极大的帧数，相当于无限制
            __instance.TeammateCommandLeftFrame = (short)32767;
            __instance.TeammateCommandTotalFrame = 32767;

            var dataContext = __instance.GetDataContext();
            // 保持百分比显示为100%
            __instance.SetTeammateCommandTimePercent(100, dataContext);
        }
    }
    
}