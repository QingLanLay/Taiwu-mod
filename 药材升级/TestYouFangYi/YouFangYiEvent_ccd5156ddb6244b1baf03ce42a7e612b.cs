using System;
using System.Collections.Generic;
using Config.EventConfig;
using GameData.Domains.TaiwuEvent;
using GameData.Domains.TaiwuEvent.Enum;

namespace Modder_76561198100202539.EventConfig.Taiwu.EventGroup483794014fbc4007b2f13bd8cc1e708e
{
	// Token: 0x02000006 RID: 6
	public class YouFangYiEvent_ccd5156ddb6244b1baf03ce42a7e612b : TaiwuEventItem
	{
		// Token: 0x06000074 RID: 116 RVA: 0x00003D0C File Offset: 0x00001F0C
		public YouFangYiEvent_ccd5156ddb6244b1baf03ce42a7e612b()
		{
			this.Guid = Guid.Parse("ccd5156d-db62-44b1-baf0-3ce42a7e612b");
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
					OptionKey = "Option_1545410644",
					OptionGuid = "b4369280-4602-4a0a-b244-156dc75302f0"
				}
			};
			this.InitOptions();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003DD8 File Offset: 0x00001FD8
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

		// Token: 0x06000076 RID: 118 RVA: 0x00003E88 File Offset: 0x00002088
		public override bool OnCheckEventCondition()
		{
			return true;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002491 File Offset: 0x00000691
		public override void OnEventEnter()
		{
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002491 File Offset: 0x00000691
		public override void OnEventExit()
		{
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003E9C File Offset: 0x0000209C
		public override string GetReplacedContentString()
		{
			return string.Empty;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003EB4 File Offset: 0x000020B4
		public override List<string> GetExtraFormatLanguageKeys()
		{
			return null;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002491 File Offset: 0x00000691
		private void OnOption1Create()
		{
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003EC8 File Offset: 0x000020C8
		private bool OnOption1VisibleCheck()
		{
			return true;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003EDC File Offset: 0x000020DC
		private bool OnOption1AvailableCheck()
		{
			return true;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003EF0 File Offset: 0x000020F0
		private string OnOption1GetReplacedContent()
		{
			return string.Empty;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003F08 File Offset: 0x00002108
		private string OnOption1Select()
		{
			int num = 1;
			this.TaiwuEvent.SetModInt("ModStoryProgress", true, num);
			return string.Empty;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003F34 File Offset: 0x00002134
		public List<string> Option1GetExtraFormatLanguageKeys()
		{
			return null;
		}
	}
}
