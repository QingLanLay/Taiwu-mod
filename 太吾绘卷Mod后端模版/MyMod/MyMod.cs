using System;
using GameData.Utilities;
using HarmonyLib;
using TaiwuModdingLib.Core.Plugin;

namespace MyMod
{
    [PluginConfig("MyMod", "作者名", "1.0.0.0")]
    public class MyMod : TaiwuRemakePlugin
    {
        private Harmony _harmony;

        public override void Initialize()
        {
            // 初始化日志
            AdaptableLog.Info("MyMod 初始化");
            
            // Harmony补丁初始化
            _harmony = new Harmony("MyMod.Patches");
            _harmony.PatchAll();
        }

        public override void Dispose()
        {
            // 清理Harmony补丁
            _harmony?.UnpatchSelf();
            
            AdaptableLog.Info("MyMod 卸载完成");
        }
    }

    // Harmony补丁示例
    [HarmonyPatch]
    public class ExamplePatch
    {
        // 此处可以添加补丁方法
    }
}