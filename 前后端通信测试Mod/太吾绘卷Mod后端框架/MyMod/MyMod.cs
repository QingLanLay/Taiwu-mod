using System;
using GameData.Common;
using GameData.Domains;
using GameData.Domains.Mod;
using GameData.Utilities;
using HarmonyLib;
using MyFrontMod;
using Newtonsoft.Json;
using TaiwuModdingLib.Core.Plugin;


namespace MyMod
{
    [PluginConfig("MyMod", "作者名", "1.0.0.0")]
    public class MyMod : TaiwuRemakePlugin
    {


        public override void Initialize()
        {
            DomainManager.Mod.AddModMethod(ModIdStr, "ExampleFoo", ExampleFoo);
        }

        public override void Dispose()
        {

        }
        
        private void ExampleFoo(DataContext context, SerializableModData data)
        {
            if (data.Get("jsonData", out string jsonData))
            {
                var testSendMethod = JsonConvert.DeserializeObject<TestSendMethod>(jsonData);
                AdaptableLog.Info($"前端调用并发送了数据 {testSendMethod.Value}");
            }
        }
    }

}