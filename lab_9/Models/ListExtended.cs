﻿using System;
using System.Collections.Generic;

namespace lab_9.Models
{
    internal static class ListExtended
    {
        public static int FindIndex<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            int i = 0;
            foreach (var item in source)
            {
                if (predicate(item))
                    return i;
                i++;
            }
            return -1;
        }
    }
}
