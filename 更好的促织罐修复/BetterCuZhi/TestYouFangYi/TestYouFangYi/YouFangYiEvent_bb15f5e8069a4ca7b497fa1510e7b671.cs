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
	// Token: 0x02000005 RID: 5
	public class YouFangYiEvent_bb15f5e8069a4ca7b497fa1510e7b671 : TaiwuEventItem
	{
		// Token: 0x06000067 RID: 103 RVA: 0x00003AA0 File Offset: 0x00001CA0
		public YouFangYiEvent_bb15f5e8069a4ca7b497fa1510e7b671()
		{
			this.Guid = Guid.Parse("bb15f5e8-069a-4ca7-b497-fa1510e7b671");
			this.IsHeadEvent = false;
			this.EventGroup = "CuZhiLing02";
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
			this.EventOptions = new TaiwuEventOption[]
			{
				new TaiwuEventOption
				{
					OptionKey = "Option_-129902647",
					OptionGuid = "09bbc5d4-fe2b-45cf-b667-499065f53e5c"
				}
			};
			this.InitOptions();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003B6C File Offset: 0x00001D6C
		private void InitOptions()
		{
			this.EventOptions[0].OnOptionVisibleCheck = new Func<bool>(this.OnOption1VisibleCheck);
			this.EventOptions[0].OnOptionAvailableCheck = new Func<bool>(this.OnOption1AvailableCheck);
			this.EventOptions[0].GetReplacedContent = new Func<string>(this.OnOption1GetReplacedContent);
			this.EventOptions[0].OnOptionSelect = new Func<string>(this.OnOption1Select);
			this.EventOptions[0].GetExtraFormatLanguageKeys = new Func<List<string>>(this.Option1GetExtraFormatLanguageKeys);
			this.EventOptions[0].DefaultState = 0;
			this.EventOptions[0].OneTimeOnly = false;
			this.OnOption1Create();
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003C1C File Offset: 0x00001E1C
		public override bool OnCheckEventCondition()
		{
			return true;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002491 File Offset: 0x00000691
		public override void OnEventEnter()
		{
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002491 File Offset: 0x00000691
		public override void OnEventExit()
		{
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003C30 File Offset: 0x00001E30
		public override string GetReplacedContentString()
		{
			return string.Empty;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003C48 File Offset: 0x00001E48
		public override List<string> GetExtraFormatLanguageKeys()
		{
			return null;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002491 File Offset: 0x00000691
		private void OnOption1Create()
		{
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003C5C File Offset: 0x00001E5C
		private bool OnOption1VisibleCheck()
		{
			return true;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003C70 File Offset: 0x00001E70
		private bool OnOption1AvailableCheck()
		{
			return true;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003C84 File Offset: 0x00001E84
		private string OnOption1GetReplacedContent()
		{
			return string.Empty;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003C9C File Offset: 0x00001E9C
		private string OnOption1Select()
		{
			Character character = this.ArgBox.GetCharacter("RoleTaiwu");
			ItemKey item = EventHelper.AddItemToRole(character, 5, 143, 1, -1);
			EventHelper.ShowGetItemPageForItems(new List<ValueTuple<ItemKey, int>>
			{
				new ValueTuple<ItemKey, int>(item, 1)
			}, "", this.ArgBox, false);
			return "ccd5156d-db62-44b1-baf0-3ce42a7e612b";
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003CF8 File Offset: 0x00001EF8
		public List<string> Option1GetExtraFormatLanguageKeys()
		{
			return null;
		}
	}
}
