using System;
using System.Collections.Generic;
using Config.EventConfig;
using GameData.Domains.Character;
using GameData.Domains.Item;
using GameData.Domains.TaiwuEvent;
using GameData.Domains.TaiwuEvent.Enum;
using GameData.Domains.TaiwuEvent.EventHelper;

namespace Modder_76561198100202539.EventConfig.Taiwu.EventGroup483794014fbc4007b2f13bd8cc1e708e
{
    /// <summary>
    /// 有方仪事件 - 药材升级选择界面
    /// 事件ID: 72c17bd7-00b3-4cec-8aa3-16a732a6d3b0
    /// 功能：提供多种药材升级选项的界面
    /// </summary>
    public class YouFangYiEvent_72c17bd700b34cec8aa316a732a6d3b0 : TaiwuEventItem
    {
        /// <summary>
        /// 构造函数 - 初始化事件配置
        /// </summary>
        public YouFangYiEvent_72c17bd700b34cec8aa316a732a6d3b0()
        {
            // 基础事件配置
            this.Guid = Guid.Parse("72c17bd7-00b3-4cec-8aa3-16a732a6d3b0");
            this.IsHeadEvent = false; // 非头部事件，由其他事件触发
            this.EventGroup = "YaoCaiShengJi"; // 药材升级事件组
            this.ForceSingle = false;
            this.EventType = (EEventType)6;
            this.TriggerType = EventTrigger.None; // 不直接触发，由其他事件跳转
            this.EventSortingOrder = 500;
            
            // 角色配置
            this.MainRoleKey = "";
            this.TargetRoleKey = "";
            
            // 界面配置
            this.EventBackground = "";
            this.MaskControl = 0;
            this.MaskTweenTime = 0f;
            this.EscOptionKey = "";
            
            // 初始化事件选项
            this.EventOptions = new TaiwuEventOption[]
            {
                new TaiwuEventOption // 选项1
                {
                    OptionKey = "Option_-1126735797",
                    OptionGuid = "613e94a9-d9e1-4511-b3a1-7e0a0d0e960c"
                },
                new TaiwuEventOption // 选项2
                {
                    OptionKey = "Option_-891361317",
                    OptionGuid = "e267f16b-6fd3-4f4b-a68f-3b898aa8e00e"
                },
                new TaiwuEventOption // 选项3
                {
                    OptionKey = "Option_-893284401",
                    OptionGuid = "59cc595d-92e0-473b-ab7f-a66dcf895990"
                },
                new TaiwuEventOption // 选项4
                {
                    OptionKey = "Option_-857513030",
                    OptionGuid = "6b8c0036-2797-4615-ad15-09447c59d5ca"
                },
                new TaiwuEventOption // 选项5
                {
                    OptionKey = "Option_1936792895",
                    OptionGuid = "ad824dc5-ac17-4b55-91cb-767f29fa64c4"
                },
                new TaiwuEventOption // 选项6
                {
                    OptionKey = "Option_1112125176",
                    OptionGuid = "133cd4a6-5599-45b2-8498-c7649be1ecc7"
                },
                new TaiwuEventOption // 选项7
                {
                    OptionKey = "Option_1518141782",
                    OptionGuid = "af3ba0d3-1378-4918-bb0c-8f0978255466"
                },
                new TaiwuEventOption // 选项8
                {
                    OptionKey = "Option_627222926",
                    OptionGuid = "97ada584-78bf-4f06-a3ec-a55ac7c9843a"
                },
                new TaiwuEventOption // 选项9
                {
                    OptionKey = "Option_1378749027",
                    OptionGuid = "ced937b4-ceef-4b5e-803d-61d5a4e23256"
                },
                new TaiwuEventOption // 选项10
                {
                    OptionKey = "Option_-1194893333",
                    OptionGuid = "a2c7de43-3037-4bc7-aa79-56468736ad8c"
                },
                new TaiwuEventOption // 选项11
                {
                    OptionKey = "Option_1602323249",
                    OptionGuid = "ce2ada07-9ca9-4830-bec8-b0064cd19334"
                },
                // 新增的12号选项 - 移速步伐
                new TaiwuEventOption
                {
                    OptionKey = "Option_170171_12",
                    OptionGuid = Guid.NewGuid().ToString()
                },
                // 新增的13号选项 - 提架回复
                new TaiwuEventOption
                {
                    OptionKey = "Option_206207_13",
                    OptionGuid = Guid.NewGuid().ToString()
                },
                // 新增的14号选项 - 施展引奇
                new TaiwuEventOption
                {
                    OptionKey = "Option_230231_14",
                    OptionGuid = Guid.NewGuid().ToString()
                },
                // 新增的99号选项 - 返回
                new TaiwuEventOption
                {
                    OptionKey = "Option_99_Return",
                    OptionGuid = Guid.NewGuid().ToString()
                }
            };
            
            // 初始化所有选项的回调函数
            this.InitOptions();
        }

