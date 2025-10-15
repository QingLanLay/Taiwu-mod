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
using TaiwuModdingLib.Core.Utils;
using Character = GameData.Domains.Character.Character;
using CombatSkill = GameData.Domains.CombatSkill.CombatSkill;

namespace CustomPeiRanJue;

[PluginConfig("CustomPeiRanJue", "超级马桶", "0.1")]
public class InitPeiRanJue : TaiwuRemakeHarmonyPlugin
{
    public static long _peiRanJueId;

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
        AdaptableLog.Info("=== 开始初始化新版沛然诀 Mod ===");
        this.harm = new Harmony("Custom_PeiRanJue");

        try
        {
            // 保持原有的沛然诀补丁
            PatchPeiRanJueMethods();
            Custom_PeiRanJue_DirectEffectID();
            Custom_PeiRanJue_ReverseEffectID();
            AdaptableLog.Info("Harmony 特效系统补丁完成");
        }
        catch (Exception e)
        {
            AdaptableLog.Error($"Harmony 补丁失败: {e}");
        }

        AdaptableLog.Info("=== 新版沛然诀 Mod 初始化完成 ===");
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

            var onDisableMethod = typeof(SpecialEffectBase).GetMethod("OnDisable",
                BindingFlags.Public | BindingFlags.Instance, null,
                new Type[] { typeof(DataContext) }, null);
            if (onDisableMethod != null)
            {
                harm.Patch(onDisableMethod,
                    prefix: new HarmonyMethod(typeof(PeiRanJueFinalPatch),
                        nameof(PeiRanJueFinalPatch.OnDisable_Prefix)));
                AdaptableLog.Info("✓ 补丁 SpecialEffectBase.OnDisable");
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
        }
        catch (Exception e)
        {
            AdaptableLog.Error($"沛然诀方法补丁失败: {e}");
        }
    }

    public static void Custom_PeiRanJue_DirectEffectID()
    {
        try
        {
            // 1. 直接获取目标 SpecialEffectItem 实例
            var targetEffect = SpecialEffect.Instance[Config.CombatSkill.Instance["沛然诀"].DirectEffectID];
            
            ModifyField_PeiRanJue(targetEffect);
            AdaptableLog.Info("沛然诀正练修改成功");
            // 验证修改是否真的生效
            VerifyModification(targetEffect);
        }
        catch (Exception ex)
        {
            AdaptableLog.Info($"修改失败: {ex.Message}");
            AdaptableLog.Info($"堆栈跟踪: {ex.StackTrace}");
        }
    }

    public static void Custom_PeiRanJue_ReverseEffectID()
    {
        try
        {
            // 1. 直接获取目标 SpecialEffectItem 实例
            var targetEffect = SpecialEffect.Instance[Config.CombatSkill.Instance["沛然诀"].ReverseEffectID];
            
            ModifyField_PeiRanJue(targetEffect);
            AdaptableLog.Info("沛然诀逆练修改成功");
            
            // 验证修改是否真的生效
            VerifyModification(targetEffect);
        }
        catch (Exception ex)
        {
            AdaptableLog.Info($"修改失败: {ex.Message}");
            AdaptableLog.Info($"堆栈跟踪: {ex.StackTrace}");
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
            (sbyte) 2,
            BindingFlags.Instance | BindingFlags.Public
        );
    }

    private static void VerifyModification(SpecialEffectItem effect)
    {
        try
        {
            // 使用内置方法获取字段值进行验证
            var descValue = TaiwuModdingLib.Core.Utils.ReflectionExtensions.GetFieldValue<SpecialEffectItem>(
                effect, 
                "Desc", 
                BindingFlags.Instance | BindingFlags.Public
            ) as string[];
        
            var effectTypeValue = TaiwuModdingLib.Core.Utils.ReflectionExtensions.GetFieldValue<SpecialEffectItem>(
                effect, 
                "EffectActiveType", 
                BindingFlags.Instance | BindingFlags.Public
            );
        
            if (descValue != null && descValue.Length > 0)
            {
                AdaptableLog.Info($"验证 - Desc: {descValue[0]}");
            }
        
            AdaptableLog.Info($"验证 - EffectActiveType: {effectTypeValue}");
        }
        catch (Exception ex)
        {
            AdaptableLog.Info($"验证失败: {ex.Message}");
        }
    }
    
    private Harmony harm;
}

