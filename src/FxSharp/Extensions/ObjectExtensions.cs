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
    }
}