        /// <summary>
        /// 初始化所有选项的回调函数
        /// </summary>
        private void InitOptions()
        {
            // 选项1初始化
            this.EventOptions[0].OnOptionVisibleCheck = new Func<bool>(this.OnOption1VisibleCheck);
            this.EventOptions[0].OnOptionAvailableCheck = new Func<bool>(this.OnOption1AvailableCheck);
            this.EventOptions[0].GetReplacedContent = new Func<string>(this.OnOption1GetReplacedContent);
            this.EventOptions[0].OnOptionSelect = new Func<string>(this.OnOption1Select);
            this.EventOptions[0].GetExtraFormatLanguageKeys = new Func<List<string>>(this.Option1GetExtraFormatLanguageKeys);
            this.EventOptions[0].DefaultState = 0;
            this.EventOptions[0].OneTimeOnly = false;
            this.OnOption1Create();
            
            // 选项2初始化
            this.EventOptions[1].OnOptionVisibleCheck = new Func<bool>(this.OnOption2VisibleCheck);
            this.EventOptions[1].OnOptionAvailableCheck = new Func<bool>(this.OnOption2AvailableCheck);
            this.EventOptions[1].GetReplacedContent = new Func<string>(this.OnOption2GetReplacedContent);
            this.EventOptions[1].OnOptionSelect = new Func<string>(this.OnOption2Select);
            this.EventOptions[1].GetExtraFormatLanguageKeys = new Func<List<string>>(this.Option2GetExtraFormatLanguageKeys);
            this.EventOptions[1].DefaultState = 0;
            this.EventOptions[1].OneTimeOnly = false;
            this.OnOption2Create();
            
            // 选项3初始化
            this.EventOptions[2].OnOptionVisibleCheck = new Func<bool>(this.OnOption3VisibleCheck);
            this.EventOptions[2].OnOptionAvailableCheck = new Func<bool>(this.OnOption3AvailableCheck);
            this.EventOptions[2].GetReplacedContent = new Func<string>(this.OnOption3GetReplacedContent);
            this.EventOptions[2].OnOptionSelect = new Func<string>(this.OnOption3Select);
            this.EventOptions[2].GetExtraFormatLanguageKeys = new Func<List<string>>(this.Option3GetExtraFormatLanguageKeys);
            this.EventOptions[2].DefaultState = 0;
            this.EventOptions[2].OneTimeOnly = false;
            this.OnOption3Create();
            
            // 选项4初始化
            this.EventOptions[3].OnOptionVisibleCheck = new Func<bool>(this.OnOption4VisibleCheck);
            this.EventOptions[3].OnOptionAvailableCheck = new Func<bool>(this.OnOption4AvailableCheck);
            this.EventOptions[3].GetReplacedContent = new Func<string>(this.OnOption4GetReplacedContent);
            this.EventOptions[3].OnOptionSelect = new Func<string>(this.OnOption4Select);
            this.EventOptions[3].GetExtraFormatLanguageKeys = new Func<List<string>>(this.Option4GetExtraFormatLanguageKeys);
            this.EventOptions[3].DefaultState = 0;
            this.EventOptions[3].OneTimeOnly = false;
            this.OnOption4Create();
            
            // 选项5初始化
            this.EventOptions[4].OnOptionVisibleCheck = new Func<bool>(this.OnOption5VisibleCheck);
            this.EventOptions[4].OnOptionAvailableCheck = new Func<bool>(this.OnOption5AvailableCheck);
            this.EventOptions[4].GetReplacedContent = new Func<string>(this.OnOption5GetReplacedContent);
            this.EventOptions[4].OnOptionSelect = new Func<string>(this.OnOption5Select);
            this.EventOptions[4].GetExtraFormatLanguageKeys = new Func<List<string>>(this.Option5GetExtraFormatLanguageKeys);
            this.EventOptions[4].DefaultState = 0;
            this.EventOptions[4].OneTimeOnly = false;
            this.OnOption5Create();
            
            // 选项6初始化
            this.EventOptions[5].OnOptionVisibleCheck = new Func<bool>(this.OnOption6VisibleCheck);
            this.EventOptions[5].OnOptionAvailableCheck = new Func<bool>(this.OnOption6AvailableCheck);
            this.EventOptions[5].GetReplacedContent = new Func<string>(this.OnOption6GetReplacedContent);
            this.EventOptions[5].OnOptionSelect = new Func<string>(this.OnOption6Select);
            this.EventOptions[5].GetExtraFormatLanguageKeys = new Func<List<string>>(this.Option6GetExtraFormatLanguageKeys);
            this.EventOptions[5].DefaultState = 0;
            this.EventOptions[5].OneTimeOnly = false;
            this.OnOption6Create();
            
            // 选项7初始化
            this.EventOptions[6].OnOptionVisibleCheck = new Func<bool>(this.OnOption7VisibleCheck);
            this.EventOptions[6].OnOptionAvailableCheck = new Func<bool>(this.OnOption7AvailableCheck);
            this.EventOptions[6].GetReplacedContent = new Func<string>(this.OnOption7GetReplacedContent);
            this.EventOptions[6].OnOptionSelect = new Func<string>(this.OnOption7Select);
            this.EventOptions[6].GetExtraFormatLanguageKeys = new Func<List<string>>(this.Option7GetExtraFormatLanguageKeys);
            this.EventOptions[6].DefaultState = 0;
            this.EventOptions[6].OneTimeOnly = false;
            this.OnOption7Create();
            
            // 选项8初始化
            this.EventOptions[7].OnOptionVisibleCheck = new Func<bool>(this.OnOption8VisibleCheck);
            this.EventOptions[7].OnOptionAvailableCheck = new Func<bool>(this.OnOption8AvailableCheck);
            this.EventOptions[7].GetReplacedContent = new Func<string>(this.OnOption8GetReplacedContent);
            this.EventOptions[7].OnOptionSelect = new Func<string>(this.OnOption8Select);
            this.EventOptions[7].GetExtraFormatLanguageKeys = new Func<List<string>>(this.Option8GetExtraFormatLanguageKeys);
            this.EventOptions[7].DefaultState = 0;
            this.EventOptions[7].OneTimeOnly = false;
            this.OnOption8Create();
            
            // 选项9初始化
            this.EventOptions[8].OnOptionVisibleCheck = new Func<bool>(this.OnOption9VisibleCheck);
            this.EventOptions[8].OnOptionAvailableCheck = new Func<bool>(this.OnOption9AvailableCheck);
            this.EventOptions[8].GetReplacedContent = new Func<string>(this.OnOption9GetReplacedContent);
            this.EventOptions[8].OnOptionSelect = new Func<string>(this.OnOption9Select);
            this.EventOptions[8].GetExtraFormatLanguageKeys = new Func<List<string>>(this.Option9GetExtraFormatLanguageKeys);
            this.EventOptions[8].DefaultState = 0;
            this.EventOptions[8].OneTimeOnly = false;
            this.OnOption9Create();
            
            // 选项10初始化
            this.EventOptions[9].OnOptionVisibleCheck = new Func<bool>(this.OnOption10VisibleCheck);
            this.EventOptions[9].OnOptionAvailableCheck = new Func<bool>(this.OnOption10AvailableCheck);
            this.EventOptions[9].GetReplacedContent = new Func<string>(this.OnOption10GetReplacedContent);
            this.EventOptions[9].OnOptionSelect = new Func<string>(this.OnOption10Select);
            this.EventOptions[9].GetExtraFormatLanguageKeys = new Func<List<string>>(this.Option10GetExtraFormatLanguageKeys);
            this.EventOptions[9].DefaultState = 0;
            this.EventOptions[9].OneTimeOnly = false;
            this.OnOption10Create();
            
            // 选项11初始化
            this.EventOptions[10].OnOptionVisibleCheck = new Func<bool>(this.OnOption11VisibleCheck);
            this.EventOptions[10].OnOptionAvailableCheck = new Func<bool>(this.OnOption11AvailableCheck);
            this.EventOptions[10].GetReplacedContent = new Func<string>(this.OnOption11GetReplacedContent);
            this.EventOptions[10].OnOptionSelect = new Func<string>(this.OnOption11Select);
            this.EventOptions[10].GetExtraFormatLanguageKeys = new Func<List<string>>(this.Option11GetExtraFormatLanguageKeys);
            this.EventOptions[10].DefaultState = 0;
            this.EventOptions[10].OneTimeOnly = false;
            this.OnOption11Create();
            
            // 选项12初始化 - 新增
            this.EventOptions[11].OnOptionVisibleCheck = new Func<bool>(this.OnOption12VisibleCheck);
            this.EventOptions[11].OnOptionAvailableCheck = new Func<bool>(this.OnOption12AvailableCheck);
            this.EventOptions[11].GetReplacedContent = new Func<string>(this.OnOption12GetReplacedContent);
            this.EventOptions[11].OnOptionSelect = new Func<string>(this.OnOption12Select);
            this.EventOptions[11].GetExtraFormatLanguageKeys = new Func<List<string>>(this.Option12GetExtraFormatLanguageKeys);
            this.EventOptions[11].DefaultState = 0;
            this.EventOptions[11].OneTimeOnly = false;
            this.OnOption12Create();

            // 选项13初始化 - 新增
            this.EventOptions[12].OnOptionVisibleCheck = new Func<bool>(this.OnOption13VisibleCheck);
            this.EventOptions[12].OnOptionAvailableCheck = new Func<bool>(this.OnOption13AvailableCheck);
            this.EventOptions[12].GetReplacedContent = new Func<string>(this.OnOption13GetReplacedContent);
            this.EventOptions[12].OnOptionSelect = new Func<string>(this.OnOption13Select);
            this.EventOptions[12].GetExtraFormatLanguageKeys = new Func<List<string>>(this.Option13GetExtraFormatLanguageKeys);
            this.EventOptions[12].DefaultState = 0;
            this.EventOptions[12].OneTimeOnly = false;
            this.OnOption13Create();

            // 选项14初始化 - 新增
            this.EventOptions[13].OnOptionVisibleCheck = new Func<bool>(this.OnOption14VisibleCheck);
            this.EventOptions[13].OnOptionAvailableCheck = new Func<bool>(this.OnOption14AvailableCheck);
            this.EventOptions[13].GetReplacedContent = new Func<string>(this.OnOption14GetReplacedContent);
            this.EventOptions[13].OnOptionSelect = new Func<string>(this.OnOption14Select);
            this.EventOptions[13].GetExtraFormatLanguageKeys = new Func<List<string>>(this.Option14GetExtraFormatLanguageKeys);
            this.EventOptions[13].DefaultState = 0;
            this.EventOptions[13].OneTimeOnly = false;
            this.OnOption14Create();

            // 选项99初始化 - 返回选项
            this.EventOptions[14].OnOptionVisibleCheck = new Func<bool>(this.OnOption99VisibleCheck);
            this.EventOptions[14].OnOptionAvailableCheck = new Func<bool>(this.OnOption99AvailableCheck);
            this.EventOptions[14].GetReplacedContent = new Func<string>(this.OnOption99GetReplacedContent);
            this.EventOptions[14].OnOptionSelect = new Func<string>(this.OnOption99Select);
            this.EventOptions[14].GetExtraFormatLanguageKeys = new Func<List<string>>(this.Option99GetExtraFormatLanguageKeys);
            this.EventOptions[14].DefaultState = 0;
            this.EventOptions[14].OneTimeOnly = false;
            this.OnOption99Create();
        }

