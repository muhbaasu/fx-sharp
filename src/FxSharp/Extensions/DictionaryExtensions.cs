using System.Collections.Generic;
using FxSharp.Utils;
using JetBrains.Annotations;

namespace FxSharp.Extensions
{
    /// <summary>
    ///     Extension methods for dictionaries.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        ///     Get a value from a dictionary.
        ///     Unlike the normal dictionary method, it returns Maybe.Nothing if the key is not
        ///     present and wraps the value in Maybe.Just if a value for this key is present.
        /// </summary>
        /// <typeparam name="TKey">Dictionary key type.</typeparam>
        /// <typeparam name="TValue">Dictionary value type.</typeparam>
        /// <param name="dict">Dictionary to access.</param>
        /// <param name="key">Key to look up.</param>
        /// <returns>
        ///     Maybe.Just(T) when key is present, Maybe.Nothing(T) when key is not present.
        /// </returns>
        public static Maybe<TValue> GetMaybe<TKey, TValue>(
            [NotNull] this IDictionary<TKey, TValue> dict,
            [NotNull] TKey key)
        {
            Ensure.NotNull(dict, "dict");
            Ensure.NotNull(key, "key");

            return dict.ContainsKey(key) ? Maybe.Just(dict[key]) : Maybe.Nothing<TValue>();
        }
    }
}