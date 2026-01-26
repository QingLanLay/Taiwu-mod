using GameData.Domains.Building;
using GameData.Domains.Mod;
using GameData.Utilities;
using Newtonsoft.Json;
using Spine;
using UnityEngine;

namespace SoulRingFrontend
{
    public static class CallMethod
    {
        public static string modIdStr;

        /// <summary>
        /// 魂环前后端通信方法，传递选择人物
        /// </summary>
        /// <param name="modIdStr"></param>
        public static void CallBackendSoulRing()
        {
            var data = new SerializableModData();
            AdaptableLog.Info("CallBackendSoulRing人物Id SoulRingUI.SoulRingCharacterId SoulRingCharacterId：" +
                              SoulRingUI.SoulRingCharacterId);
            SoulRingFrontendData charData = new SoulRingFrontendData();
            charData.SoulRingCharacterId = SoulRingUI.SoulRingCharacterId;
            data.Set("jsonData", JsonUtility.ToJson(charData));
            ModDomainMethod.Call.CallModMethodWithParam(SoulRingInit.modIdStr, "ConvertToSoulRing", data);
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