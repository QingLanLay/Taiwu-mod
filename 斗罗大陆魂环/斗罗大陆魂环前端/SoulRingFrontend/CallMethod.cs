using GameData.Domains.Building;
using GameData.Domains.Mod;
using GameData.Utilities;
using Newtonsoft.Json;
using Spine;
using UnityEngine;
using System.Collections; // 新增
using DG.Tweening; // 新增

namespace SoulRingFrontend
{
    public static class CallMethod
    {
        public static string modIdStr;

        /// <summary>
        /// 魂环前后端通信方法，传递选择人物
        /// </summary>
        public static void CallBackendSoulRing()
        {
            // 先播放音效和视觉效果
            StartReincarnationEffect();
            
            var data = new SerializableModData();
            AdaptableLog.Info("CallBackendSoulRing人物Id SoulRingUI.SoulRingCharacterId SoulRingCharacterId：" +
                              SoulRingUI.SoulRingCharacterId);
            SoulRingFrontendData charData = new SoulRingFrontendData();
            charData.SoulRingCharacterId = SoulRingUI.SoulRingCharacterId;
            data.Set("jsonData", JsonUtility.ToJson(charData));
            ModDomainMethod.Call.CallModMethodWithParam(SoulRingInit.modIdStr, "ConvertToSoulRing", data);
        }

        /// <summary>
        /// 启动轮回特效协程
        /// </summary>
        private static void StartReincarnationEffect()
        {
            if (SoulRingUI.SwapSoul != null)
            {
                SoulRingUI.SwapSoul.StartCoroutine(CoroutineReincarnationEffect());
            }
        }

        /// <summary>
        /// 轮回特效协程
        /// </summary>
        private static IEnumerator CoroutineReincarnationEffect()
        {
            // 播放开始音效
            AudioManager.Instance.PlayAmbience("SFX_Swapsoul_change_amb_loop", 1.5f, 100);
            AudioManager.Instance.PlaySound("SFX_Swapsoul_hand", false, false);
            AudioManager.Instance.PlaySound("SFX_Swapsoul_door", false, false);

            // 获取UI组件引用
            var fadeToRedImg = SoulRingUI.SwapSoul.FadeToRedImg;
            var blueSceneRoot = SoulRingUI.SwapSoul.BlueSceneRoot;
            var redSceneRoot = SoulRingUI.SwapSoul.RedSceneRoot;

            // 1. 背景淡入红色（1秒）
            if (fadeToRedImg != null)
                fadeToRedImg.DOFade(1f, 1f);
            
            yield return new WaitForSeconds(1f);

            // 2. 切换场景（蓝色->红色）
            if (blueSceneRoot != null) blueSceneRoot.SetActive(false);
            if (redSceneRoot != null) redSceneRoot.SetActive(true);

            // 3. 播放过程中的音效
            AudioManager.Instance.PlaySound("SFX_Swapsoul_lighting", false, false);
            yield return new WaitForSeconds(1f);
            
            AudioManager.Instance.PlaySound("SFX_Swapsoul_absorb", false, false);
            yield return new WaitForSeconds(1f);

            // 4. 恢复原状
            if (redSceneRoot != null) redSceneRoot.SetActive(false);
            if (blueSceneRoot != null) blueSceneRoot.SetActive(true);
            
            if (fadeToRedImg != null)
                fadeToRedImg.DOFade(0f, 0.5f);

            // 5. 恢复背景音乐
            AudioManager.Instance.PlayAmbience("SFX_Swapsoul_amb_loop", 1.5f, 100);
        }

        public static void GetJsonDataRefreshSelectAvatar(string jsonData)
        {
            var bandendBox = JsonConvert.DeserializeObject<BandendBox>(jsonData);
            AdaptableLog.Info($"后端调用并发送了数据 已经转换{bandendBox.isConverToSoulRingEnd}");
            if (bandendBox.isConverToSoulRingEnd == true)
            {
                SoulRingUI.RefreshSoulRingCharacter(-1);
                // 执行角色列表刷新
                if (SoulRingUI.SwapSoul != null && SoulRingUI.SwapSoul.Element != null)
                {
                    int gameDataListenerId = SoulRingUI.SwapSoul.Element.GameDataListenerId;
    
                    // 现在可以调用刷新方法
                    BuildingDomainMethod.Call.GetSwapSoulCeremonySoulCharIdList(gameDataListenerId);
                    BuildingDomainMethod.Call.GetSwapSoulCeremonyBodyCharIdList(gameDataListenerId);
                }
            }
        }
    }
}