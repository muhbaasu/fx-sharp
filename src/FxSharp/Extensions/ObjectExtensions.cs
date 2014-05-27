namespace FxSharp.Extensions
{
    public static class ObjectExtensions
    {
        public static T Identity<T>(this T t)
        {
            return Prelude.Id(t);
        }

        public static T Constant<T>(this T a, T b)
        {
            return Prelude.Const(a, b);
        }

        /// <summary>
        ///     Cast safely to the given type.
        /// </summary>
        /// <typeparam name="T">Type to cast to.</typeparam>
        /// <param name="value">Object to be casted.</param>
        /// <returns>The wrapped value.</returns>
        public static Maybe<T> As<T>(this object value)
            where T : class
        {
            return (value as T).ToMaybe();
        }
    }
}