using System;
using System.Collections.Generic;
using Config.EventConfig;
using GameData.Domains.Character;
using GameData.Domains.Item;
using GameData.Domains.TaiwuEvent;
using GameData.Domains.TaiwuEvent.Enum;
using GameData.Domains.TaiwuEvent.EventHelper;
using GameData.Domains.TaiwuEvent.EventOption;

namespace Modder_76561198100202539.EventConfig.Taiwu.EventGroup483794014fbc4007b2f13bd8cc1e708e
{
	// Token: 0x02000003 RID: 3
	public class YouFangYiEvent_2bd02acd244e4f81a194502a0389c3f6 : TaiwuEventItem
	{
		// Token: 0x06000002 RID: 2 RVA: 0x000020D0 File Offset: 0x000002D0
		public YouFangYiEvent_2bd02acd244e4f81a194502a0389c3f6()
		{
			this.Guid = Guid.Parse("2bd02acd-244e-4f81-a194-502a0389c3f6");
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
					OptionKey = "Option_-525764982",
					OptionGuid = "a8912f78-4fca-48af-9ac0-26a5a879318c"
				},
				new TaiwuEventOption
				{
					OptionKey = "Option_-655991711",
					OptionGuid = "28a9c07e-0478-44e0-acee-c5e42584efd9"
				},
				new TaiwuEventOption
				{
					OptionKey = "Option_-125410720",
					OptionGuid = "2897918a-1f66-4ba6-92d0-f01afb9b53c4"
				}
			};
			this.InitOptions();
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000021D8 File Offset: 0x000003D8
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
			this.EventOptions[1].OnOptionVisibleCheck = new Func<bool>(this.OnOption2VisibleCheck);
			this.EventOptions[1].OnOptionAvailableCheck = new Func<bool>(this.OnOption2AvailableCheck);
			this.EventOptions[1].GetReplacedContent = new Func<string>(this.OnOption2GetReplacedContent);
			this.EventOptions[1].OnOptionSelect = new Func<string>(this.OnOption2Select);
			this.EventOptions[1].GetExtraFormatLanguageKeys = new Func<List<string>>(this.Option2GetExtraFormatLanguageKeys);
			this.EventOptions[1].DefaultState = 0;
			this.EventOptions[1].OneTimeOnly = false;
			this.OnOption2Create();
			this.EventOptions[2].OnOptionVisibleCheck = new Func<bool>(this.OnOption3VisibleCheck);
			this.EventOptions[2].OnOptionAvailableCheck = new Func<bool>(this.OnOption3AvailableCheck);
			this.EventOptions[2].GetReplacedContent = new Func<string>(this.OnOption3GetReplacedContent);
			this.EventOptions[2].OnOptionSelect = new Func<string>(this.OnOption3Select);
			this.EventOptions[2].GetExtraFormatLanguageKeys = new Func<List<string>>(this.Option3GetExtraFormatLanguageKeys);
			this.EventOptions[2].DefaultState = 0;
			this.EventOptions[2].OneTimeOnly = false;
			this.OnOption3Create();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000023C8 File Offset: 0x000005C8
		public override bool OnCheckEventCondition()
		{
			short @short = this.ArgBox.GetShort("BuildingTemplateId");
			int num = -1;
			this.TaiwuEvent.GetModData("ModStoryProgress", true, ref num);
			bool flag = num == 1;
			if (flag)
			{
				bool flag2 = @short == 157;
				if (flag2)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002420 File Offset: 0x00000620
		public override void OnEventEnter()
		{
			int num = 0;
			bool flag = !this.ArgBox.Get("YouFangYi", ref num);
			if (flag)
			{
				var templateId = EventHelper.GetRandom(Config.EventActors.DefKey.NestBoy, Config.EventActors.DefKey.SectSickXuehou);
				EventHelper.CreateAdventureActor((short)templateId,"YouFangYi",ArgBox);
				this.ArgBox.Set("HideFavorability", true);
				this.ArgBox.Set("ForbidViewCharacter", true);
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002491 File Offset: 0x00000691
		public override void OnEventExit()
		{
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002494 File Offset: 0x00000694
		public override string GetReplacedContentString()
		{
			return string.Empty;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000024AC File Offset: 0x000006AC
		public override List<string> GetExtraFormatLanguageKeys()
		{
			return null;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000024C0 File Offset: 0x000006C0
		private void OnOption1Create()
		{
			this.EventOptions[0].OptionConsumeInfos = new List<OptionConsumeInfo>();
			this.EventOptions[0].OptionConsumeInfos.Add(new OptionConsumeInfo(8, 10, true));
			this.EventOptions[0].OptionConsumeInfos.Add(new OptionConsumeInfo(7, 1000, true));
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000251C File Offset: 0x0000071C
		private bool OnOption1VisibleCheck()
		{
			return true;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002530 File Offset: 0x00000730
		private bool OnOption1AvailableCheck()
		{
			int num = 0;
			Character character = this.ArgBox.GetCharacter("RoleTaiwu");
			Inventory inventory = character.GetInventory();
			int num2 = 1;
			foreach (KeyValuePair<ItemKey, int> keyValuePair in inventory.Items)
			{
				ItemKey key = keyValuePair.Key;
				int value = keyValuePair.Value;
				bool flag = key.ItemType == 5;
				if (flag)
				{
					short templateId = key.TemplateId;
					bool flag2 = this.IsValidNumber((int)templateId, num2);
					bool flag3 = flag2;
					if (flag3)
					{
						num += value;
					}
				}
			}
			int num3 = num / 3;
			this.ArgBox.Set("GradeReq", num2);
			this.ArgBox.Set("addNewGrade", num3);
			return num3 > 0;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000262C File Offset: 0x0000082C
		private bool IsValidNumber(int x, int y)
		{
			return x >= 140 && x <= 235 && (x - 140) % 4 == y;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002660 File Offset: 0x00000860
		private string OnOption1GetReplacedContent()
		{
			return string.Empty;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002678 File Offset: 0x00000878
		private string OnOption1Select()
		{
			int num = 0;
			Character character = this.ArgBox.GetCharacter("RoleTaiwu");
			Inventory inventory = character.GetInventory();
			int num2 = 1;
			foreach (KeyValuePair<ItemKey, int> keyValuePair in inventory.Items)
			{
				ItemKey key = keyValuePair.Key;
				int value = keyValuePair.Value;
				bool flag = key.ItemType == 5;
				if (flag)
				{
					short templateId = key.TemplateId;
					bool flag2 = this.IsValidNumber((int)templateId, num2);
					bool flag3 = flag2;
					if (flag3)
					{
						num += value;
					}
				}
			}
			int num3 = num / 3;
			this.ArgBox.Set("GradeReq", num2);
			this.ArgBox.Set("addNewGrade", num3);
			return "72c17bd7-00b3-4cec-8aa3-16a732a6d3b0";
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002768 File Offset: 0x00000968
		public List<string> Option1GetExtraFormatLanguageKeys()
		{
			return null;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000277C File Offset: 0x0000097C
		private void OnOption2Create()
		{
			this.EventOptions[1].OptionConsumeInfos = new List<OptionConsumeInfo>();
			this.EventOptions[1].OptionConsumeInfos.Add(new OptionConsumeInfo(8, 30, true));
			this.EventOptions[1].OptionConsumeInfos.Add(new OptionConsumeInfo(7, 5000, true));
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000027D8 File Offset: 0x000009D8
		private bool OnOption2VisibleCheck()
		{
			return true;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000027EC File Offset: 0x000009EC
		private bool OnOption2AvailableCheck()
		{
			int num = 0;
			Character character = this.ArgBox.GetCharacter("RoleTaiwu");
			Inventory inventory = character.GetInventory();
			int num2 = 2;
			foreach (KeyValuePair<ItemKey, int> keyValuePair in inventory.Items)
			{
				ItemKey key = keyValuePair.Key;
				int value = keyValuePair.Value;
				bool flag = key.ItemType == 5;
				if (flag)
				{
					short templateId = key.TemplateId;
					bool flag2 = this.IsValidNumber((int)templateId, num2);
					bool flag3 = flag2;
					if (flag3)
					{
						num += value;
					}
				}
			}
			int num3 = num / 3;
			this.ArgBox.Set("GradeReq", num2);
			this.ArgBox.Set("addNewGrade", num3);
			return num3 > 0;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000028E8 File Offset: 0x00000AE8
		private string OnOption2GetReplacedContent()
		{
			return string.Empty;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002900 File Offset: 0x00000B00
		private string OnOption2Select()
		{
			int num = 0;
			Character character = this.ArgBox.GetCharacter("RoleTaiwu");
			Inventory inventory = character.GetInventory();
			int num2 = 2;
			foreach (KeyValuePair<ItemKey, int> keyValuePair in inventory.Items)
			{
				ItemKey key = keyValuePair.Key;
				int value = keyValuePair.Value;
				bool flag = key.ItemType == 5;
				if (flag)
				{
					short templateId = key.TemplateId;
					bool flag2 = this.IsValidNumber((int)templateId, num2);
					bool flag3 = flag2;
					if (flag3)
					{
						num += value;
					}
				}
			}
			int num3 = num / 5;
			this.ArgBox.Set("GradeReq", num2);
			this.ArgBox.Set("addNewGrade", num3);
			return "72c17bd7-00b3-4cec-8aa3-16a732a6d3b0";
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000029F0 File Offset: 0x00000BF0
		public List<string> Option2GetExtraFormatLanguageKeys()
		{
			return null;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002491 File Offset: 0x00000691
		private void OnOption3Create()
		{
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002A04 File Offset: 0x00000C04
		private bool OnOption3VisibleCheck()
		{
			return true;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002A18 File Offset: 0x00000C18
		private bool OnOption3AvailableCheck()
		{
			return true;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002A2C File Offset: 0x00000C2C
		private string OnOption3GetReplacedContent()
		{
			return string.Empty;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002A44 File Offset: 0x00000C44
		private string OnOption3Select()
		{
			return string.Empty;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002A5C File Offset: 0x00000C5C
		public List<string> Option3GetExtraFormatLanguageKeys()
		{
			return null;
		}
	}
}
