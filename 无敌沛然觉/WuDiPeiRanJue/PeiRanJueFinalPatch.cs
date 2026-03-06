using System;
using System.Reflection;
using GameData.Combat.Math;
using GameData.Common;
using GameData.Domains;
using GameData.Domains.Character;
using GameData.Domains.CombatSkill;
using GameData.Domains.SpecialEffect;
using GameData.Domains.SpecialEffect.CombatSkill;
using GameData.Domains.SpecialEffect.CombatSkill.NoSect.Neigong;
using GameData.GameDataBridge;
using GameData.Utilities;

namespace WuDiPeiRanJue
{
    public static class PeiRanJueFinalPatch
    {
        private static readonly DataUid EmptyDataUid = new DataUid(0, 0, ulong.MaxValue, uint.MaxValue);
        private static DataUid _featureUid = EmptyDataUid;
        private static int _addPowerValue;

        // OnEnable 补丁
        public static bool OnEnable_Prefix(PeiRanJue __instance, DataContext context)
        {
            AdaptableLog.Info("🎯 === 进入 PeiRanJueFinalPatch.OnEnable_Prefix ===");

            try
            {
                __instance.AffectDatas = new Dictionary<AffectedDataKey, EDataModifyType>();
                __instance.AffectDatas.Add(new AffectedDataKey(__instance.CharacterId, 114, -1, -1, -1, -1),
                    EDataModifyType.Custom);
                __instance.AffectDatas.Add(new AffectedDataKey(__instance.CharacterId, 276, -1, -1, -1, -1),
                    EDataModifyType.AddPercent);
                __instance.AffectDatas.Add(new AffectedDataKey(__instance.CharacterId, 106, -1, -1, -1, -1),
                    EDataModifyType.AddPercent);
                AdaptableLog.Info("沛然觉注册了");
                return false;
            }
            catch (Exception e)
            {
                AdaptableLog.Error($"❌ OnEnable 补丁执行失败: {e}");
            }

            AdaptableLog.Info("🎯 === 退出 PeiRanJueFinalPatch.OnEnable_Prefix ===");

            // 返回 false 跳过原始 OnEnable 方法
            return false;
        }


        // GetModifyValue 补丁
        public static bool GetModifyValue_Prefix(SpecialEffectBase __instance, AffectedDataKey dataKey,
            int currModifyValue,
            ref int __result)
        {
            if (__instance is PeiRanJue peiRanJue)
            {
                try
                {
                    if (dataKey.CharId != peiRanJue.CharacterId)
                    {
                        __result = currModifyValue;
                    }

                    __result = -999;
                    return false;
                }
                catch (Exception e)
                {
                    AdaptableLog.Error($"❌ GetModifyValue 补丁执行失败: {e}");
                    return false;
                }
            }

            return false;
        }

        public static bool GetModifiedValue_Prefix(SpecialEffectBase __instance, AffectedDataKey dataKey,
            long dataValue,
            ref long __result)
        {
            if (__instance is PeiRanJue peiRanJue)
            {
                try
                {
                    if (dataKey.CharId != peiRanJue.CharacterId)
                    {
                        __result = dataValue;
                    }

                    if (dataKey.FieldId == 114)
                    {
                        var combatSkillEffectBase = (CombatSkillEffectBase)__instance;
                        combatSkillEffectBase.ShowSpecialEffectTips(0);
                        AdaptableLog.Info("沛然觉触发减伤");
                        __result = 0L;
                        return false;
                    }

                    AdaptableLog.Info("沛然觉修改成功了，但是没有起效了");
                    __result = dataValue;
                }
                catch (Exception e)
                {
                    AdaptableLog.Error($"❌ GetModifyValue 补丁执行失败: {e}");
                    return false;
                }
            }

            return false;
        }
    }
}