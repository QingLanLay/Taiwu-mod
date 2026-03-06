using System;
using System.Collections.Generic;
using Config.EventConfig;
using GameData.Domains.Character;
using GameData.Domains.Item;
using GameData.Domains.TaiwuEvent;
using GameData.Domains.TaiwuEvent.Enum;
using GameData.Domains.TaiwuEvent.EventHelper;

namespace Modder_76561198100202539.EventConfig.Taiwu.EventGroup483794014fbc4007b2f13bd8cc1e708e
{
    /// <summary>
    /// 有方仪事件 - 药材升级选择界面
    /// 事件ID: 72c17bd7-00b3-4cec-8aa3-16a732a6d3b0
    /// 功能：提供多种药材升级选项的界面（按功能分组排序）
    /// </summary>
    public class YouFangYiEvent_72c17bd700b34cec8aa316a732a6d3b0 : TaiwuEventItem
    {
        // 定义选项数据结构
        private class OptionInfo
        {
            public string OptionKey { get; set; }
            public string OptionGuid { get; set; }
            public short QiId { get; set; }      // 奇类药材目标ID（requiredGrade=1时使用）
            public short JueId { get; set; }      // 绝类药材目标ID（requiredGrade=2时使用）
            public string DisplayText { get; set; } // 选项显示的额外文本（为空则不显示）
            public bool IsReturn { get; set; }    // 是否为返回选项
        }

