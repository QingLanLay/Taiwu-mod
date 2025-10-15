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
using GameData.Utilities;


#pragma warning disable 1591

namespace Modder_76561198100202539.EventConfig.Taiwu.EventGroupe44b04fbc55643ee8b0aa5c7f0c6178a
{
    public class FirstModEvent_5037733e7ff445278a4cdd3854dc5933 : TaiwuEventItem
    {
        public FirstModEvent_5037733e7ff445278a4cdd3854dc5933()
        {
            //事件GUID
            Guid = Guid.Parse("5037733e-7ff4-4527-8a4c-dd3854dc5933");
            //该事件是否是头部事件
            IsHeadEvent = false;
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
                new TaiwuEventOption
                    { OptionKey = "Option_489255035", OptionGuid = "f15ade0c-444e-4713-9bd6-4d57f32b3e39" },
                new TaiwuEventOption
                    { OptionKey = "Option_513510439", OptionGuid = "5c9e1594-4596-4b65-8220-34f440ce3e39" },
                new TaiwuEventOption
                    { OptionKey = "Option_2045791296", OptionGuid = "6eda6530-ee64-4837-b12b-6d7834bd51be" }
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

        public override void OnEventEnter() { }

        public override void OnEventExit() { }

        public override string GetReplacedContentString()
        {

            return  "只见太吾自袖中取出几只青陶罐，揭盖时虫鸣骤起如裂帛。\n 围观众人齐刷刷踏前半步，草鞋碾得黄土地面沙沙作响。\n 领头村民见状问到：“不知太吾这般阵仗，是想斗虫几日？”";
        }

        public override List<string> GetExtraFormatLanguageKeys()
        {
            return default;
        }

    #endregion

    #region Options

        private void OnOption1Create()
        {
            EventOptions[0].OptionConsumeInfos = new List<OptionConsumeInfo>();
            EventOptions[0].OptionConsumeInfos.Add(new OptionConsumeInfo(OptionConsumeType.Authority, 500, true));
            EventOptions[0].OptionConsumeInfos.Add(new OptionConsumeInfo(OptionConsumeType.MovePoint, 30, true));
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
            return "十战十胜，鸣扬树间！<color=#C0C0C0>（奇脉虫者得十誉捷）</color>";
        }

        private string OnOption1Select()
        {
            Dictionary<ItemKey, int> inventoryItems = DomainManager.Character
                .GetElement_Objects(EventArgBox.TaiwuCharacterId).GetInventory().Items;
            var cricketList = new List<ItemKey>() { };
            var taiwu = EventArgBox.TaiwuCharacterId;
            foreach (ItemKey itemKey in inventoryItems.Keys)
            {
                bool flag = itemKey.ItemType == 11;
                if (flag)
                {
                    CricketData item = DomainManager.Item.GetCricketData(itemKey.Id);
                    GameData.Domains.Item.Cricket cricket = DomainManager.Item.GetElement_Crickets(itemKey.Id);
                    var dataContext = GameData.Common.DataContextManager.GetCurrentThreadDataContext();
                    bool flag1 = cricket.IsAlive && item.IsSmart;
                    if (flag1)
                    {
                        short winNum = (short)(cricket.GetWinsCount() + 10);
                        cricket.SetWinsCount((short)winNum, dataContext);
                        cricketList.Add(itemKey);
                    }
                }
            }

            if (cricketList != null && cricketList.Count > 0)
            {
                List<ItemDisplayData> itemDisplayDataList = DomainManager.Item.GetItemDisplayDataList(
                    cricketList, // 提取蛐蛐列表
                    taiwu // 获取当前太吾角色ID
                );
                using (List<ItemDisplayData>.Enumerator enumerator = itemDisplayDataList.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        ItemDisplayData data = enumerator.Current;
                        data.Amount = 1; // 设置物品数量
                    }
                }

                GameData.GameDataBridge.GameDataBridge.AddDisplayEvent<List<ItemDisplayData>, bool, bool, bool>(
                    GameData.GameDataBridge.DisplayEventType.OpenGetItem_Item, // 事件类型：打开获得物品界面
                    itemDisplayDataList, // 要显示的物品数据
                    false, // 自动存仓库标志
                    false, // 是否跳过动画（默认false）
                    true // 是否立即显示（默认true）
                );
                return "793b2f76-e026-475e-b6b0-2b232343cb3c";
            }

            return "ed82f1ad-7c54-4dcb-a5f2-0e454aa409bc";
        }

