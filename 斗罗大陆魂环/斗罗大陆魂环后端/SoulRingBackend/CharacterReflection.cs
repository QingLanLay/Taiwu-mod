using System.Reflection;
using GameData.ArchiveData;
using GameData.Common;
using GameData.Domains.Character;
using GameData.Domains.TaiwuEvent.EventHelper;
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

    public static unsafe void ChangePreexistence(Character taiwu, DataContext context,
        PreexistenceCharIds preexistenceCharIds)
    {
        // 获取轮回角色列表
        FieldInfo fieldInfo = typeof(Character).GetField("_preexistenceCharIds",
            BindingFlags.NonPublic | BindingFlags.Instance);
        
        fieldInfo?.SetValue(taiwu, preexistenceCharIds);
        

        // 获取存储轮回方法的反射
        MethodInfo SetModifiedAndInvalidateInfluencedCache = AccessTools.Method(typeof(Character),
            "SetModifiedAndInvalidateInfluencedCache",
            new Type[] { typeof(ushort), typeof(DataContext) });

        SetModifiedAndInvalidateInfluencedCache.Invoke(taiwu,
            new object[] { (ushort)64, context });

        bool isArchive = taiwu.CollectionHelperData.IsArchive;
        if (isArchive)
        {
            byte* pData = OperationAdder.DynamicObjectCollection_SetFixedField<int>(taiwu.CollectionHelperData.DomainId,
                taiwu.CollectionHelperData.DataId, taiwu.GetId(), 954U, 52);

            var preCharaIds = (PreexistenceCharIds)(fieldInfo?.GetValue(taiwu) ?? throw new InvalidOperationException());

            pData += preCharaIds.Serialize(pData);
        }
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