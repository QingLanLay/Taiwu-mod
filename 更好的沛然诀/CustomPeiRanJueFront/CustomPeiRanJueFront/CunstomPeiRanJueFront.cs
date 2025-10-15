using System;
using System.Reflection;
using Config;
using GameData.Utilities;
using HarmonyLib;
using TaiwuModdingLib.Core.Plugin;
using TaiwuModdingLib.Core.Utils;

namespace CustomPeiRanJueFront
{
    [PluginConfig("CustomPeiRanJue", "超级马桶", "0.1")]
    public class CunstomPeiRanJueFront : TaiwuRemakeHarmonyPlugin
    {
        private Harmony harm;

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
            AdaptableLog.Info("=== 开始初始化新版沛然诀前端Mod ===");
            this.harm = new Harmony("Custom_PeiRanJue_Front");

            try
            {
                Custom_PeiRanJue_DirectEffectID();
                Custom_PeiRanJue_ReverseEffectID();
                AdaptableLog.Info("沛然诀强化前端完成!");
            }
            catch (Exception e)
            {
                AdaptableLog.Error("Harmony 补丁失败: " + e.ToString());
            }

            AdaptableLog.Info("=== 新版沛然诀前端 Mod 初始化完成 ===");
        }

        public static void Custom_PeiRanJue_DirectEffectID()
        {
            try
            {
                // 1. 直接获取目标 SpecialEffectItem 实例
                var combatSkillInstance = Config.CombatSkill.Instance;
                var combatSkillItem = combatSkillInstance["沛然诀"];
                var directEffectID = combatSkillItem.DirectEffectID;
                var targetEffect = SpecialEffect.Instance[directEffectID];

                ModifyField_PeiRanJue(targetEffect);
                AdaptableLog.Info("沛然诀正练修改成功");
                // 验证修改是否真的生效
                VerifyModification(targetEffect);
            }
            catch (Exception ex)
            {
                AdaptableLog.Info("修改失败: " + ex.Message);
                AdaptableLog.Info("堆栈跟踪: " + ex.StackTrace);
            }
        }

        public static void Custom_PeiRanJue_ReverseEffectID()
        {
            try
            {
                // 1. 直接获取目标 SpecialEffectItem 实例
                var combatSkillInstance = Config.CombatSkill.Instance;
                var combatSkillItem = combatSkillInstance["沛然诀"];
                var reverseEffectID = combatSkillItem.ReverseEffectID;
                var targetEffect = SpecialEffect.Instance[reverseEffectID];

                ModifyField_PeiRanJue(targetEffect);
                AdaptableLog.Info("沛然诀逆练修改成功");

                // 验证修改是否真的生效
                VerifyModification(targetEffect);
            }
            catch (Exception ex)
            {
                AdaptableLog.Info("修改失败: " + ex.Message);
                AdaptableLog.Info("堆栈跟踪: " + ex.StackTrace);
            }
        }

        private static void ModifyField_PeiRanJue(SpecialEffectItem targetEffect)
        {
            string newDesc = "运用者受到的所有功法反噬伤害大幅降低；运用者六维属性越高，运用者所有功法的威力就提高越多。";
            string[] newDescArray = new string[] { newDesc };

            ReflectionExtensions.ModifyField<SpecialEffectItem>(
                targetEffect,
                "Desc",
                newDescArray
            );

            ReflectionExtensions.ModifyField<SpecialEffectItem>(
                targetEffect,
                "ShortDesc",
                newDescArray
            );

            ReflectionExtensions.ModifyField<SpecialEffectItem>(
                targetEffect,
                "PlayerCastBossSkillDesc",
                newDescArray
            );

            ReflectionExtensions.ModifyField<SpecialEffectItem>(
                targetEffect,
                "EffectActiveType",
                (sbyte)2,
                BindingFlags.Instance | BindingFlags.Public
            );
        }

        private static void VerifyModification(SpecialEffectItem effect)
        {
            try
            {
                // 使用内置方法获取字段值进行验证
                var descValue = ReflectionExtensions.GetFieldValue<SpecialEffectItem>(
                    effect,
                    "Desc",
                    BindingFlags.Instance | BindingFlags.Public
                ) as string[];

                var effectTypeValue = ReflectionExtensions.GetFieldValue<SpecialEffectItem>(
                    effect,
                    "EffectActiveType",
                    BindingFlags.Instance | BindingFlags.Public
                );

                if (descValue != null && descValue.Length > 0)
                {
                    AdaptableLog.Info("验证 - Desc: " + descValue[0]);
                }

                AdaptableLog.Info("验证 - EffectActiveType: " + effectTypeValue.ToString());
            }
            catch (Exception ex)
            {
                AdaptableLog.Info("验证失败: " + ex.Message);
            }
        }
    }
}