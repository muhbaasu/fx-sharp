using System;

namespace FxSharp
{
    /// <summary>
    ///     Static helper methods for Options.
    /// </summary>
    public static class Option
    {
        /// <summary>
        ///     Wrap a reference type in an Option instance. The appropriate instance is selected
        ///     by distinguishing between a null value (for None) and any other value (for Some).
        /// </summary>
        /// <typeparam name="T">The wrapped type.</typeparam>
        /// <param name="reference">The wrapped object.</param>
        /// <returns>An Option instance wrapping the given type.</returns>
        public static Option<T> SomeOrNone<T>(this T reference) where T : class
        {
            return reference != null ? new Option<T>(reference) : new Option<T>();
        }

        /// <summary>
        ///     Create a Some Option instance by wrapping an arbitratry value.
        /// </summary>
        /// <typeparam name="T">The wrapped type.</typeparam>
        /// <param name="val">The wrapped object.</param>
        /// <returns>An Option instance wrapping the given type.</returns>
        public static Option<T> Some<T>(T val)
        {
            return new Option<T>(val);
        }

        /// <summary>
        ///     Create a None Option instance.
        /// </summary>
        /// <typeparam name="T">The type of the absent value to represent.</typeparam>
        /// <returns>An Option instance wrapping the given type.</returns>
        public static Option<T> None<T>()
        {
            return new Option<T>();
        }
    }

    /// <summary>
    ///     A type to describe a possibly absent value.
    /// </summary>
    /// <typeparam name="T">The type of the wrapped value.</typeparam>
    public struct Option<T>
    {
        /// <summary>
        ///     Do we actually have a value? This is used instead of null to allow wrapping
        ///     value types as well.
        /// </summary>
        private readonly bool _hasValue;

        /// <summary>
        ///     The possibly present value.
        /// </summary>
        private readonly T _value;

        /// <summary>
        ///     Ctor for Some. The None case is covered by the default ctor.
        /// </summary>
        /// <param name="value">The value to wrap.</param>
        internal Option(T value)
        {
            _value = value;
            _hasValue = true;
        }

        /// <summary>
        ///     Get the stored value or the given default instead.
        /// </summary>
        /// <param name="other">The default value to return in case no value is stored.</param>
        /// <returns>Stored or given default value.</returns>
        public T GetOrElse(T other)
        {
            return _hasValue ? _value : other;
        }

        /// <summary>
        ///     Map the function fn over the wrapped value if present. Wrap the result in as well.
        /// </summary>
        /// <typeparam name="TResult">The type of the result of fn.</typeparam>
        /// <param name="fn">A function to apply the the wrapped value.</param>
        /// <returns>The wrapped mapped value.</returns>
        public Option<TResult> Select<TResult>(Func<T, TResult> fn)
        {
            return _hasValue ? new Option<TResult>(fn(_value)) : new Option<TResult>();
        }

        /// <summary>
        ///     Map the function fn over the wrapped value if present; discard the result.
        /// </summary>
        /// <param name="fn">A function to apply to the wrapped value.</param>
        public void Select_(Action<T> fn)
        {
            if (_hasValue)
            {
                fn(_value);
            }
        }

        /// <summary>
        ///     Map the function fn over the wrapped value if present.
        /// </summary>
        /// <typeparam name="TResult">The type of the result of fn.</typeparam>
        /// <param name="fn">
        ///     A function to apply the the wrapped value which wraps the result in an Option.
        /// </param>
        /// <returns>The result of fn.</returns>
        public Option<TResult> SelectMany<TResult>(Func<T, Option<TResult>> fn)
        {
            return _hasValue ? fn(_value) : new Option<TResult>();
        }

        /// <summary>
        ///     Call the given function, if no value is present.
        /// </summary>
        /// <param name="fn">A function to call if no value is present.</param>
        /// <returns>This instance.</returns>
        public Option<T> Otherwise(Action fn)
        {
            if (!_hasValue)
            {
                fn();
            }

            return this;
        }

        /// <summary>
        ///     The complement to Select_. Call the function fn if no value is present.
        /// </summary>
        /// <param name="fn">The function to call.</param>
        public void Otherwise_(Action fn)
        {
            if (!_hasValue)
            {
                fn();
            }
        }

        /// <summary>
        ///     Map either the function some over the present value or call none.
        /// </summary>
        /// <typeparam name="TResult">The result type of none and some.</typeparam>
        /// <param name="none">The function to call if no value is present.</param>
        /// <param name="some">The function to call with a present value.</param>
        /// <returns>The result of the either none or some functions.</returns>
        public TResult Match<TResult>(Func<TResult> none, Func<T, TResult> some)
        {
            return _hasValue ? some(_value) : none();
        }

        /// <summary>
        ///     Map either the function some over the present value or call none. Ignore the result.
        /// </summary>
        /// <param name="none">The function to call if no value is present.</param>
        /// <param name="some">The function to call with a present value.</param>
        public void Match_(Action none, Action<T> some)
        {
            if (_hasValue)
            {
                some(_value);
            }
            else
            {
                none();
            }
        }
    }
}