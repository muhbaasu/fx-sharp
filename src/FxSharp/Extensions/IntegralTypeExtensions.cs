using System;

namespace FxSharp.Extensions
{
    public static class IntegralTypeExtensions
    {
        /// <summary>
        ///     Parse an int to an enum value. Unlike Enum.TryParse it returns a result wrapped in Maybe.
        /// </summary>
        /// <typeparam name="T">The enumeration type to which to convert the value.</typeparam>
        /// <param name="value">The int representation of the enumeration or underlying value.</param>
        /// <returns>The wrapped value.</returns>
        public static Maybe<T> ParseEnum<T>(this int value)
            where T : struct
        {
            return Enum.IsDefined(typeof (T), value)
                ? Maybe.Just((T) (ValueType) value)
                : Maybe.Nothing<T>();
        }

        /// <summary>
        ///     Parse an uint to an enum value. Unlike Enum.TryParse it returns a result wrapped in Maybe.
        /// </summary>
        /// <typeparam name="T">The enumeration type to which to convert the value.</typeparam>
        /// <param name="value">The unit representation of the enumeration or underlying value.</param>
        /// <returns>The wrapped value.</returns>
        public static Maybe<T> ParseEnum<T>(this uint value)
            where T : struct
        {
            return Enum.IsDefined(typeof (T), value)
                ? Maybe.Just((T) (ValueType) value)
                : Maybe.Nothing<T>();
        }

        /// <summary>
        ///     Parse a long to an enum value. Unlike Enum.TryParse it returns a result wrapped in Maybe.
        /// </summary>
        /// <typeparam name="T">The enumeration type to which to convert the value.</typeparam>
        /// <param name="value">The lopng representation of the enumeration or underlying value.</param>
        /// <returns>The wrapped value.</returns>
        public static Maybe<T> ParseEnum<T>(this long value)
            where T : struct
        {
            return Enum.IsDefined(typeof (T), value)
                ? Maybe.Just((T) (ValueType) value)
                : Maybe.Nothing<T>();
        }

        /// <summary>
        ///     Parse a ulong to an enum value. Unlike Enum.TryParse it returns a result wrapped in Maybe.
        /// </summary>
        /// <typeparam name="T">The enumeration type to which to convert the value.</typeparam>
        /// <param name="value">The ulong representation of the enumeration or underlying value.</param>
        /// <returns>The wrapped value.</returns>
        public static Maybe<T> ParseEnum<T>(this ulong value)
            where T : struct
        {
            return Enum.IsDefined(typeof (T), value)
                ? Maybe.Just((T) (ValueType) value)
                : Maybe.Nothing<T>();
        }

        /// <summary>
        ///     Parse a short to an enum value. Unlike Enum.TryParse it returns a result wrapped in Maybe.
        /// </summary>
        /// <typeparam name="T">The enumeration type to which to convert the value.</typeparam>
        /// <param name="value">The short representation of the enumeration or underlying value.</param>
        /// <returns>The wrapped value.</returns>
        public static Maybe<T> ParseEnum<T>(this short value)
            where T : struct
        {
            return Enum.IsDefined(typeof (T), value)
                ? Maybe.Just((T) (ValueType) value)
                : Maybe.Nothing<T>();
        }

        /// <summary>
        ///     Parse a ushort to an enum value. Unlike Enum.TryParse it returns a result wrapped in Maybe.
        /// </summary>
        /// <typeparam name="T">The enumeration type to which to convert the value.</typeparam>
        /// <param name="value">The ushort representation of the enumeration or underlying value.</param>
        /// <returns>The wrapped value.</returns>
        public static Maybe<T> ParseEnum<T>(this ushort value)
            where T : struct
        {
            return Enum.IsDefined(typeof (T), value)
                ? Maybe.Just((T) (ValueType) value)
                : Maybe.Nothing<T>();
        }

        /// <summary>
        ///     Parse a byte to an enum value. Unlike Enum.TryParse it returns a result wrapped in Maybe.
        /// </summary>
        /// <typeparam name="T">The enumeration type to which to convert the value.</typeparam>
        /// <param name="value">The byte representation of the enumeration or underlying value.</param>
        /// <returns>The wrapped value.</returns>
        public static Maybe<T> ParseEnum<T>(this byte value)
            where T : struct
        {
            return Enum.IsDefined(typeof (T), value)
                ? Maybe.Just((T) (ValueType) value)
                : Maybe.Nothing<T>();
        }

        /// <summary>
        ///     Parse a sbyte to an enum value. Unlike Enum.TryParse it returns a result wrapped in Maybe.
        /// </summary>
        /// <typeparam name="T">The enumeration type to which to convert the value.</typeparam>
        /// <param name="value">The sbyte representation of the enumeration or underlying value.</param>
        /// <returns>The wrapped value.</returns>
        public static Maybe<T> ParseEnum<T>(this sbyte value)
            where T : struct
        {
            return Enum.IsDefined(typeof (T), value)
                ? Maybe.Just((T) (ValueType) value)
                : Maybe.Nothing<T>();
        }
    }
}