        /// <summary>
        /// 构造函数 - 初始化事件配置
        /// </summary>
        public YouFangYiEvent_72c17bd700b34cec8aa316a732a6d3b0()
        {
            // 基础事件配置
            this.Guid = Guid.Parse("72c17bd7-00b3-4cec-8aa3-16a732a6d3b0");
            this.IsHeadEvent = false;
            this.EventGroup = "YaoCaiShengJi";
            this.ForceSingle = false;
            this.EventType = (EEventType)6;
            this.TriggerType = EventTrigger.None;
            this.EventSortingOrder = 500;
            
            this.MainRoleKey = "";
            this.TargetRoleKey = "";
            this.EventBackground = "";
            this.MaskControl = 0;
            this.MaskTweenTime = 0f;
            this.EscOptionKey = "";

            // 构建所有选项的数据（按功能分组排序）
            var optionsData = new List<OptionInfo>
            {
                // ========== 治疗类 ==========
                new OptionInfo { OptionKey = "Option_142143", OptionGuid = "14214300-0000-0000-0000-000000001423", QiId = 142, JueId = 143, DisplayText = "治疗外伤" },
                new OptionInfo { OptionKey = "Option_158159", OptionGuid = "15815900-0000-0000-0000-000000001581", QiId = 158, JueId = 159, DisplayText = "治疗内伤" },
                new OptionInfo { OptionKey = "Option_190191", OptionGuid = "19019100-0000-0000-0000-000000001901", QiId = 190, JueId = 191, DisplayText = "恢复健康" },

                // ========== 毒类 ==========
                new OptionInfo { OptionKey = "Option_146147", OptionGuid = "14614700-0000-0000-0000-000000001461", QiId = 146, JueId = 147, DisplayText = "化解赤毒" },
                new OptionInfo { OptionKey = "Option_162163", OptionGuid = "16216300-0000-0000-0000-000000001621", QiId = 162, JueId = 163, DisplayText = "化解郁毒" },
                new OptionInfo { OptionKey = "Option_178179", OptionGuid = "17817900-0000-0000-0000-000000001781", QiId = 178, JueId = 179, DisplayText = "化解寒毒" },
                new OptionInfo { OptionKey = "Option_194195", OptionGuid = "19419500-0000-0000-0000-000000001941", QiId = 194, JueId = 195, DisplayText = "化解幻毒" },
                new OptionInfo { OptionKey = "Option_210211", OptionGuid = "21021100-0000-0000-0000-000000002101", QiId = 210, JueId = 211, DisplayText = "化解腐毒" },
                new OptionInfo { OptionKey = "Option_226227", OptionGuid = "22622700-0000-0000-0000-000000002261", QiId = 226, JueId = 227, DisplayText = "化解烈度" },

                // ========== 属性类（战斗基础属性） ==========
                new OptionInfo { OptionKey = "Option_1936792895", OptionGuid = "ad824dc5-ac17-4b55-91cb-767f29fa64c4", QiId = 154, JueId = 155, DisplayText = "力道" },      // 原选项5
                new OptionInfo { OptionKey = "Option_1112125176", OptionGuid = "133cd4a6-5599-45b2-8498-c7649be1ecc7", QiId = 186, JueId = 187, DisplayText = "精妙" },      // 原选项6
                new OptionInfo { OptionKey = "Option_1518141782", OptionGuid = "af3ba0d3-1378-4918-bb0c-8f0978255466", QiId = 218, JueId = 219, DisplayText = "迅疾" },      // 原选项7
                new OptionInfo { OptionKey = "Option_1378749027", OptionGuid = "ced937b4-ceef-4b5e-803d-61d5a4e23256", QiId = 198, JueId = 199, DisplayText = "拆招" },      // 原选项9
                new OptionInfo { OptionKey = "Option_627222926", OptionGuid = "97ada584-78bf-4f06-a3ec-a55ac7c9843a", QiId = 182, JueId = 183, DisplayText = "卸力" },      // 原选项8
                new OptionInfo { OptionKey = "Option_-1194893333", OptionGuid = "a2c7de43-3037-4bc7-aa79-56468736ad8c", QiId = 214, JueId = 215, DisplayText = "闪避" },      // 原选项10
                new OptionInfo { OptionKey = "Option_-893284401", OptionGuid = "59cc595d-92e0-473b-ab7f-a66dcf895990", QiId = 150, JueId = 151, DisplayText = "御体" },      // 原选项3
                new OptionInfo { OptionKey = "Option_-857513030", OptionGuid = "6b8c0036-2797-4615-ad15-09447c59d5ca", QiId = 166, JueId = 167, DisplayText = "御气" },      // 原选项4
                new OptionInfo { OptionKey = "Option_-891361317", OptionGuid = "e267f16b-6fd3-4f4b-a68f-3b898aa8e00e", QiId = 222, JueId = 223, DisplayText = "破气" },      // 原选项2
                new OptionInfo { OptionKey = "Option_-1126735797", OptionGuid = "613e94a9-d9e1-4511-b3a1-7e0a0d0e960c", QiId = 234, JueId = 235, DisplayText = "破体" },      // 原选项1

                // ========== 属性类（速度相关） ==========
                new OptionInfo { OptionKey = "Option_170171_12", OptionGuid = "17017100-0000-0000-0000-000000001701", QiId = 170, JueId = 171, DisplayText = "攻击速度" },   // 原选项12
                new OptionInfo { OptionKey = "Option_206207_13", OptionGuid = "20620700-0000-0000-0000-000000002061", QiId = 206, JueId = 207, DisplayText = "提气速度" },   // 原选项13
                new OptionInfo { OptionKey = "Option_1602323249", OptionGuid = "ce2ada07-9ca9-4830-bec8-b0064cd19334", QiId = 202, JueId = 203, DisplayText = "移动速度" },   // 原选项11

                // ========== 属性类（特殊） ==========
                new OptionInfo { OptionKey = "Option_174175", OptionGuid = "17417500-0000-0000-0000-000000001741", QiId = 174, JueId = 175, DisplayText = "调理内息" },    // 新增
                new OptionInfo { OptionKey = "Option_230231_14", OptionGuid = "23023100-0000-0000-0000-000000002301", QiId = 230, JueId = 231, DisplayText = "施展引气" },   // 原选项14

                // ========== 返回选项 ==========
                new OptionInfo { OptionKey = "Option_99_Return", OptionGuid = "99999999-9999-9999-9999-999999999999", IsReturn = true, DisplayText = "(返回)" }
            };

            // 创建事件选项数组
            this.EventOptions = new TaiwuEventOption[optionsData.Count];
            for (int i = 0; i < optionsData.Count; i++)
            {
                var data = optionsData[i];
                this.EventOptions[i] = new TaiwuEventOption
                {
                    OptionKey = data.OptionKey,
                    OptionGuid = data.OptionGuid
                };
            }

            // 初始化所有选项的回调
            InitOptions(optionsData);
        }

