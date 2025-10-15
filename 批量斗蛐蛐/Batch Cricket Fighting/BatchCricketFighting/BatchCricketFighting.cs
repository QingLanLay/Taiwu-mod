using System;
using System.Collections.Generic;
using Config.EventConfig;
using GameData.Domains;
using GameData.Domains.Character;
using GameData.Domains.TaiwuEvent;
using GameData.Utilities;
using HarmonyLib;
using Traverse = HarmonyLib.Traverse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Config;
using Config.EventConfig;
using GameData.Common;
using GameData.DomainEvents;
using GameData.Domains;
using GameData.Domains.Adventure;
using GameData.Domains.Character;
using GameData.Domains.Character.Relation;
using GameData.Domains.Item;
using GameData.Domains.Map;
using GameData.Domains.Mod;
using GameData.Domains.TaiwuEvent;
using GameData.Domains.TaiwuEvent.DisplayEvent;
using GameData.Domains.TaiwuEvent.Enum;
using GameData.Domains.TaiwuEvent.EventHelper;
using GameData.Domains.TaiwuEvent.EventOption;
using GameData.Domains.World.Notification;
using GameData.Utilities;
using HarmonyLib;
using Modder_76561198100202539.EventConfig.Taiwu.EventGroupe44b04fbc55643ee8b0aa5c7f0c6178a;

namespace Batch_Cricket_Fighting
{
	public class Batch_Cricket_Fighting : EventPackage
	{
		public static string ModId;
		// Token: 0x0600005B RID: 91 RVA: 0x0000343C File Offset: 0x0000163C
		public Batch_Cricket_Fighting()
		{
			base.NameSpace = "Taiwu";
			base.Author = "超级马桶";
			base.Group = "Batch_Cricket_Fighting";
			this.EventList = new List<TaiwuEventItem>
			{
				new FirstModEvent_0e9cadb5327045e3bff794f3616834a5(),
				new FirstModEvent_793b2f76e026475eb6b02b232343cb3c(),
				new FirstModEvent_5037733e7ff445278a4cdd3854dc5933(),
				new FirstModEvent_ed82f1ad7c544dcba5f20e454aa409bc()
			};
		}
	}
	
    public class FirstModEvent_0e9cadb5327045e3bff794f3616834a5 : TaiwuEventItem
	{
		public FirstModEvent_0e9cadb5327045e3bff794f3616834a5()
		{
			//事件GUID
			Guid = Guid.Parse("0e9cadb5-3270-45e3-bff7-94f3616834a5");
			//该事件是否是头部事件
			IsHeadEvent = true;
			//事件分组
			EventGroup = "Batch_Cricket_Fighting";
			//同一时间是否强制只允许触发一个同组事件
			ForceSingle = false;
			//事件类型
			EventType = EEventType.ModEvent;
			//事件触发点类型
			TriggerType = EventTrigger.None;
			//事件排序执行的优先级
			EventSortingOrder = 500;
			//事件内容
			//EventContent = "";
			//进行决策的角色的Key，为空时将不显示选项上的角色头像
			MainRoleKey = "RoleTaiwu";
			//进行交互的角色的Key，为空时将不显示背景图上的角色头像
			TargetRoleKey = "CharacterId";
			//事件背景图，为空表示该事件使用地块适应的背景图
			EventBackground = "";
			//事件对遮罩的控制
			MaskControl = EventMaskControl.NoChange;
			//遮罩渐变的时间
			MaskTweenTime = 0f;
			//绑定在Esc按键处理的选项Key
			EscOptionKey = "Option_-638152354";
			//选项列表
			EventOptions = new TaiwuEventOption[]		        
			{
				new TaiwuEventOption{ OptionKey = "Option_1588548282", OptionGuid = "36ac20b7-07e2-409e-be20-61f0233c6e7c"},
				new TaiwuEventOption{ OptionKey = "Option_-1723899526", OptionGuid = "6ed8c523-0252-4740-9351-384778476ba5"},
				new TaiwuEventOption{ OptionKey = "Option_-638152354", OptionGuid = "24f5bde0-a65a-40c3-a60e-e5ff9e427527"}
			};
			InitOptions();
		}