        public List<string> Option1GetExtraFormatLanguageKeys()
        {
            return default;
        }

        private void OnOption2Create()
        {
            EventOptions[1].OptionConsumeInfos = new List<OptionConsumeInfo>();
            EventOptions[1].OptionConsumeInfos.Add(new OptionConsumeInfo(OptionConsumeType.Authority, 250, true));
            EventOptions[1].OptionConsumeInfos.Add(new OptionConsumeInfo(OptionConsumeType.MovePoint, 15, true));
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
            return "五战五胜，半步虫王！<color=#C0C0C0>（奇脉虫者得五誉捷）</color>";
        }

        private string OnOption2Select()
        {
            Dictionary<ItemKey, int> inventoryItems = DomainManager.Character
                .GetElement_Objects(EventArgBox.TaiwuCharacterId).GetInventory().Items;
            var cricketList = new List<ItemKey>() { };
            var taiwu = EventArgBox.TaiwuCharacterId;
            foreach (ItemKey itemKey in inventoryItems.Keys)
            {
                bool flag = itemKey.ItemType == 11;
                if (flag)
                {
                    CricketData item = DomainManager.Item.GetCricketData(itemKey.Id);
                    GameData.Domains.Item.Cricket cricket = DomainManager.Item.GetElement_Crickets(itemKey.Id);
                    var dataContext = GameData.Common.DataContextManager.GetCurrentThreadDataContext();
                    bool flag1 = cricket.IsAlive && item.IsSmart;
                    if (flag1)
                    {
                        short winNum = (short)(cricket.GetWinsCount() + 5);
                        cricket.SetWinsCount((short)winNum, dataContext);
                        cricketList.Add(itemKey);
                    }
                }
            }

            if (cricketList != null && cricketList.Count > 0)
            {
                List<ItemDisplayData> itemDisplayDataList = DomainManager.Item.GetItemDisplayDataList(
                    cricketList, // 提取蛐蛐列表
                    taiwu // 获取当前太吾角色ID
                );
                using (List<ItemDisplayData>.Enumerator enumerator = itemDisplayDataList.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        ItemDisplayData data = enumerator.Current;
                        data.Amount = 1; // 设置物品数量
                    }
                }

                GameData.GameDataBridge.GameDataBridge.AddDisplayEvent<List<ItemDisplayData>, bool, bool, bool>(
                    GameData.GameDataBridge.DisplayEventType.OpenGetItem_Item, // 事件类型：打开获得物品界面
                    itemDisplayDataList, // 要显示的物品数据
                    false, // 自动存仓库标志
                    false, // 是否跳过动画（默认false）
                    true // 是否立即显示（默认true）
                );
                return "793b2f76-e026-475e-b6b0-2b232343cb3c";
            }

            return "ed82f1ad-7c54-4dcb-a5f2-0e454aa409bc";
        }

        public List<string> Option2GetExtraFormatLanguageKeys()
        {
            return default;
        }

        private void OnOption3Create() { }

        private bool OnOption3VisibleCheck()
        {
            return true;
        }


        private bool OnOption3AvailableCheck()
        {
            return true;
        }

        private string OnOption3GetReplacedContent()
        {
            return "在下想起有些琐事尚未处理，还是改日在战吧！<color=#C0C0C0>(挠头苦笑...)</color>";
        }

        private string OnOption3Select()
        {
            return string.Empty;
        }

        public List<string> Option3GetExtraFormatLanguageKeys()
        {
            return default;
        }

    #endregion
    }
}