using System.Collections;
using System.Collections.Generic;

using Bery0za.Core.Collections;

namespace Bery0za.Core.Extensions
{
    public static partial class Extent
    {
        public static IEnumerable<T> AsEnumerable<T>(this T startNode)
            where T : ILinked<T>
        {
            LinkedEnumerator<T> enumerator = new LinkedEnumerator<T>(startNode);

            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }
    }
}