using System;
using System.Reflection;
using Config;
using GameData.Utilities;
using HarmonyLib;
using TaiwuModdingLib.Core.Plugin;
using TaiwuModdingLib.Core.Utils;

namespace MyFrontMod
{
    [PluginConfig("MyFrontMod", "超级马桶", "0.1")]
    public class MyFrontMod : TaiwuRemakeHarmonyPlugin
    {
        private Harmony harm;

        public override void Dispose()
        {
            if (this.harm != null)
            {
                this.harm.UnpatchSelf();
                AdaptableLog.Info("Harmony 补丁已卸载");
            }
        }

        public override void Initialize()
        {
            AdaptableLog.Info("===  ===");
            this.harm = new Harmony("Custom_Front");
            harm.PatchAll();
            try
            {

                AdaptableLog.Info("前端完成!");
            }
            catch (Exception e)
            {
                AdaptableLog.Error("Harmony 补丁失败: " + e.ToString());
            }

            AdaptableLog.Info("===  初始化完成 ===");
        }
        
    }
    // Harmony补丁示例
    [HarmonyPatch]
    public class ExamplePatch
    {
        // 此处可以添加补丁方法
    }
}