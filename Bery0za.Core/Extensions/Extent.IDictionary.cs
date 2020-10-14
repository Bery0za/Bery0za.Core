using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Bery0za.Core.Extensions
{
    public static partial class Extent
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
                                                             TKey key,
                                                             TValue defaultValue)
        {
            return dictionary.TryGetValue(key, out TValue value) ? value : defaultValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
                                                             TKey key,
                                                             Func<TValue> defaultValueProvider)
        {
            return dictionary.TryGetValue(key, out TValue value) ? value : defaultValueProvider();
        }
    }
}