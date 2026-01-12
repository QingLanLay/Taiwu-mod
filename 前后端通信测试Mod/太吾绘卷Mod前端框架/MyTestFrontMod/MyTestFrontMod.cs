using System;
using GameData.Domains.Mod;
using GameData.Utilities;
using HarmonyLib;
using TaiwuModdingLib.Core.Plugin;
using UnityEngine;

namespace MyFrontMod
{
    [PluginConfig("MyFrMyTestFrontMod", "超级马桶", "0.1")]
    public class MyTestFrontMod : TaiwuRemakeHarmonyPlugin
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
            
            try
            {
                ExamplePatch.modIdStr = this.ModIdStr;
                AdaptableLog.Info("前端完成!");
                harm.PatchAll();
            }
            catch (Exception e)
            {
                AdaptableLog.Error("Harmony 补丁失败: " + e.ToString());
            }

            AdaptableLog.Info("===  初始化完成 ===");
        }
    }

    // Harmony补丁示例
    [HarmonyPatch(typeof(UI_CharacterMenu))]
    public static class ExamplePatch
    {
        public static string modIdStr;
        
        // 此处可以添加补丁方法
        [HarmonyPostfix]
        [HarmonyPatch("OnEnable")]
        public static void OnEnable_TestMethod()
        {
            CallBackendExampleFoo(modIdStr);
        }
        
        public static void CallBackendExampleFoo(string modIdStr)
        {
            var data = new SerializableModData();
            data.Set("jsonData", JsonUtility.ToJson(new TestSendMethod() { Value = "后端，你好！" }));
            ModDomainMethod.Call.CallModMethodWithParam(modIdStr, "ExampleFoo", data);
        }
    }
}