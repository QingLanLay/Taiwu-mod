using System;
using System.Collections.Generic;
using Config.EventConfig;
using GameData.Domains.Character;
using GameData.Domains.Item;
using GameData.Domains.Map;
using GameData.Domains.TaiwuEvent;
using GameData.Domains.TaiwuEvent.Enum;
using GameData.Domains.TaiwuEvent.EventHelper;
using GameData.Domains.TaiwuEvent.EventOption;
using GameData.Utilities;

namespace Modder_76561198100202539.EventConfig.Taiwu.EventGroup483794014fbc4007b2f13bd8cc1e708e
{
    /// <summary>
    /// 游方医事件 - 药材升级相关
    /// 事件ID: 2bd02acd-244e-4f81-a194-502a0389c3f6
    /// </summary>
    public class YouFangYiEvent_2bd02acd244e4f81a194502a0389c3f6 : TaiwuEventItem
    {
        /// <summary>
        /// 构造函数 - 初始化事件配置
        /// </summary>
        public YouFangYiEvent_2bd02acd244e4f81a194502a0389c3f6()
        {
            // 基础事件配置
            this.Guid = Guid.Parse("2bd02acd-244e-4f81-a194-502a0389c3f6");
            this.IsHeadEvent = true;
            this.EventGroup = "YaoCaiShengJi";
            this.ForceSingle = false;
            this.EventType = (EEventType)6; // 6可能对应特定的事件类型，需要根据实际枚举确认
            this.TriggerType = EventTrigger.SectBuildingClicked;
            this.EventSortingOrder = 500;

            // 角色配置
            this.MainRoleKey = "RoleTaiwu";
            this.TargetRoleKey = "YouFangYi";

            // 界面配置
            this.EventBackground = "";
            this.MaskControl = 0;
            this.MaskTweenTime = 0f;
            this.EscOptionKey = "";

            // 初始化事件选项（新增一个选项，原4号选项变为5号）
            this.EventOptions = new TaiwuEventOption[]
            {
                new TaiwuEventOption // 选项1 - 等级1药材升级
                {
                    OptionKey = "Option_-525764982",
                    OptionGuid = "a8912f78-4fca-48af-9ac0-26a5a879318c"
                },
                new TaiwuEventOption // 选项2 - 等级2药材升级
                {
                    OptionKey = "Option_-655991711",
                    OptionGuid = "28a9c07e-0478-44e0-acee-c5e42584efd9"
                },
                new TaiwuEventOption // 选项3 - 活死药选项
                {
                    OptionKey = "Option_-125410720",
                    OptionGuid = "2897918a-1f66-4ba6-92d0-f01afb9b53c4"
                },
                new TaiwuEventOption // 新增的4号选项 - 新功能选项
                {
                    OptionKey = "Option_-125410721",
                    OptionGuid = Guid.NewGuid().ToString()
                },
                new TaiwuEventOption // 原4号选项变为5号 - 离开选项
                {
                    OptionKey = "Option_125410724",
                    OptionGuid = Guid.NewGuid().ToString()
                }
            };

            // 初始化所有选项的回调函数
            this.InitOptions();
        }

