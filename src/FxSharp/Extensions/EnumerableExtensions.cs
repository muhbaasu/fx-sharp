using System;
using System.Collections.Generic;
using System.Linq;

namespace FxSharp.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        ///     Similiar to FirstOrDefault but returns an Option.
        /// </summary>
        /// <typeparam name="TSource">The type of the enumerable.</typeparam>
        /// <param name="source">The enumerable.</param>
        /// <param name="predicate">The predicate to filter with.</param>
        /// <returns>Option.None(T) if no value matches the predicate.</returns>
        public static Option<TSource> FirstOrNone<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            if (source == null || predicate == null)
            {
                return Option.None<TSource>();
            }

            var enumerable = source as TSource[] ?? source.ToArray();

            return !enumerable.Any(predicate)
                ? Option.None<TSource>()
                : Option.Some(enumerable.FirstOrDefault(predicate));
        }

        /// <summary>
        ///     Similiar to FirstOrDefault but returns an Option.
        /// </summary>
        /// <typeparam name="TSource">The type of the enumerable.</typeparam>
        /// <param name="source">The enumerable.</param>
        /// <returns>Option.None(T) if no value matches the predicate.</returns>
        public static Option<TSource> FirstOrNone<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                return Option.None<TSource>();
            }

            var enumerable = source as TSource[] ?? source.ToArray();

            return enumerable.Any()
                ? Option.None<TSource>()
                : Option.Some(enumerable.FirstOrDefault());
        }
    }
}