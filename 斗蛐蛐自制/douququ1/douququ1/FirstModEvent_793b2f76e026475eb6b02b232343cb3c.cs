using System;
using System.Linq;
using System.Collections.Generic;
using Config.EventConfig;
using GameData.Domains;
using GameData.Domains.TaiwuEvent;
using GameData.Domains.TaiwuEvent.Enum;
using GameData.Domains.TaiwuEvent.EventHelper;
using GameData.Domains.TaiwuEvent.DisplayEvent;
using GameData.Domains.TaiwuEvent.EventOption;
using GameData.Domains.TaiwuEvent.InteractOfLoveEventHelper;
using GameData.Domains.World.SectMainStory;
using GameData.Domains.Character;
using GameData.Domains.Character.AvatarSystem;
using GameData.Domains.Combat;
using GameData.Domains.CombatSkill;
using GameData.Domains.Character.Relation;
using GameData.Domains.Adventure;
using GameData.Domains.Item;
using GameData.Domains.Item.Display;
using GameData.Domains.World;
using GameData.Domains.Information;
using GameData.Domains.Map;
using GameData.Domains.Taiwu.Profession;


#pragma warning disable 1591

namespace Modder_76561198100202539.EventConfig.Taiwu.EventGroupe44b04fbc55643ee8b0aa5c7f0c6178a
{
	public class FirstModEvent_793b2f76e026475eb6b02b232343cb3c : TaiwuEventItem
	{
		public FirstModEvent_793b2f76e026475eb6b02b232343cb3c()
		{
			//事件GUID
			Guid = Guid.Parse("793b2f76-e026-475e-b6b0-2b232343cb3c");
			//该事件是否是头部事件
			IsHeadEvent = false;
			//事件分组
			EventGroup = "DouQuQu_001";
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
			MainRoleKey = "";
			//进行交互的角色的Key，为空时将不显示背景图上的角色头像
			TargetRoleKey = "";
			//事件背景图，为空表示该事件使用地块适应的背景图
			EventBackground = "";
			//事件对遮罩的控制
			MaskControl = EventMaskControl.NoChange;
			//遮罩渐变的时间
			MaskTweenTime = 0f;
			//绑定在Esc按键处理的选项Key
			EscOptionKey = "";
			//选项列表
			EventOptions = new TaiwuEventOption[]		        
			{
				new TaiwuEventOption{ OptionKey = "Option_1401059141", OptionGuid = "e08468c7-821d-42d9-b363-3eaac72a765c"}
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
			return string.Empty;
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
			return string.Empty;
		}

		private string OnOption1Select()
		{
			return string.Empty;
		}

		public List<string> Option1GetExtraFormatLanguageKeys()
		{
		    return default;
		}


		#endregion
	}
}