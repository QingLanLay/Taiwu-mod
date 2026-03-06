using Config.EventConfig;
using GameData.Common;
using GameData.Domains;
using GameData.Domains.Item;
using GameData.Domains.TaiwuEvent;
using GameData.Domains.TaiwuEvent.Enum;
using GameData.Utilities;

namespace BetterCuZhi;

public class FixedCuZhiEvent
{
    public class FixedCuZhiEvent01 : EventPackage
    {
        // Token: 0x0600005B RID: 91 RVA: 0x0000343C File Offset: 0x0000163C
        public FixedCuZhiEvent01()
        {
            base.NameSpace = "Taiwu";
            base.Author = "LazyDog";
            base.Group = "BetterFix_001_e44b04fbc55643ee8b0aa5c7f0c6178b";
            this.EventList = new List<TaiwuEventItem>
            {
                new FirstModEvent_92d832aff3504b208682511860090791(),
            };
        }
    }
}

public class FirstModEvent_92d832aff3504b208682511860090791 : TaiwuEventItem
{
    public FirstModEvent_92d832aff3504b208682511860090791()
    {
        this.Guid = Guid.Parse("92d832af-f350-4b20-8682-511860090791");
        this.IsHeadEvent = true;
        this.EventGroup = "FixCuZhi";
        this.ForceSingle = false;
        this.EventType = (EEventType)6;
        this.TriggerType = EventTrigger.NewGameMonth;
        this.EventSortingOrder = 499;
        this.MainRoleKey = "";
        this.TargetRoleKey = "";
        this.EventBackground = "";
        this.MaskControl = 0;
        this.MaskTweenTime = 0f;
        this.EscOptionKey = "";
        this.EventOptions = new TaiwuEventOption[0];
        this.InitOptions();
    }

    // 在类级别创建一个静态的Random实例
    private static readonly Random _random = new Random();

    // Token: 0x06000045 RID: 69 RVA: 0x00002092 File Offset: 0x00000292
    private void InitOptions() { }

    // Token: 0x06000046 RID: 70 RVA: 0x00002F44 File Offset: 0x00001144
    public override bool OnCheckEventCondition()
    {
        AdaptableLog.Info("促织修复事件检测");
        // TODO:促织罐月初修复包裹内三只受伤蛐蛐
        // 获取背包中的物品信息
        Dictionary<ItemKey, int> inventoryItems = DomainManager.Character
            .GetElement_Objects(EventArgBox.TaiwuCharacterId).GetInventory().Items;
        // 获取蛐蛐列表
        List<ItemKey> cricketList = new List<ItemKey>();

        // 获取上下文数据
        var context = DataContextManager.GetCurrentThreadDataContext();

        foreach (ItemKey itemKey in inventoryItems.Keys)
        {
            // 判断物品类型是否为蛐蛐（11表示蛐蛐）
            bool flag1 = itemKey.ItemType == 11;
            if (flag1)
            {
                
                Cricket cricket = DomainManager.Item.GetElement_Crickets(itemKey.Id);
                short[] injuries = cricket.GetInjuries();
                bool hasInjury = injuries.Exist((short value) => value > 0); // 存在未治愈的伤势
                bool isRecoverable = !(cricket.GetCurrDurability() == 0 ||
                                       (cricket.GetCurrDurability() >= cricket.GetMaxDurability() &&
                                        !hasInjury));
                
                if (cricket.IsAlive && isRecoverable)
                {
                    cricketList.Add(itemKey);
                }
            }
        }
        
        int currentCricket = 0;
        // 计数
        foreach (var item in inventoryItems)
        {
            
            for (int i = 0; i < item.Value; i++)
            {
                if (item.Key.ItemType == 12 && item.Key.TemplateId <= 90 && item.Key.TemplateId >= 82)
                {
                    if (cricketList.Count != 0 && currentCricket < cricketList.Count)
                    {
                        RecoverCricket(cricketList[currentCricket], item.Key.TemplateId - 80, context);
                        currentCricket++;
                    }
                }
            }
          
        }

        return false;
    }

    private static void RecoverCricket(ItemKey itemKey, int canFixNum, DataContext context)
    {
        AdaptableLog.Info("尝试修复");
        // 获取蛐蛐信息
        GameData.Domains.Item.Cricket cricket = DomainManager.Item.GetElement_Crickets(itemKey.Id);

        // 判断蛐蛐是否满足以下条件：存活
        if (cricket.IsAlive)
        {
            // 检查蟋蟀状态是否需要恢复
            short[] injuries = cricket.GetInjuries();
            bool hasInjury = injuries.Exist((short value) => value > 0); // 存在未治愈的伤势

            // 判断是否处于可恢复状态：
            // 1. 当前耐久度不为零（未死亡）
            // 2. 且（耐久度未满 或 存在未治愈的伤势）
            bool isRecoverable = !(cricket.GetCurrDurability() == 0 ||
                                   (cricket.GetCurrDurability() >= cricket.GetMaxDurability() &&
                                    !hasInjury));

            var next = _random.Next(0, 101);
            bool canFix = next <= (canFixNum * 10);
            AdaptableLog.Info($"TemplateId={itemKey.TemplateId}, canFixNum={canFixNum }, "+
                              $"概率={canFixNum * 10}%, 随机数={next}, 是否修复={canFix}");

            if (isRecoverable && canFix)
            {
                // 恢复1点耐久度（不超过最大值）
                cricket.SetCurrDurability(
                    (short)Math.Min(cricket.GetMaxDurability(), cricket.GetCurrDurability() + 1), context);

                // 治疗伤势
                if (hasInjury)
                {
                    // 使用对象池优化，创建可治疗的伤势类型列表
                    List<int> typeRandomPool = ObjectPool<List<int>>.Instance.Get();
                    typeRandomPool.Clear();

                    // 收集所有存在伤势的部位
                    for (int injuryIndex = 0; injuryIndex < injuries.Length; injuryIndex++)
                    {
                        if (injuries[injuryIndex] > 0)
                            typeRandomPool.Add(injuryIndex);
                    }

                    int nextValue = new Random().Next(typeRandomPool.Count);
                    // 随机选择一个伤势部位进行治疗
                    int healIndex = typeRandomPool[nextValue];

                    // 治疗规则：
                    // 前两个部位（可能为重要部位）每次治疗减少5点伤势值
                    // 其他部位每次治疗减少1点伤势值
                    injuries[healIndex] = (short)Math.Max(injuries[healIndex] - (healIndex < 2 ? 5 : 1), 0);

                    // 更新伤势数据
                    cricket.SetInjuries(injuries, context);

                    // 归还对象池
                    ObjectPool<List<int>>.Instance.Return(typeRandomPool);
                    AdaptableLog.Info("修复成功");
                }
            }
        }
    }


// Token: 0x06000047 RID: 71 RVA: 0x00002092 File Offset: 0x00000292
    public override void OnEventEnter() { }

// Token: 0x06000048 RID: 72 RVA: 0x00002092 File Offset: 0x00000292
    public override void OnEventExit() { }

// Token: 0x06000049 RID: 73 RVA: 0x000024D8 File Offset: 0x000006D8
    public override string GetReplacedContentString()
    {
        return string.Empty;
    }

// Token: 0x0600004A RID: 74 RVA: 0x000024F0 File Offset: 0x000006F0
    public override List<string> GetExtraFormatLanguageKeys()
    {
        return null;
    }
}