// 特效系统补丁 - 确保沛然诀在装备时就被创建和激活

// 最终的沛然诀补丁实现
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
            // 创建自定义的威力加成效果（字段199）
            __instance.CreateAffectedData(199, EDataModifyType.AddPercent, -1);
            AdaptableLog.Info("✅ 已创建自定义威力加成效果数据（字段199）");

            // 保留原始走火入魔减伤效果（字段117）
            __instance.CreateAffectedData(117, EDataModifyType.Custom, -1);
            AdaptableLog.Info("✅ 已保留原始走火入魔减伤效果（字段117）");

            // 初始化特征监听
            InitializeFeatureMonitoring(__instance);

            // 更新威力值
            UpdateAddPowerValue(__instance);

            AdaptableLog.Info($"角色ID: {__instance.CharacterId}, DataHandlerKey: {__instance.DataHandlerKey}");
            AdaptableLog.Info("✅ OnEnable 补丁执行完成");
        }
        catch (Exception e)
        {
            AdaptableLog.Error($"❌ OnEnable 补丁执行失败: {e}");
        }

        AdaptableLog.Info("🎯 === 退出 PeiRanJueFinalPatch.OnEnable_Prefix ===");

        // 返回 false 跳过原始 OnEnable 方法
        return false;
    }

    // OnDisable 补丁
    public static bool OnDisable_Prefix(SpecialEffectBase __instance, DataContext context)
    {
        if (__instance is PeiRanJue peiRanJue)
        {
            AdaptableLog.Info("🎯 === 进入 PeiRanJueFinalPatch.OnDisable_Prefix ===");

            try
            {
                // 清理数据修改处理器
                if (!_featureUid.Equals(EmptyDataUid))
                {
                    GameDataBridge.RemovePostDataModificationHandler(_featureUid, peiRanJue.DataHandlerKey);
                    AdaptableLog.Info("✅ 已移除数据修改处理器");
                    _featureUid = EmptyDataUid;
                }

                AdaptableLog.Info("✅ OnDisable 补丁执行完成");
            }
            catch (Exception e)
            {
                AdaptableLog.Error($"❌ OnDisable 补丁执行失败: {e}");
            }

            AdaptableLog.Info("🎯 === 退出 PeiRanJueFinalPatch.OnDisable_Prefix ===");

            return false;
        }

        return true;
    }

    // GetModifyValue 补丁
    public static bool GetModifyValue_Prefix(SpecialEffectBase __instance, AffectedDataKey dataKey, int currModifyValue,
        ref int __result)
    {
        if (__instance is PeiRanJue peiRanJue)
        {
            try
            {
                // 只处理我们自己的角色
                if (dataKey.CharId != peiRanJue.CharacterId)
                {
                    __result = 0;
                    return false;
                }

                // 处理自定义字段199（威力加成）
                if (dataKey.FieldId == 199)
                {
                    __result = _addPowerValue;
                    // AdaptableLog.Info($"💪 威力加成: {_addPowerValue} (字段199)");
                    return false;
                }

                // 处理原始字段117（走火入魔减伤）
                if (dataKey.FieldId == 117)
                {
                    // 保持原始逻辑：-75%
                    __result = currModifyValue * -75;
                    AdaptableLog.Info($"🛡️ 走火入魔减伤: {__result} (字段117)");
                    return false;
                }

                // 对于其他字段，返回0
                __result = 0;
                return false;
            }
            catch (Exception e)
            {
                AdaptableLog.Error($"❌ GetModifyValue 补丁执行失败: {e}");
                return true;
            }
        }

        return true;
    }

    // 初始化特征监控
    private static void InitializeFeatureMonitoring(PeiRanJue instance)
    {
        try
        {
            AdaptableLog.Info("🔄 初始化特征监控");

            // 使用正确的重载方法
            MethodInfo parseMethod = typeof(SpecialEffectBase).GetMethod("ParseCharDataUid",
                BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                new Type[] { typeof(int), typeof(ushort) },
                null);

            if (parseMethod != null)
            {
                _featureUid = (DataUid)parseMethod.Invoke(instance, new object[] { instance.CharacterId, (ushort)17 });
                GameDataBridge.AddPostDataModificationHandler(_featureUid, instance.DataHandlerKey,
                    new Action<DataContext, DataUid>(OnFeaturesChange));
                AdaptableLog.Info($"✅ 已添加数据修改处理器: {_featureUid}");
            }
            else
            {
                AdaptableLog.Error("❌ 未找到 ParseCharDataUid 方法");
            }
        }
        catch (Exception e)
        {
            AdaptableLog.Error($"❌ 特征监控初始化失败: {e}");
        }
    }

    // 特征变化回调
