using System;
using System.Runtime.CompilerServices;

namespace Bery0za.Core
{
    public static partial class Guard
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InRange<T>(T value, T min, T max, string message = null)
            where T : IComparable<T>
        {
            if (value.CompareTo(min) <= 0 || value.CompareTo(max) >= 0) throw new ArgumentException(message ?? $"{value} must be in range ({min}; {max}).");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InRangeInclusive<T>(T value, T min, T max, string message = null)
            where T : IComparable<T>
        {
            if (value.CompareTo(min) < 0 || value.CompareTo(max) > 0) throw new ArgumentException(message ?? $"{value} must be in range [{min}; {max}].");
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Equal<T>(T a, T b, string message = null)
            where T : IComparable<T>
        {
            if (a.CompareTo(b) != 0) throw new ArgumentException(message ?? $"{nameof(a)} is not equal to {nameof(b)}.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotEqual<T>(T a, T b, string message = null)
            where T : IComparable<T>
        {
            if (a.CompareTo(b) == 0) throw new ArgumentException(message ?? $"{nameof(a)} is equal to {nameof(b)}.");
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GreaterThan<T>(T a, T b, string message = null)
            where T : IComparable<T>
        {
            if (a.CompareTo(b) <= 0) throw new ArgumentException(message ?? $"{nameof(a)} is less than {nameof(b)} or equal.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GreaterThanOrEqual<T>(T a, T b, string message = null)
            where T : IComparable<T>
        {
            if (a.CompareTo(b) < 0) throw new ArgumentException(message ?? $"{nameof(a)} is less than {nameof(b)}.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LessThan<T>(T a, T b, string message = null)
            where T : IComparable<T>
        {
            if (a.CompareTo(b) >= 0) throw new ArgumentException(message ?? $"{nameof(a)} is greater than {nameof(b)} or equal.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LessThanOrEqual<T>(T a, T b, string message = null)
            where T : IComparable<T>
        {
            if (a.CompareTo(b) > 0) throw new ArgumentException(message ?? $"{nameof(a)} is greater than {nameof(b)}.");
        }
    }
}
