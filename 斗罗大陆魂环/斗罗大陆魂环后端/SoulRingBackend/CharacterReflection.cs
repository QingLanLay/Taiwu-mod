using System.Reflection;
using GameData.Domains.Character;
using HarmonyLib;

namespace SoulRingBackend;

/// <summary>太吾角色字段反射工具类</summary>
public static class CharacterReflection
{
    private static readonly AccessTools.FieldRef<Character, PreexistenceCharIds> _preexistenceCharIdsRef;

    /// <summary>静态构造函数：初始化字段反射引用</summary>
    static CharacterReflection()
    {
        _preexistenceCharIdsRef = AccessTools.FieldRefAccess<Character, PreexistenceCharIds>("_preexistenceCharIds");
    }

    public static PreexistenceCharIds GetPreexistenceCharIds(Character character) =>
        _preexistenceCharIdsRef?.Invoke(character) ?? default;

    public static void ChangePreexistence(Character taiwu, PreexistenceCharIds preexistenceCharIds)
    {
        FieldInfo fieldInfo = typeof(Character).GetField("_preexistenceCharIds",
            BindingFlags.NonPublic | BindingFlags.Instance);
        fieldInfo?.SetValue(taiwu, preexistenceCharIds);
    }

    public static bool GetOfflineAddFeatureMethod(Character taiwu, short featureId, bool removeMutexFeature,
        bool removeLowerOnly = false)
    {
        // 获取添加特性方法
        MethodInfo offlineAddFeatureMethod = AccessTools.Method(typeof(Character), "OfflineAddFeature",
            new Type[] { typeof(short), typeof(bool), typeof(bool) });
        var result =
            offlineAddFeatureMethod.Invoke(taiwu,
                new object[] { featureId, removeMutexFeature, removeLowerOnly });
        if (result is not null)
        {
            return (bool)result;
        }
        return false;
    }
}