        /// <summary>
        /// 检查事件触发条件
        /// </summary>
        /// <returns>总是返回true，因为此事件由其他事件直接跳转</returns>
        public override bool OnCheckEventCondition()
        {
            return true;
        }

        /// <summary>
        /// 事件进入时执行
        /// </summary>
        public override void OnEventEnter()
        {
            // 不需要特殊处理
        }

        /// <summary>
        /// 事件退出时执行
        /// </summary>
        public override void OnEventExit()
        {
            // 不需要特殊处理
        }

        /// <summary>
        /// 获取替换内容字符串
        /// </summary>
        /// <returns>显示可以炼制多少上等药材的字符串</returns>
        public override string GetReplacedContentString()
        {
            int upgradeCount = 0;
            this.ArgBox.Get("addNewGrade", ref upgradeCount);
            return string.Format("可以炼出{0}株上等药材。", upgradeCount);
        }

        /// <summary>
        /// 获取额外的格式语言键
        /// </summary>
        /// <returns>额外的语言键列表</returns>
        public override List<string> GetExtraFormatLanguageKeys()
        {
            return null;
        }

        // ===================== 选项1-11方法 =====================

        /// <summary>
        /// 创建选项1
        /// </summary>
        private void OnOption1Create() { }

        /// <summary>
        /// 检查选项1是否可见
        /// </summary>
        /// <returns>总是可见</returns>
        private bool OnOption1VisibleCheck()
        {
            return true;
        }

