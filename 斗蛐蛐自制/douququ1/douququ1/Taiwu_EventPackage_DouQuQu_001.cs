using System.Collections.Generic;
using Config.EventConfig;

namespace Modder_76561198100202539.EventConfig.Taiwu.EventGroupe44b04fbc55643ee8b0aa5c7f0c6178a
{
	public class Taiwu_EventPackage_DouQuQu_001 : EventPackage
	{
		public Taiwu_EventPackage_DouQuQu_001()
		{
			NameSpace = "Taiwu";
			Author = "Modder_76561198100202539";
			Group = "DouQuQu_001_e44b04fbc55643ee8b0aa5c7f0c6178a";
			EventList = new List<TaiwuEventItem>{
				new FirstModEvent_92d832aff3504b208682511860090790(),
				new FirstModEvent_0e9cadb5327045e3bff794f3616834a4(),
				new FirstModEvent_5037733e7ff445278a4cdd3854dc5933(),
				new FirstModEvent_ed82f1ad7c544dcba5f20e454aa409bc(),
				new FirstModEvent_793b2f76e026475eb6b02b232343cb3c()
			};
		}
	}
}
