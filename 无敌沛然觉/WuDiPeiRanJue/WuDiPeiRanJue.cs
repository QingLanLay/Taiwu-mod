using System;
using System.Collections.Generic;
using System.Reflection;
using GameData.Combat.Math;
using GameData.Common;
using GameData.Domains.SpecialEffect;
using GameData.Domains.SpecialEffect.CombatSkill.NoSect.Neigong;
using HarmonyLib;
using TaiwuModdingLib.Core.Plugin;
using GameData.Utilities;
using GameData.Domains.CombatSkill;

namespace WuDiPeiRanJue
{
    [PluginConfig("WuDiPeiRanJue", "超级马桶", "1.0.0.0")]
    public class WuDiPeiRanJue : TaiwuRemakePlugin
    {
        private Harmony harm;

        public override void Initialize()
        {
            AdaptableLog.Info("WuDiPeiRanJue 初始化中...");
            harm = new Harmony("WuDiPeiRanJue.Patches");
            PatchPeiRanJueMethods();
            AdaptableLog.Info("WuDiPeiRanJue 补丁应用完成");
        }

        public override void Dispose()
        {
            harm?.UnpatchSelf();
            AdaptableLog.Info("WuDiPeiRanJue 卸载完成");
        }

        private void PatchPeiRanJueMethods()
        {
            try
            {
                // 保持原有的沛然诀补丁
                var onEnableMethod = typeof(PeiRanJue).GetMethod("OnEnable",
                    BindingFlags.Public | BindingFlags.Instance, null,
                    new Type[] { typeof(DataContext) }, null);

                if (onEnableMethod != null)
                {
                    harm.Patch(onEnableMethod,
                        prefix: new HarmonyMethod(typeof(PeiRanJueFinalPatch),
                            nameof(PeiRanJueFinalPatch.OnEnable_Prefix)));
                    AdaptableLog.Info("✓ 补丁 PeiRanJue.OnEnable");
                }


                var getModifyValueMethod = typeof(SpecialEffectBase).GetMethod("GetModifyValue",
                    BindingFlags.Public | BindingFlags.Instance, null,
                    new Type[] { typeof(AffectedDataKey), typeof(int) }, null);
                if (getModifyValueMethod != null)
                {
                    harm.Patch(getModifyValueMethod,
                        prefix: new HarmonyMethod(typeof(PeiRanJueFinalPatch),
                            nameof(PeiRanJueFinalPatch.GetModifyValue_Prefix)));
                    AdaptableLog.Info("✓ 补丁 SpecialEffectBase.GetModifyValue");
                }

                var getModifiedValueMethod = typeof(SpecialEffectBase).GetMethod("GetModifiedValue",
                    BindingFlags.Public | BindingFlags.Instance, null,
                    new Type[] { typeof(AffectedDataKey), typeof(long) }, null);
                if (getModifiedValueMethod != null)
                {
                    harm.Patch(getModifiedValueMethod,
                        prefix: new HarmonyMethod(typeof(PeiRanJueFinalPatch),
                            nameof(PeiRanJueFinalPatch.GetModifiedValue_Prefix)));
                    AdaptableLog.Info("✓ 补丁 SpecialEffectBase.GetModifyValue");
                }
            }
            
            catch (Exception e)
            {
                AdaptableLog.Error($"沛然诀方法补丁失败: {e}");
            }
        }
    }
}