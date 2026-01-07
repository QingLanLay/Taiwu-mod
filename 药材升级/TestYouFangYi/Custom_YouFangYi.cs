using System;
using System.Collections.Generic;
using Config.EventConfig;

namespace Modder_76561198100202539.EventConfig.Taiwu.EventGroup483794014fbc4007b2f13bd8cc1e708e
{
    // Token: 0x02000002 RID: 2
    public class Taiwu_EventPackage_CuZhiLing02 : EventPackage
    {
        // Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
        public Taiwu_EventPackage_CuZhiLing02()
        {
            base.NameSpace = "Taiwu";
            base.Author = "Modder_76561198100202539";
            base.Group = "CuZhiLing02_483794014fbc4007b2f13bd8cc1e708e";
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