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
using GameData.Common;
using GameData.Domains;
using GameData.Domains.Item;
using GameData.Domains.TaiwuEvent;
using GameData.Domains.Map;
using GameData.Utilities;


#pragma warning disable 1591

namespace Modder_76561198100202539.EventConfig.Taiwu.EventGroupe44b04fbc55643ee8b0aa5c7f0c6178a
{
	public class FirstModEvent_92d832aff3504b208682511860090790 : TaiwuEventItem
	{
		public FirstModEvent_92d832aff3504b208682511860090790()
		{
			//事件GUID
			Guid = Guid.Parse("92d832af-f350-4b20-8682-511860090790");
			//该事件是否是头部事件
			IsHeadEvent = true;
			//事件分组
			EventGroup = "DouQuQu_001";
			//同一时间是否强制只允许触发一个同组事件
			ForceSingle = false;
			//事件类型
			EventType = EEventType.ModEvent;
			//事件触发点类型
			TriggerType = EventTrigger.NewGameMonth;
			//事件排序执行的优先级
			EventSortingOrder = 499;
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
				
			};
			InitOptions();
		}


		#region EventAPI
		private void InitOptions()
		{
		}

		public override bool OnCheckEventCondition()
		{
			int isFirstTrigger = 0;
			TaiwuEvent.GetModData("isFirstTrigger", true, ref isFirstTrigger);
			if (isFirstTrigger == 0)
			{
				return false;
			}
			if (isFirstTrigger == 2)
			{
				int currentMonth = 0;
				TaiwuEvent.GetModData("currentMonth", true, ref currentMonth);
				currentMonth++;
				TaiwuEvent.SetModInt("currentMonth",true, currentMonth);
			}
			if (isFirstTrigger == 3)
			{
				var currentMonthAboutSpring = EventHelper.GetGameDate();
				currentMonthAboutSpring = (currentMonthAboutSpring % 12) + 1;
				if (currentMonthAboutSpring >=2 && currentMonthAboutSpring <=4 )
				{
					int isSpring = 1;
					TaiwuEvent.SetModInt ("isSpring", true, isSpring);
				}
				else
				{
					int isSpring = 0;
					TaiwuEvent.SetModInt("isSpring",true, isSpring);
					int isAlchemyPerformed = 0;
					TaiwuEvent.SetModInt("isAlchemyPerformed", true, isAlchemyPerformed);
				}
			}
			int SelectNextMonth = 0;
			TaiwuEvent.SetModInt("SelectNextMonth",true, SelectNextMonth);
			Dictionary<ItemKey, int> inventoryItems = DomainManager.Character.GetElement_Objects(EventArgBox.TaiwuCharacterId).GetInventory().Items;
			List<ItemKey> cricketList = new List<ItemKey>();
			bool flag = false;
			foreach (ItemKey itemKey in inventoryItems.Keys)
			{
				if (itemKey.TemplateId == 11062)
				{
					flag = true;
				}
			}
			int i = 0;
			if (flag)
			{
				var context = DataContextManager.GetCurrentThreadDataContext();
				foreach (ItemKey itemKey in inventoryItems.Keys)
		            {
		                bool flag1 = itemKey.ItemType == 11;
		                if (flag1)
		                {
		                    GameData.Domains.Item.Cricket cricket = DomainManager.Item.GetElement_Crickets(itemKey.Id);
		                    if (cricket.IsAlive && i < 2)
		                    {
		                        short[] injuries = cricket.GetInjuries();
		                        bool hasInjury = injuries.Exist((short value) => value > 0);// 存在未治愈的伤势
		                        bool isRecoverable = !(cricket.GetCurrDurability() == 0 ||
		                                               (cricket.GetCurrDurability() >= cricket.GetMaxDurability() &&
		                                                !hasInjury));
		                        if (isRecoverable)
		                        {
		                            cricket.SetCurrDurability(
		                                (short)Math.Min(cricket.GetMaxDurability(), cricket.GetCurrDurability() + 1), context);
		                            i++;
		                            if (hasInjury)
		                            {
		                                List<int> typeRandomPool = ObjectPool<List<int>>.Instance.Get();
		                                typeRandomPool.Clear();
		                                for (int injuryIndex = 0; injuryIndex < injuries.Length; injuryIndex++)
		                                {
		                                    if (injuries[injuryIndex] > 0)
		                                        typeRandomPool.Add(injuryIndex);
		                                }
		                                int nextValue = new Random().Next(typeRandomPool.Count);
		                                int healIndex = typeRandomPool[nextValue];
		                                injuries[healIndex] = (short)Math.Max(injuries[healIndex] - (healIndex < 2 ? 5 : 1), 0);
		                                cricket.SetInjuries(injuries, context);
		                                ObjectPool<List<int>>.Instance.Return(typeRandomPool);
		                            }
		                        }
		                    }
		                }
		            }
			}
			return false;
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

		#endregion
	}
}