using System;
using System.Collections.Generic;

namespace Console.Table.Extensions
{
    public static class EnumerableExtension
    {
        public static void ForEeach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var element in collection)
            {
                action(element);
            }
        }

        public static IEnumerable<T> Separte<T>(this IEnumerable<T> collection, Func<T> separtor)
        {
            var enumerator = collection.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                yield break;
            }

            yield return enumerator.Current;

            while (enumerator.MoveNext())
            {
                yield return separtor();
                yield return enumerator.Current;
            }

        }
    }
}