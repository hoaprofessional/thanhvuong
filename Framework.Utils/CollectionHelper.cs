using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Utils
{
    public static class CollectionHelper
    {
        public static bool In<T>(this T elem, T[] array)
        {
            foreach (var item in array)
            {
                if (item.Equals(elem))
                    return true;
            }
            return false;
        }

    }
}