        /// <summary>
        /// 检查选项1是否可用
        /// </summary>
        /// <returns>总是可用</returns>
        private bool OnOption1AvailableCheck()
        {
            return true;
        }

        /// <summary>
        /// 获取选项1的替换内容
        /// </summary>
        /// <returns>替换后的内容</returns>
        private string OnOption1GetReplacedContent()
        {
            return string.Empty;
        }

        /// <summary>
        /// 选项1被选中时执行 - 处理药材ID 234-235
        /// </summary>
        /// <returns>空字符串</returns>
        private string OnOption1Select()
        {
            this.ProcessHerbUpgrade(234, 235);
            return string.Empty;
        }

        /// <summary>
        /// 获取选项1的额外格式语言键
        /// </summary>
        /// <returns>语言键列表</returns>
        public List<string> Option1GetExtraFormatLanguageKeys()
        {
            return null;
        }

        /// <summary>
        /// 创建选项2
        /// </summary>
        private void OnOption2Create() { }

        /// <summary>
        /// 检查选项2是否可见
        /// </summary>
        /// <returns>总是可见</returns>
        private bool OnOption2VisibleCheck()
        {
            return true;
        }

        /// <summary>
        /// 检查选项2是否可用
        /// </summary>
        /// <returns>总是可用</returns>
        private bool OnOption2AvailableCheck()
        {
            return true;
        }

        /// <summary>
        /// 获取选项2的替换内容
        /// </summary>
        /// <returns>替换后的内容</returns>
        private string OnOption2GetReplacedContent()
        {
            return string.Empty;
        }

