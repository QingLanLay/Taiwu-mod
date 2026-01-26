using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FrameWork;
using GameData.Domains;
using GameData.Domains.Building;
using GameData.Utilities;
using HarmonyLib;
using NPOI.Util;
using TMPro;
using UICommon.Character;
using UnityEngine;

namespace SoulRingFrontend
{
    public static class SoulRingUI
    {
        // 新增：防止重复点击的标志
        private static bool _isShowingEffect = false;

        private static GameObject _swapEditAvatar;

        // 新增：魂环角色的Avatar和Name
        private static CharacterAvatar _soulRingCharacterAvatar;
        private static CharacterName _soulRingCharacterName;

        // 新增：魂环可用角色列表
        private static List<int> _soulRingCharIdList;

        // 保存Refers引用
        private static Refers _soulCharacterBg;
        private static Refers _soulCharacterRefers;

        // 化魂阁组件
        private static UI_SwapSoul _swapSoul;

        public static UI_SwapSoul SwapSoul
        {
            get => _swapSoul;
            set => _swapSoul = value;
        }


        // 新选择头像框
        private static GameObject _soulRingAvatarBg;
        private static Refers _soulRingRefers;
        private static UICommon.Character.Avatar.Avatar _soulCharacter;
        private static GameObject _soulEditAvatar;


        // 新增：按钮组件
        private static CButton _soulRingButton;


        public static void SoulRingInit(UI_SwapSoul __instance)
        {
            if (__instance != null)
            {
                SwapSoul = __instance;
                SoulCharacterBg = SwapSoul.CGet<Refers>("SoulCharacterBg");
                _swapEditAvatar = SwapSoul.CGet<CButton>("EditAvatar").gameObject;
            }

            // 初始化人物栏
            CopySoulCharacterBg();
            // 初始化确定按钮
            CopySoulEvant();
        }

        public static void CopySoulCharacterBg()
        {
            // 从UI_SwapSoul获取SoulCharacterBg的Refers组件
            try
            {
                if (SoulCharacterBg != null)
                {
                    // 获取Refers组件所在的GameObject
                    GameObject original = SoulCharacterBg.gameObject;

                    // 复制GameObject
                    _soulRingAvatarBg = GameObject.Instantiate(original, original.transform.parent);
                    _soulRingAvatarBg.name = "SoulRingAvatarBg";

                    // 获取复制后的Refers组件
                    Refers copyRefers = _soulRingAvatarBg.GetComponent<Refers>();
                    _soulRingRefers = copyRefers;

                    if (_soulRingRefers != null)
                    {
                        // 获取Avatar组件
                        UICommon.Character.Avatar.Avatar avatar =
                            _soulRingRefers.CGet<UICommon.Character.Avatar.Avatar>("Avatar");

                        // 初始化魂环角色的Avatar和Name
                        _soulRingCharacterAvatar = new CharacterAvatar(avatar, true);
                        _soulRingCharacterAvatar.CanShowGrave = false; // 根据需求设置

                        // 获取名字文本组件
                        TextMeshProUGUI nameText = _soulRingRefers.CGet<TextMeshProUGUI>("Name");
                        _soulRingCharacterName = new CharacterName(nameText, null, null);

                        // 初始状态设为未选择
                        _soulRingCharacterAvatar.CharacterId = -1;
                        _soulRingCharacterName.CharacterId = -1;

                        // 调整文字
                        var titleTextGameObj = _soulRingAvatarBg.transform.Find("Image_Title_Bg/Label_AddSoul");
                        if (titleTextGameObj != null)
                        {
                            titleTextGameObj.GetComponent<TextMeshProUGUI>().text = "选择魂环";
                        }

                        // 调整整体位置
                        RectTransform copyRT = _soulRingAvatarBg.GetComponent<RectTransform>();
                        copyRT.anchoredPosition =
                            new Vector2(copyRT.anchoredPosition.x + 200, copyRT.anchoredPosition.y - 300);

                        // 获取并设置按钮点击事件
                        _soulRingButton = _soulRingRefers.gameObject.GetComponentInChildren<CButton>();
                        if (_soulRingButton != null)
                        {
                            // 修改按钮名称以便区分
                            _soulRingButton.name = "SoulRingCharacter";
                            AdaptableLog.Info("成功给按钮添加委托");
                            _soulRingButton.onClick.AddListener(SelectSoulRingCharacter);
                        }

                        // 初始化角色ID列表
                        _soulRingCharIdList = new List<int>();
                    }

                    AdaptableLog.Info("SoulRing人物框复制成功");
                }
            }
            catch (Exception e)
            {
                AdaptableLog.Error("SoulRingAvatar复制失败:" + e);
            }
        }


