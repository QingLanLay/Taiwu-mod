using System.Collections;
using System.Reflection;
using GameData.ArchiveData;
using GameData.Common;
using GameData.Domains;
using GameData.Domains.Character;
using GameData.Domains.Item;
using GameData.Domains.TaiwuEvent.EventHelper;
using GameData.Utilities;
using GameData.Utilities.Mod;
using HarmonyLib;
using TaiwuModdingLib.Core.Plugin;

namespace DeletMyCustomItem;

[PluginConfig("DeletMyCustomItem", "LazyDog", "1.0.0")]
public class DeletCustomItem : TaiwuRemakePlugin
{
    public override void Initialize()
    {
        harmony = new Harmony("DeletMyCustomItem");
        // 显式注册每个补丁类，而不是使用PatchAll()
        harmony.PatchAll(typeof(DeletCustomItem)); // 主类中的补丁
        harmony.PatchAll(typeof(CreateDeepCopyPatch)); // 独立的补丁类
        
        // 验证补丁是否应用成功
        var originalMethod = AccessTools.Method(typeof(ConfigDataModificationUtils), "CreateDeepCopy");
        if (originalMethod != null)
        {
            var patchInfo = Harmony.GetPatchInfo(originalMethod);
            if (patchInfo != null && patchInfo.Prefixes.Count > 0)
            {
                AdaptableLog.Info($"[DeletMyCustomItem] CreateDeepCopy补丁应用成功，前缀数量: {patchInfo.Prefixes.Count}");
            }
            else
            {
                AdaptableLog.Error("[DeletMyCustomItem] CreateDeepCopy补丁未应用！");
            }
        }
    }

    public override void Dispose()
    {
        if (harmony != null)
        {
            harmony.UnpatchSelf();
            AdaptableLog.Info("关闭mod");
        }
    }

    private Harmony harmony;

    [HarmonyPrefix]
    [HarmonyPatch(typeof(ItemDomain), "DiscardItemOptional", new Type[]
    {
        typeof(DataContext),
        typeof(int),
        typeof(ItemKey),
        typeof(sbyte),
        typeof(int)
    })]
    public static bool DiscardItemOptional_Custom(ItemDomain __instance, DataContext context, int charId,
        ItemKey itemKey, sbyte itemSourceType, int count = 1)
    {
        bool flag = itemKey.TemplateId == 11061 || itemKey.TemplateId == 11062;
        if (flag)
        {
            // 从世界移除道具
            GameData.Domains.Character.Character character = DomainManager.Character.GetElement_Objects(charId);
            EventHelper.RemoveInventoryItem(character, itemKey, 1, true);

            // 手动添加
            OperationAdder.FixedObjectCollection_Remove<int>(6, 12, itemKey.Id);
            return false;
        }

        return true;
    }
}

[HarmonyPatch(typeof(ConfigDataModificationUtils))]
[HarmonyPatch("CreateDeepCopy")]
public static class CreateDeepCopyPatch
{
    // 确保方法签名与原始方法完全一致
    public static bool Prefix(object obj, ref object __result)
    {
        AdaptableLog.Info("[DeletMyCustomItem] CreateDeepCopy补丁已触发！");
        
        if (obj == null)
        {
            __result = null;
            return false; // 跳过原始方法
        }

        try
        {
            __result = DeepCopyByReflection(obj);
            AdaptableLog.Info($"[DeletMyCustomItem] 深拷贝成功，类型: {obj.GetType().Name}");
            return false; // 跳过原始方法
        }
        catch (Exception ex)
        {
            AdaptableLog.Error($"[DeletMyCustomItem] 深拷贝失败: {ex}");
            __result = null;
            return false;
        }
    }

    /// <summary>
    /// 通过反射实现深拷贝，避免 BinaryFormatter 的序列化要求。
    /// </summary>
    private static object DeepCopyByReflection(object obj)
    {
        if (obj == null) return null;

        Type type = obj.GetType();

        // 处理值类型（如int, struct）和字符串（不可变）
        if (type.IsValueType || obj is string)
        {
            return obj;
        }

        // 处理数组
        if (type.IsArray)
        {
            Type elementType = type.GetElementType();
            Array originalArray = (Array)obj;
            Array newArray = Array.CreateInstance(elementType, originalArray.Length);
            for (int i = 0; i < originalArray.Length; i++)
            {
                newArray.SetValue(DeepCopyByReflection(originalArray.GetValue(i)), i);
            }

            return newArray;
        }

        // 处理集合（如List<>），可根据需要扩展其他集合类型
        if (obj is IList && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
        {
            IList originalList = (IList)obj;
            IList newList = (IList)Activator.CreateInstance(type);
            foreach (var item in originalList)
            {
                newList.Add(DeepCopyByReflection(item));
            }

            return newList;
        }

        // 处理普通的引用类型对象
        object newInstance = Activator.CreateInstance(type, true); // true 表示调用非公共构造函数（如果有）

        // 拷贝所有字段（包括私有和公共实例字段）
        FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (FieldInfo field in fields)
        {
            object fieldValue = field.GetValue(obj);
            object copiedFieldValue = DeepCopyByReflection(fieldValue);
            field.SetValue(newInstance, copiedFieldValue);
        }

        // 如果需要，也可以类似地处理属性（Property），但需注意可能引发逻辑副作用
        // PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        // foreach (PropertyInfo prop in properties) {
        //     if (prop.CanRead && prop.CanWrite) {
        //         object propValue = prop.GetValue(obj);
        //         object copiedPropValue = DeepCopyByReflection(propValue);
        //         prop.SetValue(newInstance, copiedPropValue);
        //     }
        // }

        return newInstance;
    }
}