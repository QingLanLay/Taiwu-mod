using System;
using System.Reflection;
using FrameWork;
using GameData.Domains.Mod;
using GameData.Utilities;
using HarmonyLib;
using TaiwuModdingLib.Core.Plugin;
using TMPro;
using UICommon.Character;
using UnityEngine;

namespace SoulRingFrontend
{
    [PluginConfig("MyFrMyTestFrontMod", "超级马桶", "0.1")]
    public class SoulRingFrontInit : TaiwuRemakeHarmonyPlugin
    {
        private Harmony harm;

        public override void Dispose()
        {
            if (this.harm != null)
            {
                ModManager.UnRegisterModDisplayEventHandler(ModIdStr, CallMethod.GetJsonDataRefreshSelectAvatar);
                this.harm.UnpatchSelf();
                AdaptableLog.Info("Harmony 补丁已卸载");
            }
        }

        public override void Initialize()
        {
            AdaptableLog.Info("===  ===");
            this.harm = new Harmony("SoulRingMod");

            try
            {
                SoulRingInit.modIdStr = this.ModIdStr;
                CallMethod.modIdStr = this.ModIdStr;
                ModManager.RegisterModDisplayEventHandler(ModIdStr, CallMethod.GetJsonDataRefreshSelectAvatar);
                harm.PatchAll();
                AdaptableLog.Info("魂环Mod前端完成!");
            }
            catch (Exception e)
            {
                AdaptableLog.Error("魂环补丁失败: " + e.ToString());
            }

            AdaptableLog.Info("===  初始化完成 ===");
        }
    }

    // Harmony补丁示例
    [HarmonyPatch(typeof(UI_SwapSoul))]
    public static class SoulRingInit
    {
        public static string modIdStr;

        // 此处可以添加补丁方法
        [HarmonyPostfix]
        [HarmonyPatch("Awake")]
        public static void Post_Awake(UI_SwapSoul __instance)
        {
            //初始化
            SoulRingUI.SoulRingInit(__instance);
        }


        [HarmonyPatch(typeof(UI_SwapSoul), "OnClick")]
        [HarmonyPrefix]
        public static bool OnClick_Prefix(UI_SwapSoul __instance, CButton btn)
        {
            string btnName = btn.name;

            // 处理魂环按钮点击
            if (btnName == "SoulRingCharacter")
            {
                // 选择角色
                SoulRingUI.SelectSoulRingCharacter();

                return false; // 跳过原方法
            }

            return true; // 继续执行原方法
        }

        [HarmonyPatch(typeof(UI_SwapSoul), "OnEnable")]
        [HarmonyPostfix]
        public static void OnEnable_Prefix(UI_SwapSoul __instance)
        {
            SoulRingUI.RefreshSoulRingCharacter(-1);
        }
        
    }
}