        public static void SelectSoulRingCharacter()
        {
            try
            {
                if (SwapSoul == null)
                {
                    AdaptableLog.Error("UI_SwapSoul实例未找到");
                    return;
                }

                // 使用反射获取原系统的灵魂角色列表
                var soulCharIdListField = typeof(UI_SwapSoul).GetField("_soulCharIdList",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                List<int> soulCharIdList = null;
                if (soulCharIdListField != null)
                {
                    soulCharIdList = soulCharIdListField.GetValue(SwapSoul) as List<int>;
                }

                if (soulCharIdList == null)
                {
                    soulCharIdList = new List<int>();
                }

                AdaptableLog.Info($"打开魂环选择界面，可用角色数: {soulCharIdList.Count}");

                // 创建参数框
                ArgumentBox box = EasyPool.Get<ArgumentBox>();

                // 设置参数（与原系统选择灵魂时相同）
                box.Set("ShowNone", true);
                box.Set("isDeadCharScroll", true); // 允许选择死亡角色
                box.Set("CanSelectInfectedChar", true); // 允许选择感染角色
                box.Set("selectedCharId", _soulRingCharacterAvatar.CharacterId);
                box.SetObject("charIdList", soulCharIdList);

                // 设置回调函数 - 修改为你的回调方法
                box.SetObject("callback", new Action<int>(RefreshSoulRingCharacter));

                // 获取选择界面的Element
                UIElement selectCharElement = UIElement.SelectChar;
                if (selectCharElement == null)
                {
                    AdaptableLog.Error("SelectChar UIElement未找到");
                    return;
                }

                // 设置初始化参数并显示界面
                selectCharElement.SetOnInitArgs(box);

                // 获取UIManager实例
                UIManager uiManager = UIManager.Instance;
                if (uiManager == null)
                {
                    AdaptableLog.Error("UIManager实例未找到");
                    return;
                }

                // 显示选择界面
                uiManager.ShowUI(selectCharElement);


                AdaptableLog.Info("魂环选择界面已打开");
            }
            catch (Exception ex)
            {
                AdaptableLog.Error($"打开魂环选择界面失败: {ex.Message}\\n{ex.StackTrace}");
            }
        }


        /// <summary>
        /// 刷新魂环角色显示（完整版）
        /// </summary>
        public static void RefreshSoulRingCharacter(int charId)
        {
            try
            {
                AdaptableLog.Info($"刷新魂环角色: {charId}");

                // 1. 更新角色信息
                _soulRingCharacterAvatar.CharacterId = charId;
                _soulRingCharacterName.CharacterId = charId;

                // 2. 更新UI显示 - 这是你缺少的关键部分
                if (_soulRingRefers != null)
                {
                    GameObject noneObj = _soulRingRefers.CGet<GameObject>("None");
                    GameObject characterInfoObj = _soulRingRefers.CGet<GameObject>("CharacterInfo");

                    if (noneObj != null) noneObj.SetActive(charId == -1);
                    if (characterInfoObj != null) characterInfoObj.SetActive(charId != -1);
                }

                // 3. 更新按钮状态
                UpdateButtonState(charId);

                // 4. 保存选择
                SoulRingCharacterId = charId;

                AdaptableLog.Info($"魂环角色已重置: {charId}");
            }
            catch (Exception ex)
            {
                AdaptableLog.Error($"刷新魂环角色失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 更新按钮状态
        /// </summary>
        private static void UpdateButtonState(int charId)
        {
            if (_soulRingButton == null) return;

            bool hasSelected = charId != -1;

            if (hasSelected)
            {
                GetRefreshSoulCharacter(SwapSoul);
                SoulCharacterBg.gameObject.SetActive(false);
            }
            else
            {
                SoulCharacterBg.gameObject.SetActive(true);
            }

            // 更新按钮外观
            UpdateButtonAppearance(hasSelected);
        }

        /// <summary>
        /// 更新按钮外观
        /// </summary>
        private static void UpdateButtonAppearance(bool isSelected)
        {
            var buttonText = _soulRingButton.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = isSelected ? _soulRingCharacterName.Name : "选择魂环";
            }
        }

        /// <summary>
        /// 复制确定UI
        /// </summary>
        // 修改CopySoulEvant方法中的按钮点击事件
        public static void CopySoulEvant()
        {
            _soulEditAvatar = GameObject.Instantiate(_swapEditAvatar, _soulRingAvatarBg.transform);
            var rectTransform = _soulEditAvatar.GetComponent<RectTransform>();
            _soulEditAvatar.name = "EditAvatar";
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, -150f);
            _soulEditAvatar.SetActive(true);

            // 设置回调方法
            var cButton = _soulEditAvatar.GetComponent<CButton>();
            if (cButton != null)
            {
                cButton.onClick.RemoveAllListeners();
                cButton.onClick.AddListener(() =>
                {
                    if (!IsShowingEffect)
                    {
                        IsShowingEffect = true;
                        CallMethod.CallBackendSoulRing();
                        // 2秒后重置标志，防止卡死
                        SoulRingUI.SwapSoul.StartCoroutine(ResetEffectFlag());
                    }
                });
            }

            // 从_swapEditAvatar开始，在所有子物体中查找TextMeshProUGUI组件
            TextMeshProUGUI[] allTextComponents = _soulEditAvatar.GetComponentsInChildren<TextMeshProUGUI>(true);
            foreach (var textComponent in allTextComponents)
            {
                textComponent.text = "化魂转环";
            }

            // 修改tip内容
            MouseTipDisplayer mouseTipDisplayer = _soulEditAvatar.GetComponent<MouseTipDisplayer>();
            if (mouseTipDisplayer != null)
            {
                mouseTipDisplayer.IsLanguageKey = false;
                mouseTipDisplayer.PresetParam = new string[] { "化魂转环", "选择灵魂附身到自己的轮回之中，成为太吾魂环！" };
            }
        }

        // 新增：重置标志的协程
        private static IEnumerator ResetEffectFlag()
        {
            yield return new WaitForSeconds(3f); // 等待特效完成
            
            IsShowingEffect = false;
        }

        // 选择的人物
        public static int SoulRingCharacterId { get; set; }

        public static Refers SoulCharacterBg
        {
            get => _soulCharacterBg;
            set => _soulCharacterBg = value;
        }

        public static bool IsShowingEffect
        {
            get => _isShowingEffect;
            set => _isShowingEffect = value;
        }

        public static void GetRefreshSoulCharacter(UI_SwapSoul ui_SwapSoul, int charId = -1)
        {
            // 获取添加特性方法
            MethodInfo refreshSoulCharacter = AccessTools.Method(typeof(UI_SwapSoul), "RefreshSoulCharacter",
                new Type[] { typeof(int) });

            refreshSoulCharacter.Invoke(ui_SwapSoul, new object[] { charId });
        }

        // 在 SoulRingUI 类中添加这个方法
        public static void ResetAllStatus()
        {
            IsShowingEffect = false;
            // 如果有正在运行的 DOTween 动画，也建议在这里 Kill 掉
            // if (SwapSoul != null && SwapSoul.FadeToRedImg != null) SwapSoul.FadeToRedImg.DOKill();
        }
        
        // 在 SoulRingUI.cs 中优化你的协程

    }
}