using System;
using System.Collections.Generic;
using Config;
using GameData.Common;
using GameData.Domains;
using GameData.Domains.Building;
using GameData.Domains.Item;
using GameData.Domains.Map;
using Character = GameData.Domains.Character.Character;

namespace AutoHarvestHelper
{
    /// <summary>
    /// 自动收获助手类
    /// 用于处理太吾村建筑中的自动收获、购买和招募功能
    /// </summary>
    internal class AutoHarvestHelper
    {
        // 配置字段
        public static bool enableAutoHarvest;   // 是否启用自动收获
        public static bool enableAutoBuy;       // 是否启用自动购买
        public static bool enableAutoRecruit;   // 是否启用自动招募

        
        /// <summary>
        /// 处理自动收获逻辑
        /// </summary>
        /// <param name="context">数据上下文</param>
        public static void HandleAutoHarvest(DataContext context)
        {
            // 检查是否启用自动收获
            if (!enableAutoHarvest)
                return;

            // 获取太吾角色和太吾村位置
            Character taiwu = DomainManager.Taiwu.GetTaiwu();
            Location taiwuVillageLocation = DomainManager.Taiwu.GetTaiwuVillageLocation();
            
            // 获取建筑区域数据
            BuildingAreaData buildingAreas = DomainManager.Building.GetElement_BuildingAreas(taiwuVillageLocation);
            
            // 遍历所有建筑地块
            for (short blockIndex = 0; blockIndex < buildingAreas.Width * buildingAreas.Width; blockIndex++)
            {
                // 创建建筑地块键
                BuildingBlockKey buildingBlockKey = new BuildingBlockKey(
                    taiwuVillageLocation.AreaId, 
                    taiwuVillageLocation.BlockId, 
                    blockIndex
                );
                
                // 获取建筑地块数据
                BuildingBlockData blockData = DomainManager.Building.GetElement_BuildingBlocks(buildingBlockKey);
                
                // 跳过非主建筑地块（附属建筑）
                if (blockData.RootBlockIndex <= 0)
                {
                    // 获取建筑配置数据
                    BuildingBlockItem configData = BuildingBlock.Instance.GetItem(blockData.TemplateId);
                    
                    if (configData != null)
                    {
                        // 处理商店售出物品收集
                        ProcessShopSoldItem(context, buildingBlockKey, configData);
                        
                        // 处理商店物品收集
                        ProcessShopItem(context, taiwu, buildingBlockKey, blockData, configData);
                        
                        // 处理人员招募
                        ProcessRecruitPeople(context, taiwu, buildingBlockKey, blockData, configData);
                    }
                }
            }
        }

        /// <summary>
        /// 处理人员招募逻辑
        /// </summary>
        /// <param name="context">数据上下文</param>
        /// <param name="taiwu">太吾角色</param>
        /// <param name="blockKey">建筑地块键</param>
        /// <param name="blockData">建筑地块数据</param>
        /// <param name="configData">建筑配置数据</param>
        private static void ProcessRecruitPeople(DataContext context, Character taiwu, BuildingBlockKey blockKey, 
            BuildingBlockData blockData, BuildingBlockItem configData)
        {
            List<int> recruitedPeopleList = new List<int>();
            
            // 检查建筑是否有招募人员的事件配置
            if (configData.SuccesEvent.Count != 0 && 
                ShopEvent.Instance.GetItem(configData.SuccesEvent[0]).RecruitPeopleProb.Count > 0)
            {
                // 特殊处理：当铺（ID:223）需要额外条件
                if (blockData.TemplateId == 223)
                {
                    // 检查是否启用自动招募且太吾拥有足够的威望（资源ID:7）
                    if (enableAutoRecruit && taiwu.GetResource(7) >= 3000)
                    {
                        DomainManager.Building.RecruitPeopleQuick(context, blockKey, recruitedPeopleList);
                    }
                }
                else
                {
                    // 普通建筑直接招募
                    DomainManager.Building.RecruitPeopleQuick(context, blockKey, recruitedPeopleList);
                }
            }
        }

        /// <summary>
        /// 处理商店售出物品收集
        /// </summary>
        /// <param name="context">数据上下文</param>
        /// <param name="buildingBlockKey">建筑地块键</param>
        /// <param name="configData">建筑配置数据</param>
        private static void ProcessShopSoldItem(DataContext context, BuildingBlockKey buildingBlockKey, 
            BuildingBlockItem configData)
        {
            // 检查是否有交换资源物品的事件
            if (configData.SuccesEvent.Count != 0 && 
                ShopEvent.Instance.GetItem(configData.SuccesEvent[0]).ExchangeResourceGoods != -1)
            {
                DomainManager.Building.ShopBuildingSoldItemReceiveQuick(context, buildingBlockKey);
            }
            
            // 检查是否有资源物品的事件
            if (configData.SuccesEvent.Count != 0 && 
                ShopEvent.Instance.GetItem(configData.SuccesEvent[0]).ResourceGoods != -1)
            {
                DomainManager.Building.AcceptBuildingBlockCollectEarningQuick(context, buildingBlockKey, false);
            }
        }

        /// <summary>
        /// 处理商店物品收集
        /// </summary>
        /// <param name="context">数据上下文</param>
        /// <param name="taiwu">太吾角色</param>
        /// <param name="blockKey">建筑地块键</param>
        /// <param name="blockData">建筑地块数据</param>
        /// <param name="configData">建筑配置数据</param>
        private static void ProcessShopItem(DataContext context, Character taiwu, BuildingBlockKey blockKey, 
            BuildingBlockData blockData, BuildingBlockItem configData)
        {
            List<ItemKey> collectedItems = new List<ItemKey>();
            
            // 检查建筑是否有物品列表事件且不是商会（ID:222）
            if (configData.SuccesEvent.Count != 0 && 
                ShopEvent.Instance.GetItem(configData.SuccesEvent[0]).ItemList.Count > 0 && 
                blockData.TemplateId != 222)
            {
                // 特殊处理：商会（ID:222）需要额外条件
                if (blockData.TemplateId == 222)
                {
                    // 检查是否启用自动购买且太吾拥有足够的银钱（资源ID:6）
                    if (enableAutoBuy && taiwu.GetResource(6) >= 3000)
                    {
                        DomainManager.Building.CollectItemQuick(context, blockKey, collectedItems);
                    }
                }
                else
                {
                    // 普通建筑直接收集物品
                    DomainManager.Building.CollectItemQuick(context, blockKey, collectedItems);
                }
            }
        }
    }
}