        /// <summary>
        /// 初始化所有选项的回调函数
        /// </summary>
        private void InitOptions()
        {
            // 选项1初始化 - 药材升级选项
            this.EventOptions[0].OnOptionVisibleCheck = new Func<bool>(this.OnOption1VisibleCheck);
            this.EventOptions[0].OnOptionAvailableCheck = new Func<bool>(this.OnOption1AvailableCheck);
            this.EventOptions[0].GetReplacedContent = new Func<string>(this.OnOption1GetReplacedContent);
            this.EventOptions[0].OnOptionSelect = new Func<string>(this.OnOption1Select);
            this.EventOptions[0].GetExtraFormatLanguageKeys =
                new Func<List<string>>(this.Option1GetExtraFormatLanguageKeys);
            this.EventOptions[0].DefaultState = 0;
            this.EventOptions[0].OneTimeOnly = false;
            this.OnOption1Create();

            // 选项2初始化 - 药材升级选项
            this.EventOptions[1].OnOptionVisibleCheck = new Func<bool>(this.OnOption2VisibleCheck);
            this.EventOptions[1].OnOptionAvailableCheck = new Func<bool>(this.OnOption2AvailableCheck);
            this.EventOptions[1].GetReplacedContent = new Func<string>(this.OnOption2GetReplacedContent);
            this.EventOptions[1].OnOptionSelect = new Func<string>(this.OnOption2Select);
            this.EventOptions[1].GetExtraFormatLanguageKeys =
                new Func<List<string>>(this.Option2GetExtraFormatLanguageKeys);
            this.EventOptions[1].DefaultState = 0;
            this.EventOptions[1].OneTimeOnly = false;
            this.OnOption2Create();

            // 选项3初始化 - 活死药选项
            this.EventOptions[2].OnOptionVisibleCheck = new Func<bool>(this.OnOption3VisibleCheck);
            this.EventOptions[2].OnOptionAvailableCheck = new Func<bool>(this.OnOption3AvailableCheck);
            this.EventOptions[2].GetReplacedContent = new Func<string>(this.OnOption3GetReplacedContent);
            this.EventOptions[2].OnOptionSelect = new Func<string>(this.OnOption3Select);
            this.EventOptions[2].GetExtraFormatLanguageKeys =
                new Func<List<string>>(this.Option3GetExtraFormatLanguageKeys);
            this.EventOptions[2].DefaultState = 0;
            this.EventOptions[2].OneTimeOnly = false;
            this.OnOption3Create();

            // 选项4初始化 - 新增选项
            this.EventOptions[3].OnOptionVisibleCheck = new Func<bool>(this.OnOption4VisibleCheck);
            this.EventOptions[3].OnOptionAvailableCheck = new Func<bool>(this.OnOption4AvailableCheck);
            this.EventOptions[3].GetReplacedContent = new Func<string>(this.OnOption4GetReplacedContent);
            this.EventOptions[3].OnOptionSelect = new Func<string>(this.OnOption4Select);
            this.EventOptions[3].GetExtraFormatLanguageKeys =
                new Func<List<string>>(this.Option4GetExtraFormatLanguageKeys);
            this.EventOptions[3].DefaultState = 0;
            this.EventOptions[3].OneTimeOnly = false;
            this.OnOption4Create();

            // 选项5初始化 - 原离开选项
            this.EventOptions[4].OnOptionVisibleCheck = new Func<bool>(this.OnOption5VisibleCheck);
            this.EventOptions[4].OnOptionAvailableCheck = new Func<bool>(this.OnOption5AvailableCheck);
            this.EventOptions[4].GetReplacedContent = new Func<string>(this.OnOption5GetReplacedContent);
            this.EventOptions[4].OnOptionSelect = new Func<string>(this.OnOption5Select);
            this.EventOptions[4].GetExtraFormatLanguageKeys =
                new Func<List<string>>(this.Option5GetExtraFormatLanguageKeys);
            this.EventOptions[4].DefaultState = 0;
            this.EventOptions[4].OneTimeOnly = false;
            this.OnOption5Create();
        }