        /// <summary>
        /// 选项2被选中时执行 - 处理药材ID 222-223
        /// </summary>
        /// <returns>空字符串</returns>
        private string OnOption2Select()
        {
            this.ProcessHerbUpgrade(222, 223);
            return string.Empty;
        }

        /// <summary>
        /// 获取选项2的额外格式语言键
        /// </summary>
        /// <returns>语言键列表</returns>
        public List<string> Option2GetExtraFormatLanguageKeys()
        {
            return null;
        }

        /// <summary>
        /// 创建选项3
        /// </summary>
        private void OnOption3Create() { }

        /// <summary>
        /// 检查选项3是否可见
        /// </summary>
        /// <returns>总是可见</returns>
        private bool OnOption3VisibleCheck()
        {
            return true;
        }

        /// <summary>
        /// 检查选项3是否可用
        /// </summary>
        /// <returns>总是可用</returns>
        private bool OnOption3AvailableCheck()
        {
            return true;
        }

        /// <summary>
        /// 获取选项3的替换内容
        /// </summary>
        /// <returns>替换后的内容</returns>
        private string OnOption3GetReplacedContent()
        {
            return string.Empty;
        }

        /// <summary>
        /// 选项3被选中时执行 - 处理药材ID 150-151
        /// </summary>
        /// <returns>空字符串</returns>
        private string OnOption3Select()
        {
            this.ProcessHerbUpgrade(150, 151);
            return string.Empty;
        }

        /// <summary>
        /// 获取选项3的额外格式语言键
        /// </summary>
        /// <returns>语言键列表</returns>
        public List<string> Option3GetExtraFormatLanguageKeys()
        {
            return null;
        }

        /// <summary>
        /// 创建选项4
        /// </summary>
        private void OnOption4Create() { }

        /// <summary>
        /// 检查选项4是否可见
        /// </summary>
        /// <returns>总是可见</returns>
        private bool OnOption4VisibleCheck()
        {
            return true;
        }

        /// <summary>
        /// 检查选项4是否可用
        /// </summary>
        /// <returns>总是可用</returns>
        private bool OnOption4AvailableCheck()
        {
            return true;
        }

        /// <summary>
        /// 获取选项4的替换内容
        /// </summary>
        /// <returns>替换后的内容</returns>
        private string OnOption4GetReplacedContent()
        {
            return string.Empty;
        }

        /// <summary>
        /// 选项4被选中时执行 - 处理药材ID 166-167
        /// </summary>
        /// <returns>空字符串</returns>
        private string OnOption4Select()
        {
            this.ProcessHerbUpgrade(166, 167);
            return string.Empty;
        }

        /// <summary>
        /// 获取选项4的额外格式语言键
        /// </summary>
        /// <returns>语言键列表</returns>
        public List<string> Option4GetExtraFormatLanguageKeys()
        {
            return null;
        }

        /// <summary>
        /// 创建选项5
        /// </summary>
        private void OnOption5Create() { }

        /// <summary>
        /// 检查选项5是否可见
        /// </summary>
        /// <returns>总是可见</returns>
        private bool OnOption5VisibleCheck()
        {
            return true;
        }

        /// <summary>
        /// 检查选项5是否可用
        /// </summary>
        /// <returns>总是可用</returns>
        private bool OnOption5AvailableCheck()
        {
            return true;
        }

        /// <summary>
        /// 获取选项5的替换内容
        /// </summary>
        /// <returns>替换后的内容</returns>
        private string OnOption5GetReplacedContent()
        {
            return string.Empty;
        }

        /// <summary>
        /// 选项5被选中时执行 - 处理药材ID 154-155
        /// </summary>
        /// <returns>空字符串</returns>
        private string OnOption5Select()
        {
            this.ProcessHerbUpgrade(154, 155);
            return string.Empty;
        }

        /// <summary>
        /// 获取选项5的额外格式语言键
        /// </summary>
        /// <returns>语言键列表</returns>
        public List<string> Option5GetExtraFormatLanguageKeys()
        {
            return null;
        }

        /// <summary>
        /// 创建选项6
        /// </summary>
        private void OnOption6Create() { }

        /// <summary>
        /// 检查选项6是否可见
        /// </summary>
        /// <returns>总是可见</returns>
        private bool OnOption6VisibleCheck()
        {
            return true;
        }

        /// <summary>
        /// 检查选项6是否可用
        /// </summary>
        /// <returns>总是可用</returns>
        private bool OnOption6AvailableCheck()
        {
            return true;
        }

        /// <summary>
        /// 获取选项6的替换内容
        /// </summary>
        /// <returns>替换后的内容</returns>
        private string OnOption6GetReplacedContent()
        {
            return string.Empty;
        }

        /// <summary>
        /// 选项6被选中时执行 - 处理药材ID 186-187
        /// </summary>
        /// <returns>空字符串</returns>
        private string OnOption6Select()
        {
            this.ProcessHerbUpgrade(186, 187);
            return string.Empty;
        }

