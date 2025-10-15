using System;
using GameData.Common;
using GameData.Domains;
using GameData.Domains.Character;
using GameData.Domains.Combat;
using GameData.Domains.Item;
using GameData.Utilities;
using HarmonyLib;
using TaiwuModdingLib.Core.Plugin;


namespace AutoFixCombatItem
{
    [PluginConfig("AutoFixCombatItem", "懒狗", "1.0.0.0")]
    public class AutoFixInit : TaiwuRemakePlugin
    {
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
            AdaptableLog.Info("自动修复装备启动");
            this.harm = new Harmony("Auto_FixCombatItem");
            harm.PatchAll(typeof(CombatDomain_StartCombat_Patch));
            AdaptableLog.Info("----------AutoFixCombatItem--------");
        }

        public Harmony harm;
    }

    [HarmonyPatch(typeof(CombatDomain), "StartCombat")]
    public class CombatDomain_StartCombat_Patch
    {
        public static void Prefix(CombatDomain __instance, DataContext context)
        {
            try
            {
                var selfChar = __instance.GetCombatCharacter(true).GetCharacter();
                ItemKey[] equipment = selfChar.GetEquipment();

                if (selfChar != null && equipment != null)
                {
                    AutoFixEquip(selfChar, equipment, context);
                }
            }
            catch (Exception ex)
            {
                AdaptableLog.Error($"战斗前装备修补异常: {ex.Message}");
            }
        }

        private static void AutoFixEquip(Character selfChar, ItemKey[] equipment, DataContext context)
        {
            var jade = selfChar.GetLifeSkillAttainment(LifeSkillType.Jade);
            var weaving = selfChar.GetLifeSkillAttainment(LifeSkillType.Weaving);
            var woodworking = selfChar.GetLifeSkillAttainment(LifeSkillType.Woodworking);
            var forging = selfChar.GetLifeSkillAttainment(LifeSkillType.Forging);

            var sumSkill = jade + weaving + woodworking + forging;

            short equipFixNum = (short)(sumSkill / 50); // 总修复值

            // 遍历装备，并修复，直到修复值用完
            foreach (var itemKey in equipment)
            {
                if (itemKey.ItemType == -1)
                {
                    continue;
                }
                if (equipFixNum <= 0) break; // 修复值已用完
            
                EquipmentBase equipmentBase = DomainManager.Item.GetBaseEquipment(itemKey);
                if (equipmentBase == null) continue;

                short maxDurability = equipmentBase.GetMaxDurability();
                short currDurability = equipmentBase.GetCurrDurability();
                if (currDurability < maxDurability)
                {
                    short missingDurability = (short)(maxDurability - currDurability);
                    short repairAmount = Math.Min(equipFixNum, missingDurability); // 本次修复量
                    equipmentBase.SetCurrDurability((short)(currDurability + repairAmount), context);
                    equipFixNum -= repairAmount; // 扣除已使用的修复值
                }
            }
        }
    }
}
