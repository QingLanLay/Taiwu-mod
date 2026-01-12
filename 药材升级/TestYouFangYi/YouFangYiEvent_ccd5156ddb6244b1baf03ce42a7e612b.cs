using System;
using System.Collections.Generic;
using Config.EventConfig;
using GameData.Domains.TaiwuEvent;
using GameData.Domains.TaiwuEvent.Enum;

namespace Modder_76561198100202539.EventConfig.Taiwu.EventGroup483794014fbc4007b2f13bd8cc1e708e
{
    /// <summary>
    /// 游方医事件 - 剧情进度更新事件
    /// 事件ID: ccd5156d-db62-44b1-baf0-3ce42a7e612b
    /// 功能：更新MOD剧情进度，标志着药材升级流程的完成
    /// </summary>
    public class YouFangYiEvent_ccd5156ddb6244b1baf03ce42a7e612b : TaiwuEventItem
    {
        /// <summary>
        /// 构造函数 - 初始化事件配置
        /// </summary>
        public YouFangYiEvent_ccd5156ddb6244b1baf03ce42a7e612b()
        {
            // 基础事件配置
            this.Guid = Guid.Parse("ccd5156d-db62-44b1-baf0-3ce42a7e612b");
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
                    OptionKey = "Option_1545410644",
                    OptionGuid = "b4369280-4602-4a0a-b244-156dc75302f0"
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
            this.EventOptions[0].GetExtraFormatLanguageKeys = new Func<List<string>>(this.Option1GetExtraFormatLanguageKeys);
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
        /// 选项1被选中时执行 - 更新MOD剧情进度
        /// 功能：将MOD剧情进度设置为1，表示药材升级流程已完成
        /// 注意：在之前的第一个事件中，剧情进度为1是触发条件
        /// 现在设置为1，可能用于标记流程完成或解锁后续内容
        /// </summary>
        /// <returns>空字符串，表示结束当前事件链</returns>
        private string OnOption1Select()
        {
            int storyProgress = 1;
            this.TaiwuEvent.SetModInt("ModStoryProgress", true, storyProgress);
            return string.Empty;
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