        /// <summary>
        /// 获取选项6的额外格式语言键
        /// </summary>
        /// <returns>语言键列表</returns>
        public List<string> Option6GetExtraFormatLanguageKeys()
        {
            return null;
        }

        /// <summary>
        /// 创建选项7
        /// </summary>
        private void OnOption7Create() { }

        /// <summary>
        /// 检查选项7是否可见
        /// </summary>
        /// <returns>总是可见</returns>
        private bool OnOption7VisibleCheck()
        {
            return true;
        }

        /// <summary>
        /// 检查选项7是否可用
        /// </summary>
        /// <returns>总是可用</returns>
        private bool OnOption7AvailableCheck()
        {
            return true;
        }

        /// <summary>
        /// 获取选项7的替换内容
        /// </summary>
        /// <returns>替换后的内容</returns>
        private string OnOption7GetReplacedContent()
        {
            return string.Empty;
        }

        /// <summary>
        /// 选项7被选中时执行 - 处理药材ID 218-219
        /// </summary>
        /// <returns>空字符串</returns>
        private string OnOption7Select()
        {
            this.ProcessHerbUpgrade(218, 219);
            return string.Empty;
        }

        /// <summary>
        /// 获取选项7的额外格式语言键
        /// </summary>
        /// <returns>语言键列表</returns>
        public List<string> Option7GetExtraFormatLanguageKeys()
        {
            return null;
        }

        /// <summary>
        /// 创建选项8
        /// </summary>
        private void OnOption8Create() { }

        /// <summary>
        /// 检查选项8是否可见
        /// </summary>
        /// <returns>总是可见</returns>
        private bool OnOption8VisibleCheck()
        {
            return true;
        }

        /// <summary>
        /// 检查选项8是否可用
        /// </summary>
        /// <returns>总是可用</returns>
        private bool OnOption8AvailableCheck()
        {
            return true;
        }

        /// <summary>
        /// 获取选项8的替换内容
        /// </summary>
        /// <returns>替换后的内容</returns>
        private string OnOption8GetReplacedContent()
        {
            return string.Empty;
        }

        /// <summary>
        /// 选项8被选中时执行 - 处理药材ID 182-183
        /// </summary>
        /// <returns>空字符串</returns>
        private string OnOption8Select()
        {
            this.ProcessHerbUpgrade(182, 183);
            return string.Empty;
        }

        /// <summary>
        /// 获取选项8的额外格式语言键
        /// </summary>
        /// <returns>语言键列表</returns>
        public List<string> Option8GetExtraFormatLanguageKeys()
        {
            return null;
        }

        /// <summary>
        /// 创建选项9
        /// </summary>
        private void OnOption9Create() { }

        /// <summary>
        /// 检查选项9是否可见
        /// </summary>
        /// <returns>总是可见</returns>
        private bool OnOption9VisibleCheck()
        {
            return true;
        }

        /// <summary>
        /// 检查选项9是否可用
        /// </summary>
        /// <returns>总是可用</returns>
        private bool OnOption9AvailableCheck()
        {
            return true;
        }

        /// <summary>
        /// 获取选项9的替换内容
        /// </summary>
        /// <returns>替换后的内容</returns>
        private string OnOption9GetReplacedContent()
        {
            return string.Empty;
        }

        /// <summary>
        /// 选项9被选中时执行 - 处理药材ID 198-199
        /// </summary>
        /// <returns>空字符串</returns>
        private string OnOption9Select()
        {
            this.ProcessHerbUpgrade(198, 199);
            return string.Empty;
        }

        /// <summary>
        /// 获取选项9的额外格式语言键
        /// </summary>
        /// <returns>语言键列表</returns>
        public List<string> Option9GetExtraFormatLanguageKeys()
        {
            return null;
        }

        /// <summary>
        /// 创建选项10
        /// </summary>
        private void OnOption10Create() { }

        /// <summary>
        /// 检查选项10是否可见
        /// </summary>
        /// <returns>总是可见</returns>
        private bool OnOption10VisibleCheck()
        {
            return true;
        }

        /// <summary>
        /// 检查选项10是否可用
        /// </summary>
        /// <returns>总是可用</returns>
        private bool OnOption10AvailableCheck()
        {
            return true;
        }

        /// <summary>
        /// 获取选项10的替换内容
        /// </summary>
        /// <returns>替换后的内容</returns>
        private string OnOption10GetReplacedContent()
        {
            return string.Empty;
        }

        /// <summary>
        /// 选项10被选中时执行 - 处理药材ID 214-215
        /// </summary>
        /// <returns>空字符串</returns>
        private string OnOption10Select()
        {
            this.ProcessHerbUpgrade(214, 215);
            return string.Empty;
        }

        /// <summary>
        /// 获取选项10的额外格式语言键
        /// </summary>
        /// <returns>语言键列表</returns>
        public List<string> Option10GetExtraFormatLanguageKeys()
        {
            return null;
        }

