using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Config;
using GameData.Common;
using GameData.Domains;
using GameData.Domains.Character;
using GameData.Domains.Mod;
using GameData.Domains.Taiwu;
using GameData.GameDataBridge;
using GameData.Utilities;
using HarmonyLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Redzen.Random;
using TaiwuModdingLib.Core.Plugin;
using Character = GameData.Domains.Character.Character;

namespace SoulRingBackend
{
    [PluginConfig("SoulRingBackend", "懒狗", "1.0.0.0")]
    public class SoulRingBackend : TaiwuRemakePlugin
    {
        private static PreexistenceCharIds _preexistenceCharIds;
        public static int SoulRingFrontendId { get; set; }
        public static string modIdStr;
        public static BandendBox bandendBox;

        public override void Initialize()
        {
            modIdStr = ModIdStr;
            bandendBox = new BandendBox();
            DomainManager.Mod.AddModMethod(ModIdStr, "ConvertToSoulRing", ConvertToSoulRing);
        }

        public override void Dispose() { }


    #region 前传后调用的方法

        /// <summary>处理前端传来的角色数据并转换为魂环</summary>
        private void ConvertToSoulRing(DataContext context, SerializableModData data)
        {
            if (data.Get("jsonData", out string jsonData))
            {
                var soulRingData = JsonConvert.DeserializeObject<SoulRingFrontendData>(jsonData);
                if (soulRingData != null)
                {
                    SoulRingFrontendId = soulRingData.SoulRingCharacterId;
                    AdaptableLog.Info($"魂环：获取人物Id {SoulRingFrontendId}");

                    if (SoulRingFrontendId != -1)
                    {
                        SetPreexistenceCharId(context);
                        SoulRingFrontendId = -1; // 重置ID避免重复处理
                    }
                }
                else
                {
                    AdaptableLog.Info("魂环：错误角色Id");
                }
            }
        }

        /// <summary>将指定角色添加到太吾的轮回列表中</summary>
        public void SetPreexistenceCharId(DataContext context)
        {
            Character taiwu = DomainManager.Taiwu.GetTaiwu();
            AddPreexistenceCharId(taiwu, context, SoulRingFrontendId);
        }

        /// <summary>核心逻辑：管理轮回列表的添加和替换机制</summary>
        public static unsafe void AddPreexistenceCharId(Character taiwu, DataContext context, int reincarnationCharId)
        {
            AdaptableLog.Info($"魂环：开始处理角色ID: {reincarnationCharId}");

            // 初始化太吾字段
            _preexistenceCharIds = CharacterReflection.GetPreexistenceCharIds(taiwu);
            AdaptableLog.Info($"魂环：当前轮回数: {_preexistenceCharIds.Count}");

            // 检查角色是否已存在
            if (IsCharacterExists(reincarnationCharId))
            {
                AdaptableLog.Info($"魂环：角色已存在，跳过处理");
                return;
            }

            // 根据轮回数量执行不同逻辑
            if (_preexistenceCharIds.Count < 9)
            {
                AddCharacterToSlot(context, reincarnationCharId, taiwu);
            }
            else
            {
                AddSpecialFeature(context, _preexistenceCharIds, taiwu);
                ReplaceFullList(context, reincarnationCharId, taiwu);
            }

            // 处理待轮回角色
            DomainManager.Character.PossessionRemoveWaitingReincarnationChar(context, reincarnationCharId);

            // 清理临时角色
            DomainManager.Building.RemoveTemporaryPossessionCharacter(context);

            // 发送消息给前端
            bandendBox.isConverToSoulRingEnd = true;
            NotifyFrontend(bandendBox);


            AdaptableLog.Info($"魂环：处理完成，最终轮回数: {_preexistenceCharIds.Count}");
        }

        private static unsafe void AddSpecialFeature(DataContext context, PreexistenceCharIds preexistenceCharIds,
            Character taiwu)
        {
            // 计算声望差异值
            int diff = 0;
            for (int i = 0; i < preexistenceCharIds.Count; i++)
            {
                int charId;

                int* ptr = preexistenceCharIds.CharIds;
                charId = ptr[i];

                if (charId < 0) continue;

                DeadCharacter deadChar = DomainManager.Character.TryGetDeadCharacter(charId);
                if (deadChar == null) continue;

                // 简化判断逻辑
                if (deadChar.FameType == 3 || deadChar.FameType == -2)
                {
                    diff += (context.Random.NextBool() ? 1 : -1);
                }
                else
                {
                    diff += (deadChar.FameType > 3) ? 1 : -1;
                }
            }

            // 选择特性ID
            short featureId = (diff >= 0)
                ? ((short)context.Random.Next(257, 262)) // 正面特性
                : ((short)context.Random.Next(394, 399)); // 负面特性

            // 添加特性
            CharacterReflection.GetOfflineAddFeatureMethod(taiwu, featureId, true, false);
        }


        /// <summary>检查角色是否已在轮回列表中</summary>
        private unsafe static bool IsCharacterExists(int reincarnationCharId)
        {
            for (int i = 0; i < _preexistenceCharIds.Count; i++)
            {
                if (_preexistenceCharIds.CharIds[i] == reincarnationCharId)
                    return true;
            }

            return false;
        }

        /// <summary>将角色添加到未满的轮回列表</summary>
        private static void AddCharacterToSlot(DataContext context, int reincarnationCharId, Character taiwu)
        {
            _preexistenceCharIds.Add(context.Random, reincarnationCharId);
            CharacterReflection.ChangePreexistence(taiwu, _preexistenceCharIds);
            AdaptableLog.Info($"魂环：成功添加角色，当前轮回数: {_preexistenceCharIds.Count}");
        }


        /// <summary>轮回列表已满时的替换逻辑</summary>
        private static void ReplaceFullList(DataContext context, int reincarnationCharId, Character taiwu)
        {
            AdaptableLog.Info("魂环：轮回列表已满，执行替换逻辑");

            // 执行游戏原版的满额替换逻辑
            DomainManager.Character.RecordDeletedFromOthersPreexistence(context, ref _preexistenceCharIds);
            _preexistenceCharIds.Reset();
            _preexistenceCharIds.Add(context.Random, reincarnationCharId);
            CharacterReflection.ChangePreexistence(taiwu, _preexistenceCharIds);

            AdaptableLog.Info($"魂环：列表已重置并添加新角色");
        }

    #endregion


    #region 后传前

        public static void NotifyFrontend(BandendBox data)
        {
            var jsonData = JsonConvert.SerializeObject(data);
            GameDataBridge.AddDisplayEvent(DisplayEventType.ModDisplayEvent, modIdStr, jsonData);
        }

    #endregion
    }
}