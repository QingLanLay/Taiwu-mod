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
	// Token: 0x02000004 RID: 4
	public class YouFangYiEvent_72c17bd700b34cec8aa316a732a6d3b0 : TaiwuEventItem
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002A70 File Offset: 0x00000C70
		public YouFangYiEvent_72c17bd700b34cec8aa316a732a6d3b0()
		{
			this.Guid = Guid.Parse("72c17bd7-00b3-4cec-8aa3-16a732a6d3b0");
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
					OptionKey = "Option_-1126735797",
					OptionGuid = "613e94a9-d9e1-4511-b3a1-7e0a0d0e960c"
				},
				new TaiwuEventOption
				{
					OptionKey = "Option_-891361317",
					OptionGuid = "e267f16b-6fd3-4f4b-a68f-3b898aa8e00e"
				},
				new TaiwuEventOption
				{
					OptionKey = "Option_-893284401",
					OptionGuid = "59cc595d-92e0-473b-ab7f-a66dcf895990"
				},
				new TaiwuEventOption
				{
					OptionKey = "Option_-857513030",
					OptionGuid = "6b8c0036-2797-4615-ad15-09447c59d5ca"
				},
				new TaiwuEventOption
				{
					OptionKey = "Option_1936792895",
					OptionGuid = "ad824dc5-ac17-4b55-91cb-767f29fa64c4"
				},
				new TaiwuEventOption
				{
					OptionKey = "Option_1112125176",
					OptionGuid = "133cd4a6-5599-45b2-8498-c7649be1ecc7"
				},
				new TaiwuEventOption
				{
					OptionKey = "Option_1518141782",
					OptionGuid = "af3ba0d3-1378-4918-bb0c-8f0978255466"
				},
				new TaiwuEventOption
				{
					OptionKey = "Option_627222926",
					OptionGuid = "97ada584-78bf-4f06-a3ec-a55ac7c9843a"
				},
				new TaiwuEventOption
				{
					OptionKey = "Option_1378749027",
					OptionGuid = "ced937b4-ceef-4b5e-803d-61d5a4e23256"
				},
				new TaiwuEventOption
				{
					OptionKey = "Option_-1194893333",
					OptionGuid = "a2c7de43-3037-4bc7-aa79-56468736ad8c"
				},
				new TaiwuEventOption
				{
					OptionKey = "Option_1602323249",
					OptionGuid = "ce2ada07-9ca9-4830-bec8-b0064cd19334"
				}
			};
			this.InitOptions();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002C6C File Offset: 0x00000E6C
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
			this.EventOptions[3].OnOptionVisibleCheck = new Func<bool>(this.OnOption4VisibleCheck);
			this.EventOptions[3].OnOptionAvailableCheck = new Func<bool>(this.OnOption4AvailableCheck);
			this.EventOptions[3].GetReplacedContent = new Func<string>(this.OnOption4GetReplacedContent);
			this.EventOptions[3].OnOptionSelect = new Func<string>(this.OnOption4Select);
			this.EventOptions[3].GetExtraFormatLanguageKeys = new Func<List<string>>(this.Option4GetExtraFormatLanguageKeys);
			this.EventOptions[3].DefaultState = 0;
			this.EventOptions[3].OneTimeOnly = false;
			this.OnOption4Create();
			this.EventOptions[4].OnOptionVisibleCheck = new Func<bool>(this.OnOption5VisibleCheck);
			this.EventOptions[4].OnOptionAvailableCheck = new Func<bool>(this.OnOption5AvailableCheck);
			this.EventOptions[4].GetReplacedContent = new Func<string>(this.OnOption5GetReplacedContent);
			this.EventOptions[4].OnOptionSelect = new Func<string>(this.OnOption5Select);
			this.EventOptions[4].GetExtraFormatLanguageKeys = new Func<List<string>>(this.Option5GetExtraFormatLanguageKeys);
			this.EventOptions[4].DefaultState = 0;
			this.EventOptions[4].OneTimeOnly = false;
			this.OnOption5Create();
			this.EventOptions[5].OnOptionVisibleCheck = new Func<bool>(this.OnOption6VisibleCheck);
			this.EventOptions[5].OnOptionAvailableCheck = new Func<bool>(this.OnOption6AvailableCheck);
			this.EventOptions[5].GetReplacedContent = new Func<string>(this.OnOption6GetReplacedContent);
			this.EventOptions[5].OnOptionSelect = new Func<string>(this.OnOption6Select);
			this.EventOptions[5].GetExtraFormatLanguageKeys = new Func<List<string>>(this.Option6GetExtraFormatLanguageKeys);
			this.EventOptions[5].DefaultState = 0;
			this.EventOptions[5].OneTimeOnly = false;
			this.OnOption6Create();
			this.EventOptions[6].OnOptionVisibleCheck = new Func<bool>(this.OnOption7VisibleCheck);
			this.EventOptions[6].OnOptionAvailableCheck = new Func<bool>(this.OnOption7AvailableCheck);
			this.EventOptions[6].GetReplacedContent = new Func<string>(this.OnOption7GetReplacedContent);
			this.EventOptions[6].OnOptionSelect = new Func<string>(this.OnOption7Select);
			this.EventOptions[6].GetExtraFormatLanguageKeys = new Func<List<string>>(this.Option7GetExtraFormatLanguageKeys);
			this.EventOptions[6].DefaultState = 0;
			this.EventOptions[6].OneTimeOnly = false;
			this.OnOption7Create();
			this.EventOptions[7].OnOptionVisibleCheck = new Func<bool>(this.OnOption8VisibleCheck);
			this.EventOptions[7].OnOptionAvailableCheck = new Func<bool>(this.OnOption8AvailableCheck);
			this.EventOptions[7].GetReplacedContent = new Func<string>(this.OnOption8GetReplacedContent);
			this.EventOptions[7].OnOptionSelect = new Func<string>(this.OnOption8Select);
			this.EventOptions[7].GetExtraFormatLanguageKeys = new Func<List<string>>(this.Option8GetExtraFormatLanguageKeys);
			this.EventOptions[7].DefaultState = 0;
			this.EventOptions[7].OneTimeOnly = false;
			this.OnOption8Create();
			this.EventOptions[8].OnOptionVisibleCheck = new Func<bool>(this.OnOption9VisibleCheck);
			this.EventOptions[8].OnOptionAvailableCheck = new Func<bool>(this.OnOption9AvailableCheck);
			this.EventOptions[8].GetReplacedContent = new Func<string>(this.OnOption9GetReplacedContent);
			this.EventOptions[8].OnOptionSelect = new Func<string>(this.OnOption9Select);
			this.EventOptions[8].GetExtraFormatLanguageKeys = new Func<List<string>>(this.Option9GetExtraFormatLanguageKeys);
			this.EventOptions[8].DefaultState = 0;
			this.EventOptions[8].OneTimeOnly = false;
			this.OnOption9Create();
			this.EventOptions[9].OnOptionVisibleCheck = new Func<bool>(this.OnOption10VisibleCheck);
			this.EventOptions[9].OnOptionAvailableCheck = new Func<bool>(this.OnOption10AvailableCheck);
			this.EventOptions[9].GetReplacedContent = new Func<string>(this.OnOption10GetReplacedContent);
			this.EventOptions[9].OnOptionSelect = new Func<string>(this.OnOption10Select);
			this.EventOptions[9].GetExtraFormatLanguageKeys = new Func<List<string>>(this.Option10GetExtraFormatLanguageKeys);
			this.EventOptions[9].DefaultState = 0;
			this.EventOptions[9].OneTimeOnly = false;
			this.OnOption10Create();
			this.EventOptions[10].OnOptionVisibleCheck = new Func<bool>(this.OnOption11VisibleCheck);
			this.EventOptions[10].OnOptionAvailableCheck = new Func<bool>(this.OnOption11AvailableCheck);
			this.EventOptions[10].GetReplacedContent = new Func<string>(this.OnOption11GetReplacedContent);
			this.EventOptions[10].OnOptionSelect = new Func<string>(this.OnOption11Select);
			this.EventOptions[10].GetExtraFormatLanguageKeys = new Func<List<string>>(this.Option11GetExtraFormatLanguageKeys);
			this.EventOptions[10].DefaultState = 0;
			this.EventOptions[10].OneTimeOnly = false;
			this.OnOption11Create();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00003368 File Offset: 0x00001568
		public override bool OnCheckEventCondition()
		{
			return true;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002491 File Offset: 0x00000691
		public override void OnEventEnter()
		{
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002491 File Offset: 0x00000691
		public override void OnEventExit()
		{
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000337C File Offset: 0x0000157C
		public override string GetReplacedContentString()
		{
			int num = 0;
			this.ArgBox.Get("addNewGrade", ref num);
			return string.Format("可以炼出{0}株上等药材。", num);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000033B4 File Offset: 0x000015B4
		public override List<string> GetExtraFormatLanguageKeys()
		{
			return null;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002491 File Offset: 0x00000691
		private void OnOption1Create()
		{
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000033C8 File Offset: 0x000015C8
		private bool OnOption1VisibleCheck()
		{
			return true;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000033DC File Offset: 0x000015DC
		private bool OnOption1AvailableCheck()
		{
			return true;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000033F0 File Offset: 0x000015F0
		private string OnOption1GetReplacedContent()
		{
			return string.Empty;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00003408 File Offset: 0x00001608
		private string OnOption1Select()
		{
			this.AddNewGrade(234, 235);
			return string.Empty;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00003430 File Offset: 0x00001630
		private bool IsValidNumber(int x, int y)
		{
			return x >= 140 && x <= 235 && (x - 140) % 4 == y;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003464 File Offset: 0x00001664
		public void AddNewGrade(short qi, short jue)
		{
			int num = 0;
			this.ArgBox.Get("GradeReq", ref num);
			int num2 = 0;
			Character character = this.ArgBox.GetCharacter("RoleTaiwu");
			Inventory inventory = character.GetInventory();
			foreach (KeyValuePair<ItemKey, int> keyValuePair in inventory.Items)
			{
				ItemKey key = keyValuePair.Key;
				int value = keyValuePair.Value;
				bool flag = key.ItemType == 5;
				if (flag)
				{
					short templateId = key.TemplateId;
					bool flag2 = this.IsValidNumber((int)templateId, num);
					bool flag3 = flag2;
					if (flag3)
					{
						num2 += value;
						EventHelper.RemoveInventoryItem(character, key, value, true);
					}
				}
			}
			bool flag4 = num == 1;
			if (flag4)
			{
				int num3 = num2 / 3;
				ItemKey item = EventHelper.AddItemToRole(character, 5, qi, num3, -1);
				EventHelper.ShowGetItemPageForItems(new List<ValueTuple<ItemKey, int>>
				{
					new ValueTuple<ItemKey, int>(item, num3)
				}, "", this.ArgBox, false);
			}
			else
			{
				bool flag5 = num == 2;
				if (flag5)
				{
					int num3 = num2 / 5;
					ItemKey item2 = EventHelper.AddItemToRole(character, 5, jue, num3, -1);
					EventHelper.ShowGetItemPageForItems(new List<ValueTuple<ItemKey, int>>
					{
						new ValueTuple<ItemKey, int>(item2, num3)
					}, "", this.ArgBox, false);
				}
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000035C4 File Offset: 0x000017C4
		public List<string> Option1GetExtraFormatLanguageKeys()
		{
			return null;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002491 File Offset: 0x00000691
		private void OnOption2Create()
		{
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000035D8 File Offset: 0x000017D8
		private bool OnOption2VisibleCheck()
		{
			return true;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000035EC File Offset: 0x000017EC
		private bool OnOption2AvailableCheck()
		{
			return true;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003600 File Offset: 0x00001800
		private string OnOption2GetReplacedContent()
		{
			return string.Empty;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003618 File Offset: 0x00001818
		private string OnOption2Select()
		{
			this.AddNewGrade(222, 223);
			return string.Empty;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003640 File Offset: 0x00001840
		public List<string> Option2GetExtraFormatLanguageKeys()
		{
			return null;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002491 File Offset: 0x00000691
		private void OnOption3Create()
		{
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003654 File Offset: 0x00001854
		private bool OnOption3VisibleCheck()
		{
			return true;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003668 File Offset: 0x00001868
		private bool OnOption3AvailableCheck()
		{
			return true;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000367C File Offset: 0x0000187C
		private string OnOption3GetReplacedContent()
		{
			return string.Empty;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003694 File Offset: 0x00001894
		private string OnOption3Select()
		{
			this.AddNewGrade(150, 151);
			return string.Empty;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000036BC File Offset: 0x000018BC
		public List<string> Option3GetExtraFormatLanguageKeys()
		{
			return null;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002491 File Offset: 0x00000691
		private void OnOption4Create()
		{
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000036D0 File Offset: 0x000018D0
		private bool OnOption4VisibleCheck()
		{
			return true;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000036E4 File Offset: 0x000018E4
		private bool OnOption4AvailableCheck()
		{
			return true;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000036F8 File Offset: 0x000018F8
		private string OnOption4GetReplacedContent()
		{
			return string.Empty;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003710 File Offset: 0x00001910
		private string OnOption4Select()
		{
			this.AddNewGrade(166, 167);
			return string.Empty;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003738 File Offset: 0x00001938
		public List<string> Option4GetExtraFormatLanguageKeys()
		{
			return null;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002491 File Offset: 0x00000691
		private void OnOption5Create()
		{
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000374C File Offset: 0x0000194C
		private bool OnOption5VisibleCheck()
		{
			return true;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003760 File Offset: 0x00001960
		private bool OnOption5AvailableCheck()
		{
			return true;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003774 File Offset: 0x00001974
		private string OnOption5GetReplacedContent()
		{
			return string.Empty;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000378C File Offset: 0x0000198C
		private string OnOption5Select()
		{
			this.AddNewGrade(154, 155);
			return string.Empty;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000037B4 File Offset: 0x000019B4
		public List<string> Option5GetExtraFormatLanguageKeys()
		{
			return null;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002491 File Offset: 0x00000691
		private void OnOption6Create()
		{
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000037C8 File Offset: 0x000019C8
		private bool OnOption6VisibleCheck()
		{
			return true;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000037DC File Offset: 0x000019DC
		private bool OnOption6AvailableCheck()
		{
			return true;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000037F0 File Offset: 0x000019F0
		private string OnOption6GetReplacedContent()
		{
			return string.Empty;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003808 File Offset: 0x00001A08
		private string OnOption6Select()
		{
			this.AddNewGrade(186, 187);
			return string.Empty;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003830 File Offset: 0x00001A30
		public List<string> Option6GetExtraFormatLanguageKeys()
		{
			return null;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002491 File Offset: 0x00000691
		private void OnOption7Create()
		{
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003844 File Offset: 0x00001A44
		private bool OnOption7VisibleCheck()
		{
			return true;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003858 File Offset: 0x00001A58
		private bool OnOption7AvailableCheck()
		{
			return true;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000386C File Offset: 0x00001A6C
		private string OnOption7GetReplacedContent()
		{
			return string.Empty;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003884 File Offset: 0x00001A84
		private string OnOption7Select()
		{
			this.AddNewGrade(218, 219);
			return string.Empty;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000038AC File Offset: 0x00001AAC
		public List<string> Option7GetExtraFormatLanguageKeys()
		{
			return null;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002491 File Offset: 0x00000691
		private void OnOption8Create()
		{
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000038C0 File Offset: 0x00001AC0
		private bool OnOption8VisibleCheck()
		{
			return true;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000038D4 File Offset: 0x00001AD4
		private bool OnOption8AvailableCheck()
		{
			return true;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000038E8 File Offset: 0x00001AE8
		private string OnOption8GetReplacedContent()
		{
			return string.Empty;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003900 File Offset: 0x00001B00
		private string OnOption8Select()
		{
			this.AddNewGrade(182, 183);
			return string.Empty;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003928 File Offset: 0x00001B28
		public List<string> Option8GetExtraFormatLanguageKeys()
		{
			return null;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002491 File Offset: 0x00000691
		private void OnOption9Create()
		{
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000393C File Offset: 0x00001B3C
		private bool OnOption9VisibleCheck()
		{
			return true;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003950 File Offset: 0x00001B50
		private bool OnOption9AvailableCheck()
		{
			return true;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003964 File Offset: 0x00001B64
		private string OnOption9GetReplacedContent()
		{
			return string.Empty;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000397C File Offset: 0x00001B7C
		private string OnOption9Select()
		{
			this.AddNewGrade(198, 199);
			return string.Empty;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000039A4 File Offset: 0x00001BA4
		public List<string> Option9GetExtraFormatLanguageKeys()
		{
			return null;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002491 File Offset: 0x00000691
		private void OnOption10Create()
		{
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000039B8 File Offset: 0x00001BB8
		private bool OnOption10VisibleCheck()
		{
			return true;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000039CC File Offset: 0x00001BCC
		private bool OnOption10AvailableCheck()
		{
			return true;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000039E0 File Offset: 0x00001BE0
		private string OnOption10GetReplacedContent()
		{
			return string.Empty;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000039F8 File Offset: 0x00001BF8
		private string OnOption10Select()
		{
			this.AddNewGrade(214, 215);
			return string.Empty;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003A20 File Offset: 0x00001C20
		public List<string> Option10GetExtraFormatLanguageKeys()
		{
			return null;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002491 File Offset: 0x00000691
		private void OnOption11Create()
		{
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003A34 File Offset: 0x00001C34
		private bool OnOption11VisibleCheck()
		{
			return true;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003A48 File Offset: 0x00001C48
		private bool OnOption11AvailableCheck()
		{
			return true;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003A5C File Offset: 0x00001C5C
		private string OnOption11GetReplacedContent()
		{
			return string.Empty;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003A74 File Offset: 0x00001C74
		private string OnOption11Select()
		{
			return "2bd02acd-244e-4f81-a194-502a0389c3f6";
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003A8C File Offset: 0x00001C8C
		public List<string> Option11GetExtraFormatLanguageKeys()
		{
			return null;
		}
	}
}