        /// <summary>
        /// 创建选项11
        /// </summary>
        private void OnOption11Create() { }

        /// <summary>
        /// 检查选项11是否可见
        /// </summary>
        /// <returns>总是可见</returns>
        private bool OnOption11VisibleCheck()
        {
            return true;
        }

        /// <summary>
        /// 检查选项11是否可用
        /// </summary>
        /// <returns>总是可用</returns>
        private bool OnOption11AvailableCheck()
        {
            return true;
        }

        /// <summary>
        /// 获取选项11的替换内容 - 攻速武用
        /// </summary>
        /// <returns>"攻速武用"</returns>
        private string OnOption11GetReplacedContent()
        {
            return "攻速武用";
        }

        /// <summary>
        /// 选项11被选中时执行 - 处理药材ID 202-203
        /// </summary>
        /// <returns>空字符串</returns>
        private string OnOption11Select()
        {
            this.ProcessHerbUpgrade(202, 203);
            return string.Empty;
        }

        /// <summary>
        /// 获取选项11的额外格式语言键
        /// </summary>
        /// <returns>语言键列表</returns>
        public List<string> Option11GetExtraFormatLanguageKeys()
        {
            return null;
        }

        // ===================== 新增选项方法 =====================

        /// <summary>
        /// 创建选项12 - 移速步伐
        /// </summary>
        private void OnOption12Create() { }

        /// <summary>
        /// 检查选项12是否可见
        /// </summary>
        /// <returns>总是可见</returns>
        private bool OnOption12VisibleCheck()
        {
            return true;
        }

        /// <summary>
        /// 检查选项12是否可用
        /// </summary>
        /// <returns>总是可用</returns>
        private bool OnOption12AvailableCheck()
        {
            return true;
        }

        /// <summary>
        /// 获取选项12的替换内容 - 移速步伐
        /// </summary>
        /// <returns>"移速步伐"</returns>
        private string OnOption12GetReplacedContent()
        {
            return "移速步伐";
        }

        /// <summary>
        /// 选项12被选中时执行 - 处理药材ID 170-171
        /// </summary>
        /// <returns>空字符串</returns>
        private string OnOption12Select()
        {
            this.ProcessHerbUpgrade(170, 171);
            return string.Empty;
        }

        /// <summary>
        /// 获取选项12的额外格式语言键
        /// </summary>
        /// <returns>语言键列表</returns>
        public List<string> Option12GetExtraFormatLanguageKeys()
        {
            return null;
        }

        /// <summary>
        /// 创建选项13 - 提架回复
        /// </summary>
        private void OnOption13Create() { }

        /// <summary>
        /// 检查选项13是否可见
        /// </summary>
        /// <returns>总是可见</returns>
        private bool OnOption13VisibleCheck()
        {
            return true;
        }

        /// <summary>
        /// 检查选项13是否可用
        /// </summary>
        /// <returns>总是可用</returns>
        private bool OnOption13AvailableCheck()
        {
            return true;
        }

        /// <summary>
        /// 获取选项13的替换内容 - 提架回复
        /// </summary>
        /// <returns>"提架回复"</returns>
        private string OnOption13GetReplacedContent()
        {
            return "提架回复";
        }

        /// <summary>
        /// 选项13被选中时执行 - 处理药材ID 206-207
        /// </summary>
        /// <returns>空字符串</returns>
        private string OnOption13Select()
        {
            this.ProcessHerbUpgrade(206, 207);
            return string.Empty;
        }

        /// <summary>
        /// 获取选项13的额外格式语言键
        /// </summary>
        /// <returns>语言键列表</returns>
        public List<string> Option13GetExtraFormatLanguageKeys()
        {
            return null;
        }

        /// <summary>
        /// 创建选项14 - 施展引奇
        /// </summary>
        private void OnOption14Create() { }

        /// <summary>
        /// 检查选项14是否可见
        /// </summary>
        /// <returns>总是可见</returns>
        private bool OnOption14VisibleCheck()
        {
            return true;
        }

        /// <summary>
        /// 检查选项14是否可用
        /// </summary>
        /// <returns>总是可用</returns>
        private bool OnOption14AvailableCheck()
        {
            return true;
        }

        /// <summary>
        /// 获取选项14的替换内容 - 施展引奇
        /// </summary>
        /// <returns>"施展引奇"</returns>
        private string OnOption14GetReplacedContent()
        {
            return "施展引奇";
        }

        /// <summary>
        /// 选项14被选中时执行 - 处理药材ID 230-231
        /// </summary>
        /// <returns>空字符串</returns>
        private string OnOption14Select()
        {
            this.ProcessHerbUpgrade(230, 231);
            return string.Empty;
        }

        /// <summary>
        /// 获取选项14的额外格式语言键
        /// </summary>
        /// <returns>语言键列表</returns>
        public List<string> Option14GetExtraFormatLanguageKeys()
        {
            return null;
        }

