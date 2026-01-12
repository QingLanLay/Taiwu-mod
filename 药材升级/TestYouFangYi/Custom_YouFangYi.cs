using System;
using System.Collections.Generic;
using Config.EventConfig;

namespace Modder_76561198100202539.EventConfig.Taiwu.EventGroup483794014fbc4007b2f13bd8cc1e708e
{
    /// <summary>
    /// 事件包：药材升级事件包
    /// 作者：懒狗
    /// 命名空间：Taiwu
    /// </summary>
    public class Taiwu_EventPackage_YaoCaiShengJi : EventPackage
    {
        /// <summary>
        /// 构造函数 - 初始化事件包
        /// </summary>
        public Taiwu_EventPackage_YaoCaiShengJi()
        {
            // 设置基本信息
            base.NameSpace = "Taiwu";
            base.Author = "Modder_76561198100202539";
            base.Group = "YaoCaiShengJi_483794014fbc4007b2f13bd8cc1e708e";
            
            // 初始化事件列表
            this.EventList = new List<TaiwuEventItem>
            {
                new YouFangYiEvent_cd8d478c12c544d0aa51a917eacd7530(),
                new YouFangYiEvent_bb15f5e8069a4ca7b497fa1510e7b671(),
                new YouFangYiEvent_ccd5156ddb6244b1baf03ce42a7e612b(),
                new YouFangYiEvent_2bd02acd244e4f81a194502a0389c3f6(),
                new YouFangYiEvent_72c17bd700b34cec8aa316a732a6d3b0()
            };
        }
    }
}