        /// <summary>
        /// 初始化选项回调
        /// </summary>
        private void InitOptions(List<OptionInfo> optionsData)
        {
            for (int i = 0; i < optionsData.Count; i++)
            {
                var data = optionsData[i];
                var option = this.EventOptions[i];

                // 通用可见性和可用性（全部可用）
                option.OnOptionVisibleCheck = () => true;
                option.OnOptionAvailableCheck = () => true;

                // 替换内容（如果有显示文本则返回，否则返回空）
                option.GetReplacedContent = () => data.DisplayText ?? string.Empty;

                // 选择处理
                if (data.IsReturn)
                {
                    // 返回选项：跳回上一级事件
                    option.OnOptionSelect = () => "2bd02acd-244e-4f81-a194-502a0389c3f6";
                }
                else
                {
                    // 普通选项：执行药材升级
                    // 捕获当前ID对，避免闭包问题
                    short qi = data.QiId;
                    short jue = data.JueId;
                    option.OnOptionSelect = () =>
                    {
                        ProcessHerbUpgrade(qi, jue);
                        return string.Empty;
                    };
                }

                option.GetExtraFormatLanguageKeys = () => null;
                option.DefaultState = 0;
                option.OneTimeOnly = false;
            }
        }

        /// <summary>
        /// 检查事件触发条件（由其他事件直接跳转，始终可触发）
        /// </summary>
        public override bool OnCheckEventCondition() => true;

        /// <summary>
        /// 事件进入时执行
        /// </summary>
        public override void OnEventEnter() { }

        /// <summary>
        /// 事件退出时执行
        /// </summary>
        public override void OnEventExit() { }

        /// <summary>
        /// 获取替换内容字符串（事件描述）
        /// </summary>
        public override string GetReplacedContentString()
        {
            int upgradeCount = 0;
            this.ArgBox.Get("addNewGrade", ref upgradeCount);
            return string.Format("可以炼出{0}株上等药材。", upgradeCount);
        }

        /// <summary>
        /// 获取额外的格式语言键
        /// </summary>
        public override List<string> GetExtraFormatLanguageKeys() => null;

        // ===================== 核心处理方法 =====================

        /// <summary>
        /// 检查药材编号是否有效
        /// </summary>
        private bool IsValidNumber(int itemId, int grade)
        {
            return itemId >= 140 && itemId <= 235 && (itemId - 140) % 4 == grade;
        }

        /// <summary>
        /// 核心药材升级处理方法
        /// </summary>
        /// <param name="newHerbIdQi">新的奇类药材ID</param>
        /// <param name="newHerbIdJue">新的绝类药材ID</param>
        public void ProcessHerbUpgrade(short newHerbIdQi, short newHerbIdJue)
        {
            int requiredGrade = 0;
            this.ArgBox.Get("GradeReq", ref requiredGrade);

            int totalHerbCount = 0;
            Character playerCharacter = this.ArgBox.GetCharacter("RoleTaiwu");
            Inventory inventory = playerCharacter.GetInventory();

            // 遍历背包，移除符合条件的低等级药材并统计数量
            foreach (KeyValuePair<ItemKey, int> itemPair in inventory.Items)
            {
                ItemKey itemKey = itemPair.Key;
                int itemCount = itemPair.Value;

                if (itemKey.ItemType == 5) // 药材类型
                {
                    short templateId = itemKey.TemplateId;
                    if (IsValidNumber(templateId, requiredGrade))
                    {
                        totalHerbCount += itemCount;
                        EventHelper.RemoveInventoryItem(playerCharacter, itemKey, itemCount, true);
                    }
                }
            }

            // 根据等级计算可升级数量
            int upgradeCount = 0;
            if (requiredGrade == 1)
                upgradeCount = totalHerbCount / 3;
            else if (requiredGrade == 2)
                upgradeCount = totalHerbCount / 5;

            if (upgradeCount <= 0)
                return;

            // 添加高等级药材
            ItemKey newItem;
            if (requiredGrade == 1)
                newItem = EventHelper.AddItemToRole(playerCharacter, 5, newHerbIdQi, upgradeCount, -1);
            else if (requiredGrade == 2)
                newItem = EventHelper.AddItemToRole(playerCharacter, 5, newHerbIdJue, upgradeCount, -1);
            else
                return;

            // 显示获得物品页面
            EventHelper.ShowGetItemPageForItems(
                new List<ValueTuple<ItemKey, int>> { new ValueTuple<ItemKey, int>(newItem, upgradeCount) },
                "",
                this.ArgBox,
                false
            );
        }
    }
}