using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Bery0za.Core.Extensions
{
    public static partial class Extent
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            foreach (TSource item in source)
            {
                action(item);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource, int> action)
        {
            int id = 0;
            foreach (TSource item in source)
            {
                action(item, id);
                id++;
            }
        }
    }
}
