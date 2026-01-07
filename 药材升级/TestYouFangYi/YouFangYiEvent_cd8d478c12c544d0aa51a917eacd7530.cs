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
    // Token: 0x02000007 RID: 7
    public class YouFangYiEvent_cd8d478c12c544d0aa51a917eacd7530 : TaiwuEventItem
    {
        // Token: 0x06000081 RID: 129 RVA: 0x00003F48 File Offset: 0x00002148
        public YouFangYiEvent_cd8d478c12c544d0aa51a917eacd7530()
        {
            this.Guid = Guid.Parse("cd8d478c-12c5-44d0-aa51-a917eacd7530");
            this.IsHeadEvent = true;
            this.EventGroup = "CuZhiLing02";
            this.ForceSingle = false;
            this.EventType = (EEventType)6;
            this.TriggerType = EventTrigger.SectBuildingClicked;
            this.EventSortingOrder = 500;
            this.MainRoleKey = "RoleTaiwu";
            this.TargetRoleKey = "YouFangYi";
            this.EventBackground = "";
            this.MaskControl = 0;
            this.MaskTweenTime = 0f;
            this.EscOptionKey = "";
            this.EventOptions = new TaiwuEventOption[]
            {
                new TaiwuEventOption
                {
                    OptionKey = "Option_-1246246002",
                    OptionGuid = "932544a1-245e-4f86-9a8f-ceee47ea5c3c"
                }
            };
            this.InitOptions();
        }

        // Token: 0x06000082 RID: 130 RVA: 0x00004014 File Offset: 0x00002214
        private void InitOptions()
        {
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

        // Token: 0x06000083 RID: 131 RVA: 0x000040C4 File Offset: 0x000022C4
        public override bool OnCheckEventCondition()
        {
            short @short = this.ArgBox.GetShort("BuildingTemplateId");
            int num = -1;
            bool flag = !this.TaiwuEvent.GetModData("ModStoryProgress", true, ref num);
            if (flag)
            {
                num = 0;
                this.TaiwuEvent.SetModInt("ModStoryProgress", true, num);
            }

            return @short == 157 && num == 0;
        }

        // Token: 0x06000084 RID: 132 RVA: 0x00004138 File Offset: 0x00002338
        public override void OnEventEnter()
        {
            var character = EventHelper.CreateTemporaryIntelligentCharacter(
                new Location(EventArgBox.TaiwuVillageAreaId, EventArgBox.TaiwuVillageBlockId)
                , 1, 60, 100,
                EventHelper.GetBelongSettlementId(EventArgBox.TaiwuVillageAreaId, EventArgBox.TaiwuVillageBlockId),
                0
            );

            // 更新角色ID和标记
            this.ArgBox.Set("YouFangYi", character.GetId());
            this.ArgBox.Set(EventArgBox.HideFavorability, true);
            this.ArgBox.Set(EventArgBox.ForbidViewCharacter, true);
            this.ArgBox.Set("HideFavorability", true);
            this.ArgBox.Set("ForbidViewCharacter", true);
                
        }

        // Token: 0x06000085 RID: 133 RVA: 0x00002491 File Offset: 0x00000691
        public override void OnEventExit() { }

        // Token: 0x06000086 RID: 134 RVA: 0x000041AC File Offset: 0x000023AC
        public override string GetReplacedContentString()
        {
            return string.Empty;
        }

        // Token: 0x06000087 RID: 135 RVA: 0x000041C4 File Offset: 0x000023C4
        public override List<string> GetExtraFormatLanguageKeys()
        {
            return null;
        }

        // Token: 0x06000088 RID: 136 RVA: 0x00002491 File Offset: 0x00000691
        private void OnOption1Create() { }

        // Token: 0x06000089 RID: 137 RVA: 0x000041D8 File Offset: 0x000023D8
        private bool OnOption1VisibleCheck()
        {
            return true;
        }

        // Token: 0x0600008A RID: 138 RVA: 0x000041EC File Offset: 0x000023EC
        private bool OnOption1AvailableCheck()
        {
            return true;
        }

        // Token: 0x0600008B RID: 139 RVA: 0x00004200 File Offset: 0x00002400
        private string OnOption1GetReplacedContent()
        {
            return string.Empty;
        }

        // Token: 0x0600008C RID: 140 RVA: 0x00004218 File Offset: 0x00002418
        private string OnOption1Select()
        {
            return "bb15f5e8-069a-4ca7-b497-fa1510e7b671";
        }

        // Token: 0x0600008D RID: 141 RVA: 0x00004230 File Offset: 0x00002430
        public List<string> Option1GetExtraFormatLanguageKeys()
        {
            return null;
        }
    }
}