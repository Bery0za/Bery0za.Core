using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Bery0za.Core
{
    public static partial class Guard
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotNull<T>(T obj, string message = null)
            where T : class
        {
            if (obj == null) throw new NullReferenceException(message ?? "Given object can't be null.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotNull<T>(T obj, Exception exception)
            where T : class
        {
            if (obj == null) throw exception ?? new NullReferenceException("Given object can't be null.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotNullOrEmpty(string str, string message = null)
        {
            if (str == null) throw new NullReferenceException(message ?? "Given string can't be null.");
            if (str == String.Empty) throw new ArgumentException(message ?? "Given string can't be empty.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotNullOrEmpty(string str, Exception exception)
        {
            if (str == null) throw exception ?? new NullReferenceException("Given string can't be null.");
            if (str == String.Empty) throw exception ?? new ArgumentException("Given string can't be empty.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotNullOrEmpty<T>(IEnumerable<T> collection, string message = null)
        {
            if (collection == null) throw new NullReferenceException(message ?? "Given collection can't be null.");
            if (!collection.Any()) throw new ArgumentException(message ?? "Given collection can't be empty.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotNullOrEmpty<T>(IEnumerable<T> collection, Exception exception)
        {
            if (collection == null) throw exception ?? new NullReferenceException("Given collection can't be null.");
            if (!collection.Any()) throw exception ?? new ArgumentException("Given collection can't be empty.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TrueThat(bool condition, string message = null)
        {
            if (!condition) throw new Exception(message ?? "Condition is false.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TrueThat(bool condition, Exception exception)
        {
            if (!condition) throw exception ?? new Exception("Condition is false.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FalseThat(bool condition, string message = null)
        {
            if (condition) throw new Exception(message ?? "Condition is true.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FalseThat(bool condition, Exception exception)
        {
            if (condition) throw exception ?? new Exception("Condition is true.");
        }
    }
}
