using System;
using System.Collections.Generic;
using Config.EventConfig;
using GameData.Domains.Character;
using GameData.Domains.Map;
using GameData.Domains.Organization;
using GameData.Domains.TaiwuEvent;
using GameData.Domains.TaiwuEvent.Enum;
using GameData.Domains.TaiwuEvent.EventHelper;

namespace Modder_76561198100202539.EventConfig.Taiwu.EventGroup483794014fbc4007b2f13bd8cc1e708e
{
    /// <summary>
    /// 游方医事件 - 初始触发事件
    /// 事件ID: cd8d478c-12c5-44d0-aa51-a917eacd7530
    /// 功能：药材升级流程的起始事件，初始化MOD剧情进度并创建游方医角色
    /// </summary>
    public class YouFangYiEvent_cd8d478c12c544d0aa51a917eacd7530 : TaiwuEventItem
    {
        /// <summary>
        /// 构造函数 - 初始化事件配置
        /// </summary>
        public YouFangYiEvent_cd8d478c12c544d0aa51a917eacd7530()
        {
            // 基础事件配置
            this.Guid = Guid.Parse("cd8d478c-12c5-44d0-aa51-a917eacd7530");
            this.IsHeadEvent = true; // 头部事件，可直接触发
            this.EventGroup = "YaoCaiShengJi"; // 药材升级事件组
            this.ForceSingle = false;
            this.EventType = (EEventType)6;
            this.TriggerType = EventTrigger.SectBuildingClicked; // 点击门派建筑时触发
            this.EventSortingOrder = 500;
            
            // 角色配置
            this.MainRoleKey = "RoleTaiwu";
            this.TargetRoleKey = "YouFangYi";
            
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
                    OptionKey = "Option_-1246246002",
                    OptionGuid = "932544a1-245e-4f86-9a8f-ceee47ea5c3c"
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
        /// 条件：
        /// 1. 点击的建筑模板ID为157
        /// 2. MOD剧情进度为0（初次触发）或未设置（初始化）
        /// </summary>
        /// <returns>是否满足触发条件</returns>
        public override bool OnCheckEventCondition()
        {
            // 获取被点击的建筑模板ID
            short buildingTemplateId = this.ArgBox.GetShort("BuildingTemplateId");
            
            // 获取MOD剧情进度
            int storyProgress = -1;
            bool hasStoryProgress = this.TaiwuEvent.GetModData("ModStoryProgress", true, ref storyProgress);
            
            // 如果MOD剧情进度不存在，则初始化为0
            if (!hasStoryProgress)
            {
                storyProgress = 0;
                this.TaiwuEvent.SetModInt("ModStoryProgress", true, storyProgress);
            }
            
            // 触发条件：建筑ID为157且剧情进度为0（初次触发状态）
            return buildingTemplateId == 157 && storyProgress == 0;
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
                1, // 性别：未知或默认
                60, // 年龄：60岁
                100, // 健康度：100%
                EventHelper.GetBelongSettlementId(EventArgBox.TaiwuVillageAreaId, EventArgBox.TaiwuVillageBlockId),
                0 // 阵营或其他标识
            );

            // 更新角色ID和标记
            this.ArgBox.Set("YouFangYi", character.GetId());
            
            // 设置界面隐藏标记
            this.ArgBox.Set(EventArgBox.HideFavorability, true); // 隐藏好感度
            this.ArgBox.Set(EventArgBox.ForbidViewCharacter, true); // 禁止查看角色
            this.ArgBox.Set("HideFavorability", true);
            this.ArgBox.Set("ForbidViewCharacter", true);
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
        /// 选项1被选中时执行 - 跳转到下一个事件
        /// 功能：跳转到赠送药材事件（bb15f5e8-069a-4ca7-b497-fa1510e7b671）
        /// 这是整个药材升级流程的第一步
        /// </summary>
        /// <returns>下一个事件的GUID</returns>
        private string OnOption1Select()
        {
            return "bb15f5e8-069a-4ca7-b497-fa1510e7b671";
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