        // ===================== 返回选项方法 =====================

        /// <summary>
        /// 创建返回选项
        /// </summary>
        private void OnOption99Create() { }

        /// <summary>
        /// 检查返回选项是否可见
        /// </summary>
        /// <returns>总是可见</returns>
        private bool OnOption99VisibleCheck()
        {
            return true;
        }

        /// <summary>
        /// 检查返回选项是否可用
        /// </summary>
        /// <returns>总是可用</returns>
        private bool OnOption99AvailableCheck()
        {
            return true;
        }

        /// <summary>
        /// 获取返回选项的替换内容
        /// </summary>
        /// <returns>"(返回)"</returns>
        private string OnOption99GetReplacedContent()
        {
            return "(返回)";
        }

        /// <summary>
        /// 返回选项被选中时执行 - 返回上一级事件
        /// </summary>
        /// <returns>返回上一级事件的GUID</returns>
        private string OnOption99Select()
        {
            return "2bd02acd-244e-4f81-a194-502a0389c3f6";
        }

        /// <summary>
        /// 获取返回选项的额外格式语言键
        /// </summary>
        /// <returns>语言键列表</returns>
        public List<string> Option99GetExtraFormatLanguageKeys()
        {
            return null;
        }

        // ===================== 公共辅助方法 =====================

        /// <summary>
        /// 检查药材编号是否有效
        /// </summary>
        /// <param name="itemId">药材ID</param>
        /// <param name="grade">药材等级</param>
        /// <returns>是否有效</returns>
        private bool IsValidNumber(int itemId, int grade)
        {
            // 检查药材ID范围：140-235
            // 药材ID公式：140 + (品级*4) + 等级偏移
            // 其中grade为余数：0,1,2,3对应不同等级
            return itemId >= 140 && itemId <= 235 && (itemId - 140) % 4 == grade;
        }

        /// <summary>
        /// 核心药材升级处理方法
        /// 从背包中移除低等级药材，添加高等级药材
        /// </summary>
        /// <param name="newHerbIdQi">新的奇类药材ID</param>
        /// <param name="newHerbIdJue">新的绝类药材ID</param>
        public void ProcessHerbUpgrade(short newHerbIdQi, short newHerbIdJue)
        {
            int requiredGrade = 0;
            this.ArgBox.Get("GradeReq", ref requiredGrade);
            
            int totalHerbCount = 0;
            Character playerCharacter = this.ArgBox.GetCharacter("RoleTaiwu");
            Inventory inventory = playerCharacter.GetInventory();
            
            // 遍历背包，统计符合条件的药材数量并移除
            foreach (KeyValuePair<ItemKey, int> itemPair in inventory.Items)
            {
                ItemKey itemKey = itemPair.Key;
                int itemCount = itemPair.Value;
                
                // 只处理药材类型（ItemType == 5）
                if (itemKey.ItemType == 5)
                {
                    short templateId = itemKey.TemplateId;
                    
                    // 检查是否是需求等级对应的药材
                    bool isValidHerb = this.IsValidNumber((int)templateId, requiredGrade);
                    if (isValidHerb)
                    {
                        totalHerbCount += itemCount;
                        // 移除低等级药材
                        EventHelper.RemoveInventoryItem(playerCharacter, itemKey, itemCount, true);
                    }
                }
            }
            
            // 根据需求等级计算可升级的数量
            int upgradeCount = 0;
            if (requiredGrade == 1)
            {
                // 等级1的药材：每3个升级1个
                upgradeCount = totalHerbCount / 3;
            }
            else if (requiredGrade == 2)
            {
                // 等级2的药材：每5个升级1个
                upgradeCount = totalHerbCount / 5;
            }
            
            // 如果没有可升级的数量，直接返回
            if (upgradeCount <= 0)
            {
                return;
            }
            
            // 根据需求等级添加对应的高等级药材
            if (requiredGrade == 1)
            {
                // 添加奇类药材
                ItemKey newItem = EventHelper.AddItemToRole(playerCharacter, 5, newHerbIdQi, upgradeCount, -1);
                EventHelper.ShowGetItemPageForItems(
                    new List<ValueTuple<ItemKey, int>>
                    {
                        new ValueTuple<ItemKey, int>(newItem, upgradeCount)
                    }, 
                    "", 
                    this.ArgBox, 
                    false
                );
            }
            else if (requiredGrade == 2)
            {
                // 添加绝类药材
                ItemKey newItem = EventHelper.AddItemToRole(playerCharacter, 5, newHerbIdJue, upgradeCount, -1);
                EventHelper.ShowGetItemPageForItems(
                    new List<ValueTuple<ItemKey, int>>
                    {
                        new ValueTuple<ItemKey, int>(newItem, upgradeCount)
                    }, 
                    "", 
                    this.ArgBox, 
                    false
                );
            }
        }
    }
}