using System.Collections.Generic;
using System.Linq;
using FxSharp.Utils;
using JetBrains.Annotations;

namespace FxSharp.Extensions
{
    public static class StackExtensions
    {
        /// <summary>
        ///     Remove and return the object at the top of the stack.
        /// </summary>
        /// <typeparam name="TSource">The type of the stack.</typeparam>
        /// <param name="source">The stack.</param>
        /// <returns>Maybe.Nothing(T) if stack is empty.</returns>
        public static Maybe<TSource> PopOrNothing<TSource>([NotNull] this Stack<TSource> source)
        {
            Ensure.NotNull(source, "source");

            return source.Any() ? Maybe.Just(source.Pop()) : Maybe.Nothing<TSource>();
        }
    }
}