namespace FxSharp
{
    public static class Prelude
    {
        /// <summary>
        ///     The identity function.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T Id<T>(T t)
        {
            return t;
        }

        /// <summary>
        ///     A binary function that always returns its first argument.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static T Const<T>(T a, T b)
        {
            return a;
        }
    }
}