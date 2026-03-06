using System.Collections;
using System.Reflection;
using GameData.ArchiveData;
using GameData.Common;
using GameData.Domains;
using GameData.Domains.Character;
using GameData.Domains.Item;
using GameData.Domains.TaiwuEvent.EventHelper;
using GameData.Domains.World;
using GameData.Utilities;
using GameData.Utilities.Mod;
using HarmonyLib;
using TaiwuModdingLib.Core.Plugin;

namespace AutoHarvestHelper;

[PluginConfig("AutoHarvestHelper", "LazyDog", "1.0.0")]
public class InitAutoHarvestHelper : TaiwuRemakePlugin
{
    
    
    public override void Initialize()
    {
        harmony = new Harmony("AutoHarvestHelper");
        // 显式注册每个补丁类，而不是使用PatchAll()
        harmony.PatchAll(typeof(InitAutoHarvestHelper)); // 主类中的补丁
    }

    public override void Dispose()
    {
        if (harmony != null)
        {
            harmony.UnpatchSelf();
            AdaptableLog.Info("关闭mod");
        }
    }

    public override void OnModSettingUpdate()
    {
        DomainManager.Mod.GetSetting(base.ModIdStr, "enableAutoHarvest", ref AutoHarvestHelper.enableAutoHarvest);
        DomainManager.Mod.GetSetting(base.ModIdStr, "enableAutoBuy",ref AutoHarvestHelper.enableAutoBuy);
        DomainManager.Mod.GetSetting(base.ModIdStr, "enableAutoRecruit", ref AutoHarvestHelper.enableAutoRecruit);
    }
    
    private Harmony harmony;
    
    [HarmonyPostfix]
    [HarmonyPatch(typeof(WorldDomain), "AdvanceMonth")]
    public static void WorldDomain_AdvanceMonth_Postfix(DataContext context)
    {
        AutoHarvestHelper.HandleAutoHarvest(context);
    }

}

