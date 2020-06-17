using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace Dreamland.Core.LinearWorkFlow
{
    /// <summary>
    ///     给任意对象添加属性
    /// </summary>
    internal static class AttachedProperty
    {
        private static ConditionalWeakTable<object, ConcurrentDictionary<string, object>> AttachePropertyList { get; } =
            new ConditionalWeakTable<object, ConcurrentDictionary<string, object>>();

        /// <summary>
        ///     设置属性
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void SetAttachedProperty(this object obj, string name, object value)
        {
            var dictionary = AttachePropertyList.GetValue(obj, key => new ConcurrentDictionary<string, object>());
            dictionary?.AddOrUpdate(name, value, (s, o) => value);
        }

        /// <summary>
        ///     获取属性
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static object GetAttachedProperty(this object obj, string name)
        {
            if (!AttachePropertyList.TryGetValue(obj, out var dictionary)) return null;
            return dictionary.TryGetValue(name, out var value) ? value : null;
        }
    }
}