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
            
                        
            // 如果正在播放特效，拦截所有点击（包括关闭按钮）
            if (SoulRingUI.IsShowingEffect)
            {
                // 可以加一个漂浮提示提示玩家
                return false; 
            }

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
        
        [HarmonyPatch(typeof(UI_SwapSoul), "OnDisable")]
        [HarmonyPostfix] // 使用 Postfix 确保在基类处理完后执行
        public static void OnDisable_Postfix()
        {
            // 强制重置所有静态变量，防止下次打开时卡死
            SoulRingUI.ResetAllStatus(); 
        }
        
        [HarmonyPatch(typeof(UI_SwapSoul), "QuickHide")]
        [HarmonyPrefix]
        public static bool QuickHide_Prefix()
        {
            // 如果正在播放特效或者正在执行逻辑，返回 false 阻止 UI 关闭
            if (SoulRingUI.IsShowingEffect)
            {
                // 这里可以添加一个浮窗提示，告诉玩家“化魂中，请稍候”
                // UI_MessageManager.Instance.AddText("化魂过程中无法关闭界面"); 
                return false; 
            }

            // 如果不在过程中，返回 true，允许执行原版的 QuickHide 逻辑
            return true;
        }



        
    }
}