		#region EventAPI
		private void InitOptions()
		{
			EventOptions[0].OnOptionVisibleCheck = OnOption1VisibleCheck;
			EventOptions[0].OnOptionAvailableCheck = OnOption1AvailableCheck;
			EventOptions[0].GetReplacedContent = OnOption1GetReplacedContent;
			EventOptions[0].OnOptionSelect = OnOption1Select;
			EventOptions[0].GetExtraFormatLanguageKeys = Option1GetExtraFormatLanguageKeys;
			EventOptions[0].DefaultState = EventOptionState.Normal;
			EventOptions[0].OneTimeOnly = false;
			OnOption1Create();

			EventOptions[1].OnOptionVisibleCheck = OnOption2VisibleCheck;
			EventOptions[1].OnOptionAvailableCheck = OnOption2AvailableCheck;
			EventOptions[1].GetReplacedContent = OnOption2GetReplacedContent;
			EventOptions[1].OnOptionSelect = OnOption2Select;
			EventOptions[1].GetExtraFormatLanguageKeys = Option2GetExtraFormatLanguageKeys;
			EventOptions[1].DefaultState = EventOptionState.Normal;
			EventOptions[1].OneTimeOnly = false;
			OnOption2Create();

			EventOptions[2].OnOptionVisibleCheck = OnOption3VisibleCheck;
			EventOptions[2].OnOptionAvailableCheck = OnOption3AvailableCheck;
			EventOptions[2].GetReplacedContent = OnOption3GetReplacedContent;
			EventOptions[2].OnOptionSelect = OnOption3Select;
			EventOptions[2].GetExtraFormatLanguageKeys = Option3GetExtraFormatLanguageKeys;
			EventOptions[2].DefaultState = EventOptionState.Normal;
			EventOptions[2].OneTimeOnly = false;
			OnOption3Create();
		}

		public override bool OnCheckEventCondition()
		{
			return true;
		}

		public override void OnEventEnter()
		{
		}

		public override void OnEventExit()
		{
		}

		public override string GetReplacedContentString()
		{
			return "太吾" +"亮出几罐白玉镶金的促织罐，只闻虫鸣寰宇，须臾间人声鼎沸，\n " +
			       $"但见樵夫挑着八宝蛐蛐罐健步如飞，渔翁擎着紫竹探笼踏浪而来。\n" +
			       $"只听得一声长笑：“太吾既想斗虫，某家便唤来乡里三五壮士，\n " +
			       $"且看这村野群豪的草莽战将，可敢与太吾的脱骨惊秋一决雌雄！”";
		}

		public override List<string> GetExtraFormatLanguageKeys()
		{
		    return default;
		}


		#endregion

		#region Options
		private void OnOption1Create()
		{
		}

		private bool OnOption1VisibleCheck()
		{
			return true;
		}

		private bool OnOption1AvailableCheck()
		{
			return true;
		}

		private string OnOption1GetReplacedContent()
		{
			return "好，我这几只无名小虫，正缺尔等凡品砥砺锋芒！";
		}

		private string OnOption1Select()
		{
			Dictionary<ItemKey, int> inventoryItems = DomainManager.Character.GetElement_Objects(EventArgBox.TaiwuCharacterId).GetInventory().Items;
			List<ItemKey> cricketList = new List<ItemKey>();
			foreach (ItemKey itemKey in inventoryItems.Keys)
			{
				bool flag = itemKey.ItemType == 11;
				if (flag)
				{
					CricketData item = DomainManager.Item.GetCricketData(itemKey.Id);
					GameData.Domains.Item.Cricket cricket = DomainManager.Item.GetElement_Crickets(itemKey.Id);
					bool flag1 = cricket.IsAlive && item.IsSmart;
					if (flag1)
					{
						cricketList.Add(itemKey);
					}
				}
			}
			if (cricketList.Count > 0 && cricketList != null )
			{
				return "5037733e-7ff4-4527-8a4c-dd3854dc5933";
			}
			else
			{
				return "ed82f1ad-7c54-4dcb-a5f2-0e454aa409bc";
			}
		}

		public List<string> Option1GetExtraFormatLanguageKeys()
		{
		
		    return default;
		}

		private void OnOption2Create()
		{
		}

		private bool OnOption2VisibleCheck()
		{
			return true;
		}

		private bool OnOption2AvailableCheck()
		{
			return true;
		}

		private string OnOption2GetReplacedContent()
		{
			return "明日再说，今日已有安排。";
		}

		private string OnOption2Select()
		{
			return string.Empty;
		}

		public List<string> Option2GetExtraFormatLanguageKeys()
		{
		    return default;
		}

		private void OnOption3Create()
		{
		}

		private bool OnOption3VisibleCheck()
		{
			return false;
		}

		private bool OnOption3AvailableCheck()
		{
			return true;
		}

		private string OnOption3GetReplacedContent()
		{
			return "选项3";
		}

		private string OnOption3Select()
		{
			int selectNextMonth = 1;
			TaiwuEvent.SetModInt("SelectNextMonth", true,selectNextMonth);
			return string.Empty;
		}

		public List<string> Option3GetExtraFormatLanguageKeys()
		{
		    return default;
		}


		#endregion
	}

}