using System.Reflection;
using Config.EventConfig;
using GameData.Domains;
using GameData.Domains.Item;
using GameData.Domains.Mod;
using GameData.Domains.TaiwuEvent;
using GameData.Domains.TaiwuEvent.EventHelper;
using GameData.Domains.TaiwuEvent.EventManager;
using GameData.Utilities;
using HarmonyLib;
using HarmonyLib.Public.Patching;
using TaiwuModdingLib.Core.Plugin;

namespace Batch_Cricket_Fighting
{
    [PluginConfig("BatchCricketFighting", "超级马桶", "0.1")]
    public class BatchCricketFightingInit : TaiwuRemakeHarmonyPlugin
    {
        // Token: 0x0600000F RID: 15 RVA: 0x00002A0C File Offset: 0x00000C0C
        public override void Dispose()
        {
            bool flag = this.harm != null;
            if (flag)
            {
                this.harm.UnpatchSelf();
            }
        }

        public override void Initialize()
        {
            AdaptableLog.Info("批量促织启动了");
            this.harm = new Harmony("CricketFightingBackendPlugin");
            BatchCricketFightingInit.ModId = base.ModIdStr;
            harm.PatchAll(typeof(EventPlugin));
        }

        public static string ModId;

        // Token: 0x04000001 RID: 1
        private Harmony harm;
    }

    internal class EventPlugin
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(ModDomain), "LoadAllEventPackages")]
        public static void LoadEventPackageFromAssembly_Postfix(ModDomain __instance)
        {
            // 创建事件包
            Batch_Cricket_Fighting Batch_Cricket_Fighting = new Batch_Cricket_Fighting();
            // 存储modid
            Batch_Cricket_Fighting.SetModIdString(BatchCricketFightingInit.ModId);
            // 反射获取游戏基础事件和事件列表
            EventManagerBase[] value = Traverse.Create(DomainManager.TaiwuEvent).Field("_managerArray")
                .GetValue<EventManagerBase[]>();
            List<EventPackage> value2 = Traverse.Create(DomainManager.TaiwuEvent).Field("_packagesList")
                .GetValue<List<EventPackage>>();
            // 将事件包注册到管理器中
            foreach (EventManagerBase eventManagerBase in value)
            {
                if (eventManagerBase != null)
                {
                    eventManagerBase.HandleEventPackage(Batch_Cricket_Fighting);
                }
            }

            // 将事件存储到包列表
            value2.Add(Batch_Cricket_Fighting);
            
            bool flag = !EventPlugin.isInit;
            if (flag)
            {
                Harmony harmony = new Harmony("eventPatch");
                ManualEventPatcher.Initialize(harmony);
                EventPlugin.isInit = true;
            }
        }

        static bool isInit = false;
    }

    public class ManualEventPatcher
    {
        public static void Initialize(Harmony harmony)
        {
            try
            {
                // 直接修补您知道的特定事件类型
                Type eventType = AccessTools.TypeByName("TaiwuEvent_567d1caf8b284dbf8cbee746e8ac8cfd");
                if (eventType != null)
                {
                    MethodInfo onEventEnter = AccessTools.Method(eventType, "OnEventEnter");
                    if (onEventEnter != null)
                    {
                        MethodInfo postfix = typeof(ManualEventPatcher).GetMethod("EventPostfix");
                        harmony.Patch(onEventEnter, postfix: new HarmonyMethod(postfix));
                        AdaptableLog.Info("成功手动修补目标事件");
                        return;
                    }
                }

                AdaptableLog.Error("找不到目标事件类型或方法");
            }
            catch (Exception ex)
            {
                AdaptableLog.Error($"手动修补失败: {ex}");
            }
        }

        public static void EventPostfix(object __instance)
        {
            // 使用您原有的 AddBatchCricketOption 逻辑
            if (__instance?.GetType().Name == "TaiwuEvent_567d1caf8b284dbf8cbee746e8ac8cfd")
            {
                AddBatchCricketOption((TaiwuEventItem)__instance);
            }
        }

        private static void AddBatchCricketOption(TaiwuEventItem __instance)
        {
            string myOptionKey = BatchCricketFightingInit.ModId + "0001";

            TaiwuEventOption taiwuEventOption =
                __instance.EventOptions.Find((TaiwuEventOption tmp) => tmp.OptionKey == myOptionKey);
            AdaptableLog.Info("选项检测");
            
            bool flag = taiwuEventOption == null;
            if (flag)
            {
                AdaptableLog.Info("开始克隆选项");
                taiwuEventOption = Traverse.Create(__instance.EventOptions[8])
                    .Method("MemberwiseClone", Array.Empty<object>()).GetValue<TaiwuEventOption>();
                taiwuEventOption.OptionKey = myOptionKey;
                taiwuEventOption.OnOptionSelect = new Func<string>(OnOptionSelect_BatchCricketFighting);
                List<TaiwuEventOption> list = __instance.EventOptions.ToList<TaiwuEventOption>();
                
                // 计算倒数第二个位置的索引
                int insertIndex = Math.Max(0, list.Count - 1); // 确保索引不小于0
                list.Insert(insertIndex, taiwuEventOption);

                __instance.EventOptions = list.ToArray();
                AdaptableLog.Info("选项注入成功");
            }

            int npcID = 0;
            __instance.ArgBox.Get("CharacterId", ref npcID);
            if (npcID != null)
            {
                __instance.ArgBox.Set("npcID", npcID);
                var npc = EventHelper.GetCharacterById(npcID);

                Dictionary<ItemKey, int> inventoryItems = DomainManager.Character
                    .GetElement_Objects(EventArgBox.TaiwuCharacterId).GetInventory().Items;
                bool isVillageMan = false;

                var npcOrganizationInfo = npc.GetOrganizationInfo();
            
                if (npcOrganizationInfo.OrgTemplateId.ToString() == "16" )
                {
                    isVillageMan = true;
                }

                if (isVillageMan)
                {
                    taiwuEventOption.SetContent("(举办促织大会······)");
                    taiwuEventOption.OnOptionVisibleCheck = CheckReturnTrue;
                }
                else
                {
                    taiwuEventOption.SetContent("不是村民");
                    taiwuEventOption.OnOptionVisibleCheck = CheckReturnFalse;
                }
            }
        }
        
        

        public static bool CheckReturnTrue()
        {
            return true;
        }
        public static bool CheckReturnFalse()
        {
            return false;
        }


        public static string OnOptionSelect_BatchCricketFighting()
        {
            return "0e9cadb5-3270-45e3-bff7-94f3616834a5";
        }
    }
}