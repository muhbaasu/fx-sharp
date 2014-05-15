using System.Collections.Generic;

namespace FxSharp.Extensions
{
    /// <summary>
    ///     Extension methods for dictionaries.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        ///     Get a value from a dictionary.
        ///     Unlike the normal dictionary method, it returns Option.None if the key is not
        ///     present and wraps the value in Option.Some if a value for this key is present.
        /// </summary>
        /// <typeparam name="TKey">Dictionary key type.</typeparam>
        /// <typeparam name="TValue">Dictionary value type.</typeparam>
        /// <param name="dict">Dictionary to access.</param>
        /// <param name="key">Key to look up.</param>
        /// <returns>
        ///     Option.Some(T) when key is present, Option.None(T) when key is not present.
        /// </returns>
        public static Option<TValue> GetOption<TKey, TValue>
            (this IDictionary<TKey, TValue> dict, TKey key)
        {
            return dict.ContainsKey(key) ? Option.Some(dict[key]) : Option.None<TValue>();
        }
    }
}