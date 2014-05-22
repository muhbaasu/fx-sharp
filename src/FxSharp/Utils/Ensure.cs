using System;

namespace FxSharp.Utils
{
    internal class Ensure
    {
        public static void NotNull<T>(T o, string source)
        {
            // ReSharper disable once CompareNonConstrainedGenericWithNull
            if (typeof (T).IsClass && o == null)
            {
                throw new ArgumentNullException(source);
            }
        }
    }
}