// 特征变化回调 - 修复版本
    private static void OnFeaturesChange(DataContext context, DataUid dataUid)
    {
        try
        {
            int charId = (int)dataUid.SubId0;
            AdaptableLog.Info($"🔄 特征数据变化，刷新角色 {charId} 的沛然诀缓存");

            // 1. 重新计算威力值
            UpdateAddPowerValueForCharacter(charId);

            // 2. 关键：通知系统刷新这个角色的199字段缓存
            DomainManager.SpecialEffect.InvalidateCache(context, charId, 199);

            AdaptableLog.Info($"✅ 已刷新角色 {charId} 的沛然诀缓存，新威力值: {GetCurrentAddPowerValue(charId)}");
        }
        catch (Exception e)
        {
            AdaptableLog.Error($"❌ 特征变化回调失败: {e}");
        }
    }

// 为特定角色更新威力值
    private static void UpdateAddPowerValueForCharacter(int charId)
    {
        try
        {
            // 这里需要找到对应的PeiRanJue实例
            // 由于是静态方法，我们需要通过角色ID来管理
            var character = DomainManager.Character.GetElement_Objects(charId);
            if (character != null)
            {
                MainAttributes allAttrs = character.GetMaxMainAttributes();
                var sum = allAttrs.GetSum();
                int newValue = (sum / 75) * 5;

                // 更新对应角色的威力值
                UpdateCharacterPowerValue(charId, newValue);

                AdaptableLog.Info($"🔢 重新计算角色 {charId} 的威力加成: {newValue} (总属性: {sum})");
            }
        }
        catch (Exception e)
        {
            AdaptableLog.Error($"❌ 角色 {charId} 威力计算失败: {e}");
        }
    }

// 管理多个角色的威力值（支持多角色）
    private static readonly Dictionary<int, int> _characterPowerValues = new Dictionary<int, int>();

    private static void UpdateCharacterPowerValue(int charId, int value)
    {
        _characterPowerValues[charId] = value;
    }

    private static int GetCurrentAddPowerValue(int charId)
    {
        return _characterPowerValues.TryGetValue(charId, out int value) ? value : 100;
    }

    // 更新威力值
    private static void UpdateAddPowerValue(PeiRanJue instance)
    {
        _addPowerValue = 0;
        if (instance?.CharObj != null)
        {
            try
            {
                Character taiWu = instance.CharObj;
                MainAttributes allAttrs = taiWu.GetMaxMainAttributes();
                var sum = allAttrs.GetSum();
                _addPowerValue = (sum / 75) * 5; // 你的自定义计算公式
                AdaptableLog.Info($"🔢 计算威力加成: {_addPowerValue} (总属性: {sum})");
            }
            catch (Exception e)
            {
                AdaptableLog.Error($"❌ 威力计算失败: {e}");
                _addPowerValue = 100; // 默认值
            }
        }
        else
        {
            AdaptableLog.Error("⚠️ 无法获取角色对象，使用默认威力值");
            _addPowerValue = 100; // 默认值
        }
    }
}