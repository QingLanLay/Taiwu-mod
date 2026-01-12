using System;
using System.Collections.Generic;
using Config.EventConfig;
using GameData.Domains.Character;
using GameData.Domains.Item;
using GameData.Domains.TaiwuEvent;
using GameData.Domains.TaiwuEvent.Enum;
using GameData.Domains.TaiwuEvent.EventHelper;
using GameData.Utilities;

namespace Modder_76561198100202539.EventConfig.Taiwu.EventGroup483794014fbc4007b2f13bd8cc1e708e
{
    /// <summary>
    /// 游方医事件 - 药材赠送事件
    /// 事件ID: bb15f5e8-069a-4ca7-b497-fa1510e7b671
    /// 功能：赠送特定药材并跳转到下一个事件
    /// </summary>
    public class YouFangYiEvent_bb15f5e8069a4ca7b497fa1510e7b671 : TaiwuEventItem
    {
        /// <summary>
        /// 构造函数 - 初始化事件配置
        /// </summary>
        public YouFangYiEvent_bb15f5e8069a4ca7b497fa1510e7b671()
        {
            // 基础事件配置
            this.Guid = Guid.Parse("bb15f5e8-069a-4ca7-b497-fa1510e7b671");
            this.IsHeadEvent = false; // 非头部事件，由其他事件触发
            this.EventGroup = "YaoCaiShengJi"; // 药材升级事件组
            this.ForceSingle = false;
            this.EventType = (EEventType)6;
            this.TriggerType = EventTrigger.None; // 不直接触发，由其他事件跳转
            this.EventSortingOrder = 500;

            // 角色配置
            this.MainRoleKey = "";
            this.TargetRoleKey = "";

            // 界面配置
            this.EventBackground = "";
            this.MaskControl = 0;
            this.MaskTweenTime = 0f;
            this.EscOptionKey = "";

            // 初始化事件选项（只有一个选项）
            this.EventOptions = new TaiwuEventOption[]
            {
                new TaiwuEventOption
                {
                    OptionKey = "Option_-129902647",
                    OptionGuid = "09bbc5d4-fe2b-45cf-b667-499065f53e5c"
                }
            };

            // 初始化选项的回调函数
            this.InitOptions();
        }

        /// <summary>
        /// 初始化选项的回调函数
        /// </summary>
        private void InitOptions()
        {
            // 选项1初始化
            this.EventOptions[0].OnOptionVisibleCheck = new Func<bool>(this.OnOption1VisibleCheck);
            this.EventOptions[0].OnOptionAvailableCheck = new Func<bool>(this.OnOption1AvailableCheck);
            this.EventOptions[0].GetReplacedContent = new Func<string>(this.OnOption1GetReplacedContent);
            this.EventOptions[0].OnOptionSelect = new Func<string>(this.OnOption1Select);
            this.EventOptions[0].GetExtraFormatLanguageKeys =
                new Func<List<string>>(this.Option1GetExtraFormatLanguageKeys);
            this.EventOptions[0].DefaultState = 0;
            this.EventOptions[0].OneTimeOnly = false;
            this.OnOption1Create();
        }

        /// <summary>
        /// 检查事件触发条件
        /// </summary>
        /// <returns>总是返回true，因为此事件由其他事件直接跳转</returns>
        public override bool OnCheckEventCondition()
        {
            return true;
        }

        /// <summary>
        /// 事件进入时执行
        /// </summary>
        public override void OnEventEnter()
        {
            // 不需要特殊处理
        }

        /// <summary>
        /// 事件退出时执行
        /// </summary>
        public override void OnEventExit()
        {
            // 不需要特殊处理
        }

        /// <summary>
        /// 获取替换内容字符串
        /// </summary>
        /// <returns>空字符串</returns>
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
        /// 创建选项1
        /// </summary>
        private void OnOption1Create()
        {
            // 此选项不需要消耗信息，保持为空
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
        /// </summary>
        /// <returns>总是可用</returns>
        private bool OnOption1AvailableCheck()
        {
            return true;
        }

        /// <summary>
        /// 获取选项1的替换内容
        /// </summary>
        /// <returns>空字符串</returns>
        private string OnOption1GetReplacedContent()
        {
            return string.Empty;
        }

        /// <summary>
        /// 选项1被选中时执行 - 赠送药材并跳转到下一个事件
        /// 功能：
        /// 1. 给玩家添加1个ID为143的药材（药材类型5）
        /// 2. 显示获得物品界面
        /// 3. 跳转到下一个事件（GUID: ccd5156d-db62-44b1-baf0-3ce42a7e612b）
        /// </summary>
        /// <returns>下一个事件的GUID</returns>
        private string OnOption1Select()
        {
            // 获取玩家角色
            Character playerCharacter = this.ArgBox.GetCharacter("RoleTaiwu");

            // 给玩家添加药材：类型5，模板ID 143，数量1，无限制
            ItemKey newItem = EventHelper.AddItemToRole(playerCharacter, 5, 143, 1, -1);

            // 显示获得物品的界面
            EventHelper.ShowGetItemPageForItems(
                new List<ValueTuple<ItemKey, int>>
                {
                    new ValueTuple<ItemKey, int>(newItem, 1)
                },
                "",
                this.ArgBox,
                false
            );

            // 跳转到下一个事件
            return "ccd5156d-db62-44b1-baf0-3ce42a7e612b";
        }

        /// <summary>
        /// 获取选项1的额外格式语言键
        /// </summary>
        /// <returns>语言键列表</returns>
        public List<string> Option1GetExtraFormatLanguageKeys()
        {
            return null;
        }

        
    }
}