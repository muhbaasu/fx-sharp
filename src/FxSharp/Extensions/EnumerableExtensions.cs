﻿using System;
using System.Collections.Generic;
using System.Linq;
using FxSharp.Utils;
using JetBrains.Annotations;

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
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, bool> predicate)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            Ensure.NotNull(source, "source");
            Ensure.NotNull(predicate, "predicate");

            // ReSharper disable once PossibleMultipleEnumeration
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
            [NotNull] this IEnumerable<TSource> source)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            Ensure.NotNull(source, "source");

            // ReSharper disable once PossibleMultipleEnumeration
            var enumerable = source as TSource[] ?? source.ToArray();

            return enumerable.Any()
                ? Maybe.Just(enumerable.FirstOrDefault())
                : Maybe.Nothing<TSource>();
        }


        /// <summary>
        ///     Returns last element of sequence wrapped in Maybe.
        /// </summary>
        /// <typeparam name="TSource">The type of the enumerable.</typeparam>
        /// <param name="source">The enumerable.</param>
        /// <returns>Maybe.Nothing(T) when sequence is empty.</returns>
        public static Maybe<TSource> LastOrNothing<TSource>(
            [NotNull] this IEnumerable<TSource> source)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            Ensure.NotNull(source, "source");

            // ReSharper disable once PossibleMultipleEnumeration
            var enumerable = source as TSource[] ?? source.ToArray();

            return enumerable.Any()
                ? Maybe.Just(enumerable.LastOrDefault())
                : Maybe.Nothing<TSource>();
        }

        /// <summary>
        ///     Returns last element of sequence which matches the predicate,
        ///     wrapped in Maybe.
        /// </summary>
        /// <typeparam name="TSource">The type of the enumerable.</typeparam>
        /// <param name="source">Maybe.Nothing(T) when sequence is empty.</param>
        /// <param name="predicate">The predicate to filter with.</param>
        /// <returns>
        ///     Maybe.Nothing(T) when no element matches the sequence
        ///     or the sequence is empty.
        /// </returns>
        public static Maybe<TSource> LastOrNothing<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, bool> predicate)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            Ensure.NotNull(source, "source");
            Ensure.NotNull(predicate, "predicate");

            // ReSharper disable once PossibleMultipleEnumeration
            var enumerable = source as TSource[] ?? source.ToArray();

            return !enumerable.Any(predicate)
                ? Maybe.Nothing<TSource>()
                : Maybe.Just(enumerable.LastOrDefault(predicate));
        }

        /// <summary>
        ///     Returns the only element of a sequence, which matches a predicate.
        ///     Returns Option.None(T) when source contains more than one element or
        ///     when the source is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the enumerable.</typeparam>
        /// <param name="source">
        ///     Option.Some(T) if the source contains exactly one element,
        ///     which matches the predicate.
        ///     Option.None(T) when source contains more than one element
        ///     or when the source is empty.
        /// </param>
        /// <param name="predicate">The predicate to filter with.</param>
        public static Maybe<TSource> SingleOrNothing<TSource>(
            [NotNull] this IEnumerable<TSource> source,
            [NotNull] Func<TSource, bool> predicate)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            Ensure.NotNull(source, "source");
            Ensure.NotNull(predicate, "predicate");

            // ReSharper disable once PossibleMultipleEnumeration
            var filtered = source.Where(predicate);

            using (var enumerator = filtered.GetEnumerator())
            {
                if (!enumerator.MoveNext()) return Maybe.Nothing<TSource>();
                var result = enumerator.Current;
                if (!enumerator.MoveNext()) return Maybe.Just(result);
            }

            return Maybe.Nothing<TSource>();
        }

        /// <summary>
        ///     Returns the only element of a sequence.
        ///     Returns Option.None(T) when source contains more than one element or
        ///     when the source is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of the enumerable.</typeparam>
        /// <param name="source">
        ///     Option.Some(T) if the source contains exactly one element.
        ///     Option.None(T) when source contains more than one element
        ///     or when the source is empty.
        /// </param>
        public static Maybe<TSource> SingleOrNothing<TSource>(
            [NotNull] this IEnumerable<TSource> source)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            Ensure.NotNull(source, "source");

            // ReSharper disable once PossibleMultipleEnumeration
            using (var enumerator = source.GetEnumerator())
            {
                if (!enumerator.MoveNext()) return Maybe.Nothing<TSource>();
                var result = enumerator.Current;
                if (!enumerator.MoveNext()) return Maybe.Just(result);
            }
            return Maybe.Nothing<TSource>();
        }
    }
}