using System;
using System.Collections.Generic;
using System.Linq;

namespace FxSharp.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        ///     Convert a single value into an one item-sized enumerable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val"></param>
        /// <returns></returns>
        public static IEnumerable<T> ToEnumerable<T>(this T val)
        {
            yield return val;
        }

        /// <summary>
        ///     Similiar to FirstOrDefault but return a Maybe.
        /// </summary>
        /// <typeparam name="TSource">The type of the enumerable.</typeparam>
        /// <param name="source">The enumerable.</param>
        /// <param name="predicate">The predicate to filter with.</param>
        /// <returns>Maybe.Nothing(T) if no value matches the predicate.</returns>
        public static Maybe<TSource> FirstOrNothing<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            if (source == null || predicate == null)
            {
                throw new ArgumentNullException("Source or predicate null in Maybe.FirstOrNothing.");
            }

            var enumerable = source as TSource[] ?? source.ToArray();

            return !enumerable.Any(predicate)
                ? Maybe.Nothing<TSource>()
                : Maybe.Just(enumerable.FirstOrDefault(predicate));
        }

        /// <summary>
        ///     Similiar to FirstOrDefault but return a Maybe.
        /// </summary>
        /// <typeparam name="TSource">The type of the enumerable.</typeparam>
        /// <param name="source">The enumerable.</param>
        /// <returns>Maybe.Nothing(T) if no value matches the predicate.</returns>
        public static Maybe<TSource> FirstOrNothing<TSource>(
            this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("Source null in Maybe.FirstOrNothing.");
            }

            var enumerable = source as TSource[] ?? source.ToArray();

            return enumerable.Any()
                ? Maybe.Just(enumerable.FirstOrDefault())
                : Maybe.Nothing<TSource>();
        }
    }
}