        /// <summary>
        /// 检查事件触发条件
        /// </summary>
        /// <returns>是否满足触发条件</returns>
        public override bool OnCheckEventCondition()
        {
            // 获取建筑模板ID
            short buildingTemplateId = this.ArgBox.GetShort("BuildingTemplateId");

            // 检查MOD剧情进度
            int storyProgress = -1;
            this.TaiwuEvent.GetModData("ModStoryProgress", true, ref storyProgress);
            bool isCorrectProgress = storyProgress == 1;

            // 只有剧情进度为1且点击的是建筑模板ID为157的建筑时触发
            if (isCorrectProgress)
            {
                if (buildingTemplateId == 157)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 事件进入时执行
        /// 创建游方医角色并设置相关参数
        /// </summary>
        public override void OnEventEnter()
        {
            // 创建游方医角色
            var character = EventHelper.CreateTemporaryIntelligentCharacter(
                new Location(EventArgBox.TaiwuVillageAreaId, EventArgBox.TaiwuVillageBlockId),
                1, 60, 100,
                EventHelper.GetBelongSettlementId(EventArgBox.TaiwuVillageAreaId, EventArgBox.TaiwuVillageBlockId),
                0
            );

            // 更新角色ID和标记
            this.ArgBox.Set("YouFangYi", character.GetId());

            // 设置界面隐藏标记
            this.ArgBox.Set(EventArgBox.HideFavorability, true);
            this.ArgBox.Set(EventArgBox.ForbidViewCharacter, true);
            this.ArgBox.Set("HideFavorability", true);
            this.ArgBox.Set("ForbidViewCharacter", true);
        }

        /// <summary>
        /// 事件退出时执行
        /// </summary>
        public override void OnEventExit()
        {
            // 清理操作，目前为空
        }

        /// <summary>
        /// 获取替换内容字符串
        /// </summary>
        /// <returns>替换后的内容字符串</returns>
        public override string GetReplacedContentString()
        {
            return string.Empty;
        }

        /// <summary>
        /// 获取额外的格式语言键
        /// </summary>
        /// <returns>额外的语言键列表</returns>
        public override List<string> GetExtraFormatLanguageKeys()
        {
            return null;
        }

        // ===================== 选项1方法 =====================

        /// <summary>
        /// 创建选项1的消耗信息
        /// </summary>
        private void OnOption1Create()
        {
            this.EventOptions[0].OptionConsumeInfos = new List<OptionConsumeInfo>();

            // 消耗资源：8号资源10单位，7号资源10000单位
            this.EventOptions[0].OptionConsumeInfos.Add(new OptionConsumeInfo(8, 10, true));
            this.EventOptions[0].OptionConsumeInfos.Add(new OptionConsumeInfo(7, 10000, true));
        }

        /// <summary>
        /// 检查选项1是否可见
        /// </summary>
        /// <returns>总是可见</returns>
        private bool OnOption1VisibleCheck()
        {
            return true;
        }

        /// <summary>
        /// 检查选项1是否可用
        /// 检查玩家是否有足够的指定等级药材
        /// </summary>
        /// <returns>是否有足够药材</returns>
        private bool OnOption1AvailableCheck()
        {
            int totalAmount = 0;
            Character character = this.ArgBox.GetCharacter("RoleTaiwu");
            Inventory inventory = character.GetInventory();

            int requiredGrade = 1; // 需求等级为1的药材

            // 遍历背包中的所有物品
            foreach (KeyValuePair<ItemKey, int> itemPair in inventory.Items)
            {
                ItemKey itemKey = itemPair.Key;
                int itemAmount = itemPair.Value;

                // 只处理药材类型（ItemType == 5）
                if (itemKey.ItemType == 5)
                {
                    short templateId = itemKey.TemplateId;

                    // 检查是否是有效等级为1的药材
                    bool isValidGrade1Item = this.IsValidNumber((int)templateId, requiredGrade);
                    if (isValidGrade1Item)
                    {
                        totalAmount += itemAmount;
                    }
                }
            }

            // 每3个药材可以升级1次
            int upgradeCount = totalAmount / 3;

            // 保存到参数盒供后续使用
            this.ArgBox.Set("GradeReq", requiredGrade);
            this.ArgBox.Set("addNewGrade", upgradeCount);

            return upgradeCount > 0;
        }

        /// <summary>
        /// 检查药材编号是否有效
        /// </summary>
        /// <param name="itemId">药材ID</param>
        /// <param name="grade">需求等级</param>
        /// <returns>是否有效</returns>
        private bool IsValidNumber(int itemId, int grade)
        {
            // 检查是否是药材ID范围：140-235
            // 且药材ID-140能被4整除，余数为grade-1（即grade对应余数0,1,2,3）
            return itemId >= 140 && itemId <= 235 && (itemId - 140) % 4 == grade;
        }

        /// <summary>
        /// 获取选项1的替换内容
        /// </summary>
        /// <returns>替换后的内容</returns>
        private string OnOption1GetReplacedContent()
        {
            return string.Empty;
        }

        /// <summary>
        /// 选项1被选中时执行
        /// </summary>
        /// <returns>下一个事件的GUID</returns>
        private string OnOption1Select()
        {
            int totalAmount = 0;
            Character character = this.ArgBox.GetCharacter("RoleTaiwu");
            Inventory inventory = character.GetInventory();

            int requiredGrade = 1; // 需求等级为1的药材

            // 计算可升级次数
            foreach (KeyValuePair<ItemKey, int> itemPair in inventory.Items)
            {
                ItemKey itemKey = itemPair.Key;
                int itemAmount = itemPair.Value;

                if (itemKey.ItemType == 5)
                {
                    short templateId = itemKey.TemplateId;
                    bool isValidGrade1Item = this.IsValidNumber((int)templateId, requiredGrade);
                    if (isValidGrade1Item)
                    {
                        totalAmount += itemAmount;
                    }
                }
            }

            // 每3个药材可以升级1次
            int upgradeCount = totalAmount / 3;

            // 保存到参数盒
            this.ArgBox.Set("GradeReq", requiredGrade);
            this.ArgBox.Set("addNewGrade", upgradeCount);

            // 返回下一个事件的GUID
            return "72c17bd7-00b3-4cec-8aa3-16a732a6d3b0";
        }

        /// <summary>
        /// 获取选项1的额外格式语言键
        /// </summary>
        /// <returns>语言键列表</returns>
        public List<string> Option1GetExtraFormatLanguageKeys()
        {
            return null;
        }

        // ===================== 选项2方法 =====================

        /// <summary>
        /// 创建选项2的消耗信息
        /// </summary>
        private void OnOption2Create()
        {
            this.EventOptions[1].OptionConsumeInfos = new List<OptionConsumeInfo>();

            // 消耗资源：8号资源30单位，7号资源50000单位
            this.EventOptions[1].OptionConsumeInfos.Add(new OptionConsumeInfo(8, 30, true));
            this.EventOptions[1].OptionConsumeInfos.Add(new OptionConsumeInfo(7, 50000, true));
        }

        /// <summary>
        /// 检查选项2是否可见
        /// </summary>
        /// <returns>总是可见</returns>
        private bool OnOption2VisibleCheck()
        {
            return true;
        }

        /// <summary>
        /// 检查选项2是否可用
        /// 检查玩家是否有足够的等级2药材
        /// </summary>
        /// <returns>是否有足够药材</returns>
        private bool OnOption2AvailableCheck()
        {
            int totalAmount = 0;
            Character character = this.ArgBox.GetCharacter("RoleTaiwu");
            Inventory inventory = character.GetInventory();

            int requiredGrade = 2; // 需求等级为2的药材

            // 遍历背包中的所有物品
            foreach (KeyValuePair<ItemKey, int> itemPair in inventory.Items)
            {
                ItemKey itemKey = itemPair.Key;
                int itemAmount = itemPair.Value;

                if (itemKey.ItemType == 5)
                {
                    short templateId = itemKey.TemplateId;
                    bool isValidGrade2Item = this.IsValidNumber((int)templateId, requiredGrade);
                    if (isValidGrade2Item)
                    {
                        totalAmount += itemAmount;
                    }
                }
            }

            // 每3个药材可以升级1次
            int upgradeCount = totalAmount / 3;

            // 保存到参数盒
            this.ArgBox.Set("GradeReq", requiredGrade);
            this.ArgBox.Set("addNewGrade", upgradeCount);

            return upgradeCount > 0;
        }

        /// <summary>
        /// 获取选项2的替换内容
        /// </summary>
        /// <returns>替换后的内容</returns>
        private string OnOption2GetReplacedContent()
        {
            return string.Empty;
        }

        /// <summary>
        /// 选项2被选中时执行
        /// </summary>
        /// <returns>下一个事件的GUID</returns>
        private string OnOption2Select()
        {
            int totalAmount = 0;
            Character character = this.ArgBox.GetCharacter("RoleTaiwu");
            Inventory inventory = character.GetInventory();

            int requiredGrade = 2; // 需求等级为2的药材

            // 计算可升级次数
            foreach (KeyValuePair<ItemKey, int> itemPair in inventory.Items)
            {
                ItemKey itemKey = itemPair.Key;
                int itemAmount = itemPair.Value;

                if (itemKey.ItemType == 5)
                {
                    short templateId = itemKey.TemplateId;
                    bool isValidGrade2Item = this.IsValidNumber((int)templateId, requiredGrade);
                    if (isValidGrade2Item)
                    {
                        totalAmount += itemAmount;
                    }
                }
            }

            // 每5个药材可以升级1次
            int upgradeCount = totalAmount / 5;

            // 保存到参数盒
            this.ArgBox.Set("GradeReq", requiredGrade);
            this.ArgBox.Set("addNewGrade", upgradeCount);

            // 返回下一个事件的GUID
            return "72c17bd7-00b3-4cec-8aa3-16a732a6d3b0";
        }

        /// <summary>
        /// 获取选项2的额外格式语言键
        /// </summary>
        /// <returns>语言键列表</returns>
        public List<string> Option2GetExtraFormatLanguageKeys()
        {
            return null;
        }

        // ===================== 选项3方法 =====================

        /// <summary>
        /// 创建选项3的消耗信息
        /// </summary>
        private void OnOption3Create()
        {
            this.EventOptions[2].OptionConsumeInfos = new List<OptionConsumeInfo>();

            // 消耗资源：8号资源30单位，7号资源50000单位
            this.EventOptions[2].OptionConsumeInfos.Add(new OptionConsumeInfo(8, 30, true));
            this.EventOptions[2].OptionConsumeInfos.Add(new OptionConsumeInfo(7, 50000, true));
        }

        /// <summary>
        /// 检查选项3是否可见
        /// </summary>
        /// <returns>总是可见</returns>
        private bool OnOption3VisibleCheck()
        {
            return true;
        }

        /// <summary>
        /// 检查选项3是否可用
        /// </summary>
        /// <returns>总是可用</returns>
        private bool OnOption3AvailableCheck()
        {
            return true;
        }

        /// <summary>
        /// 获取选项3的替换内容
        /// </summary>
        /// <returns>"活死药。"</returns>
        private string OnOption3GetReplacedContent()
        {
            return "活死药。";
        }

        /// <summary>
        /// 选项3被选中时执行 - 给予玩家活死药
        /// </summary>
        /// <returns>空字符串，表示不跳转其他事件</returns>
        private string OnOption3Select()
        {
            Character character = this.ArgBox.GetCharacter("RoleTaiwu");

            // 添加活死药（物品模板ID 387）到玩家背包
            ItemKey item = EventHelper.AddItemToRole(character, 8, 387, 1, -1);

            // 显示获得物品的界面
            EventHelper.ShowGetItemPageForItems(
                new List<ValueTuple<ItemKey, int>>
                {
                    new ValueTuple<ItemKey, int>(item, 1)
                },
                "",
                this.ArgBox,
                false
            );

            return string.Empty;
        }

        /// <summary>
        /// 获取选项3的额外格式语言键
        /// </summary>
        /// <returns>语言键列表</returns>
        public List<string> Option3GetExtraFormatLanguageKeys()
        {
            return null;
        }

        // ===================== 新增选项4方法 =====================

        /// <summary>
        /// 创建新增选项4
        /// </summary>
        private void OnOption4Create()
        {
            this.EventOptions[3].OptionConsumeInfos = new List<OptionConsumeInfo>();

            // 消耗资源：8号资源30单位，7号资源50000单位
            this.EventOptions[3].OptionConsumeInfos.Add(new OptionConsumeInfo(8, 15, true));
            this.EventOptions[3].OptionConsumeInfos.Add(new OptionConsumeInfo(7, 5000, true));
        }

        /// <summary>
        /// 检查选项4是否可见
        /// </summary>
        /// <returns>总是可见</returns>
        private bool OnOption4VisibleCheck()
        {
            return true;
        }

        /// <summary>
        /// 检查选项4是否可用
        /// 可以根据需要添加特定的可用性检查逻辑
        /// </summary>
        /// <returns>总是可用</returns>
        private bool OnOption4AvailableCheck()
        {
            return true;
        }

        /// <summary>
        /// 获取选项4的替换内容
        /// 这里需要根据新增功能设置具体的显示内容
        /// </summary>
        /// <returns>新增选项的显示文本</returns>
        private string OnOption4GetReplacedContent()
        {
            return "毒炼十炉九废，神品全凭天赐。奉上全数毒引后，祸福自担，余毒存毁，望三思。"; // 修改为您需要的文本
        }

        /// <summary>
        /// 选项4被选中时执行
        /// 这里需要根据新增功能设置具体的执行逻辑
        /// </summary>
        /// <returns>
        /// 如果跳转到其他事件，返回事件GUID
        /// 如果结束当前事件，返回空字符串
        /// </returns>
        private string OnOption4Select()
        {
            Character character = this.ArgBox.GetCharacter("RoleTaiwu");

            ProcessPoisonUpgrade(character);

            return string.Empty; // 默认返回空字符串
        }

        /// <summary>
        /// 获取选项4的额外格式语言键
        /// </summary>
        /// <returns>语言键列表</returns>
        public List<string> Option4GetExtraFormatLanguageKeys()
        {
            return null;
        }

        // ===================== 选项5方法（原选项4） =====================

        /// <summary>
        /// 创建选项5的消耗信息（原选项4）
        /// </summary>
        private void OnOption5Create()
        {
            // 5号选项为离开选项，不消耗任何资源
            // 可以在此处添加离开时的特殊处理逻辑
        }

        /// <summary>
        /// 检查选项5是否可见
        /// </summary>
        /// <returns>总是可见</returns>
        private bool OnOption5VisibleCheck()
        {
            return true;
        }

        /// <summary>
        /// 检查选项5是否可用
        /// </summary>
        /// <returns>总是可用</returns>
        private bool OnOption5AvailableCheck()
        {
            return true;
        }

        /// <summary>
        /// 获取选项5的替换内容
        /// </summary>
        /// <returns>"药材不足，改日再叙。"</returns>
        private string OnOption5GetReplacedContent()
        {
            return "药材不足，改日再叙。";
        }

        /// <summary>
        /// 选项5被选中时执行 - 离开对话
        /// </summary>
        /// <returns>空字符串，结束事件</returns>
        private string OnOption5Select()
        {
            // 可以在此处添加离开时的特殊处理逻辑
            return string.Empty;
        }

        /// <summary>
        /// 获取选项5的额外格式语言键
        /// </summary>
        /// <returns>语言键列表</returns>
        public List<string> Option5GetExtraFormatLanguageKeys()
        {
            return null;
        }

        public void ProcessPoisonUpgrade(Character character)
        {
            // 毒药的ItemType（需要根据实际游戏设置调整）
            const int POISON_ITEM_TYPE = 5; // 假设毒药的ItemType为5

            // 毒药ID范围：236-277（A-F类，每类7个等级）
            const int MIN_POISON_ID = 236;
            const int MAX_POISON_ID = 277;
            const int CLASS_COUNT = 6; // A-F共6类
            const int GRADE_COUNT = 7; // 每类7个等级

            // 获取玩家和背包
            Character playerCharacter = this.ArgBox.GetCharacter("RoleTaiwu");
            Inventory inventory = playerCharacter.GetInventory();

            // 1. 遍历背包，统计1-6级毒药的总价值并移除
            int totalPoisonValue = 0;
            Dictionary<short, int> removedPoisons = new Dictionary<short, int>(); // 记录移除的毒药（用于日志）

            foreach (KeyValuePair<ItemKey, int> itemPair in inventory.Items.ToList()) // 使用ToList避免修改集合错误
            {
                ItemKey itemKey = itemPair.Key;
                int itemCount = itemPair.Value;

                // 只处理毒药类型
                if (itemKey.ItemType == POISON_ITEM_TYPE)
                {
                    short templateId = itemKey.TemplateId;

                    // 检查是否在毒药ID范围内
                    if (templateId >= MIN_POISON_ID && templateId <= MAX_POISON_ID)
                    {
                        // 计算毒药等级 (1-7)
                        int poisonGrade = ((templateId - MIN_POISON_ID) % GRADE_COUNT) + 1;

                        // 只处理1-6级毒药
                        if (poisonGrade >= 1 && poisonGrade <= 6)
                        {
                            // 计算毒药价值：等级 × 数量
                            int poisonValue = poisonGrade * itemCount;
                            totalPoisonValue += poisonValue;

                            // 记录移除的毒药
                            if (!removedPoisons.ContainsKey(templateId))
                                removedPoisons[templateId] = 0;
                            removedPoisons[templateId] += itemCount;

                            // 移除低等级毒药
                            EventHelper.RemoveInventoryItem(playerCharacter, itemKey, itemCount, true);
                        }
                    }
                }
            }

            // 如果没有收集到毒药，直接返回
            if (totalPoisonValue <= 0)
            {
                AdaptableLog.Info("没找到毒药");
                return;
            }

            // 2. 计算可用价值（50%损耗）
            int availableValue = totalPoisonValue / 2;
            var upgradedPoisons = new Dictionary<ItemKey, int>();

            // 3. 随机兑换新毒药
            Random random = new Random();

            while (availableValue > 0)
            {
                // 确定本次兑换的毒药等级
                int newPoisonGrade;

                // 25%概率生成7级毒药，75%概率生成1-6级毒药
                if (random.NextDouble() < 0.25)
                {
                    newPoisonGrade = 7; // 7级毒药
                }
                else
                {
                    // 1-6级随机（等概率）
                    newPoisonGrade = random.Next(1, 7);
                }

                // 随机选择毒药种类（A-F类）
                int poisonClass = random.Next(0, CLASS_COUNT); // 0-5对应A-F

                // 计算新毒药的templateId
                short newTemplateId = (short)(MIN_POISON_ID + (poisonClass * GRADE_COUNT) + (newPoisonGrade - 1));

                // 计算兑换所需价值：7级毒药价值翻倍
                int poisonCost = newPoisonGrade;
                if (newPoisonGrade == 7)
                {
                    poisonCost *= 2; // 7级毒药价值翻倍
                }

                // 检查剩余价值是否足够兑换
                if (availableValue >= poisonCost)
                {
                    // 创建ItemKey
                    ItemKey newItem =
                        EventHelper.AddItemToRole(character, POISON_ITEM_TYPE, newTemplateId, 1, -1);
                    if (upgradedPoisons.ContainsKey(newItem))
                    {
                        upgradedPoisons[newItem] += 1; // 如果存在，相加
                    }
                    else
                    {
                        upgradedPoisons[newItem] = 1; // 如果不存在，添加
                    }

                    // 扣除相应价值
                    availableValue -= poisonCost;
                }
                else
                {
                    // 剩余价值不足以兑换任何毒药，结束循环
                    break;
                }
            }

            // 4. 将兑换的毒药添加到背包
            List<ValueTuple<ItemKey, int>> gainedItems = new List<ValueTuple<ItemKey, int>>();

            foreach (var newItem in upgradedPoisons)
            {
                gainedItems.Add(new ValueTuple<ItemKey, int>(newItem.Key, newItem.Value));
            }

            // 5. 显示兑换结果
            if (gainedItems.Count > 0)
            {
                // 构建兑换日志
                string logMessage = $"消耗毒药总价值：{totalPoisonValue}\n";
                logMessage += $"实际可用价值：{totalPoisonValue / 2}\n";
                logMessage += $"兑换损耗：{totalPoisonValue / 2}\n";

                // 计算实际消耗的价值（用于显示对比）
                int actualUsedValue = 0;
                foreach (var item in gainedItems)
                {
                    short templateId = item.Item1.TemplateId;
                    int poisonGrade = ((templateId - MIN_POISON_ID) % GRADE_COUNT) + 1;
                    actualUsedValue += (poisonGrade == 7) ? 14 : poisonGrade; // 7级毒药价值为14
                }

                logMessage += $"实际消耗价值：{actualUsedValue}\n";
                logMessage += $"兑换获得：\n";

                foreach (var item in gainedItems)
                {
                    short templateId = item.Item1.TemplateId;
                    int poisonClass = (templateId - MIN_POISON_ID) / GRADE_COUNT;
                    int poisonGrade = ((templateId - MIN_POISON_ID) % GRADE_COUNT) + 1;
                    char classChar = (char)('A' + poisonClass);

                    logMessage += $"- {classChar}{poisonGrade}级毒药 ×{item.Item2}\n";
                }

                AdaptableLog.Info(logMessage);

                // 显示获取物品页面
                EventHelper.ShowGetItemPageForItems(gainedItems, "", this.ArgBox, false);
            }
        }
    }
}