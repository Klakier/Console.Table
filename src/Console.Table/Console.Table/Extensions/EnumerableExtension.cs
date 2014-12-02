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
    }
}