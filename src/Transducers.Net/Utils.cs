﻿using System.Collections.Generic;

namespace Transducers.Net
{
    public class Utils
    {
        public static List<TSource> Appender<TSource>(List<TSource> acc, TSource source)
        {
            acc.Add(source);
            return acc;
        }
    }
}
