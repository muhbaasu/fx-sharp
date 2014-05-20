using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using FxSharp.Extensions;

namespace FxSharp
{
    /// <summary>
    ///     Static helper methods for Maybes.
    /// </summary>
    public static class Maybe
    {
        /// <summary>
        ///     Wrap a val type in a Maybe instance. The appropriate instance is selected
        ///     by distinguishing between a default val (for Nothing) and any other val
        ///     (for Just).
        /// </summary>
        /// <typeparam name="T">The wrapped type.</typeparam>
        /// <param name="val">The wrapped object.</param>
        /// <returns>A Maybe instance wrapping the given type.</returns>
        public static Maybe<T> ToMaybe<T>(this T val)
        {
            return !val.Equals(default(T))
                ? Just(val)
                : Nothing<T>();
        }

        /// <summary>
        ///     Convert a Maybe into an enumerable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="maybe"></param>
        /// <returns></returns>
        public static IEnumerable<T> ToEnumerable<T>(this Maybe<T> maybe)
        {
            return maybe.Match(
                just: t => t.ToEnumerable(),
                nothing: Enumerable.Empty<T>);
        }

        /// <summary>
        ///     Create a Just Maybe instance by wrapping an arbitratry val.
        /// </summary>
        /// <typeparam name="T">The wrapped type.</typeparam>
        /// <param name="val">The wrapped object.</param>
        /// <returns>A Maybe instance wrapping the given type.</returns>
        public static Maybe<T> Just<T>(T val)
        {
            return new Maybe<T>(val);
        }

        /// <summary>
        ///     Create a Nothing Maybe instance.
        /// </summary>
        /// <typeparam name="T">The type of the absent val to represent.</typeparam>
        /// <returns>A Maybe instance wrapping the given type.</returns>
        public static Maybe<T> Nothing<T>()
        {
            return new Maybe<T>();
        }
    }

    /// <summary>
    ///     A type to describe a possibly absent val.
    /// </summary>
    /// <typeparam name="T">The type of the wrapped val.</typeparam>
    public struct Maybe<T>
    {
        /// <summary>
        ///     Do we actually have a val? This is used instead of null to allow wrapping
        ///     val types as well.
        /// </summary>
        private readonly bool _hasValue;

        /// <summary>
        ///     The possibly present val.
        /// </summary>
        private readonly T _val;

        /// <summary>
        ///     Ctor for Just. The Nothing case is covered by the default ctor.
        /// </summary>
        /// <param name="val">The val to wrap.</param>
        internal Maybe(T val)
        {
            _val = val;
            _hasValue = true;
        }

        /// <summary>
        ///     Get the stored val or the given default instead.
        /// </summary>
        /// <param name="other">The default val to return in case no val is stored.</param>
        /// <returns>Stored or given default val.</returns>
        [Pure]
        public T GetOrElse(T other)
        {
            return _hasValue ? _val : other;
        }

        /// <summary>
        ///     Map the function fn over the wrapped val if present. Wrap the result in as well.
        /// </summary>
        /// <typeparam name="TResult">The type of the result of fn.</typeparam>
        /// <param name="fn">A function to apply the the wrapped val.</param>
        /// <returns>The wrapped mapped val.</returns>
        [Pure]
        public Maybe<TResult> Select<TResult>(Func<T, TResult> fn)
        {
            return _hasValue
                ? new Maybe<TResult>(fn(_val))
                : new Maybe<TResult>();
        }

        /// <summary>
        ///     Map the function fn over the wrapped val if present; discard the result.
        /// </summary>
        /// <param name="fn">A function to apply to the wrapped val.</param>
        /// <returns>This.</returns>
        [Pure]
        public Maybe<T> Select_(Action<T> fn)
        {
            if (_hasValue)
            {
                fn(_val);
            }

            return this;
        }

        /// <summary>
        ///     Map the function fn over the wrapped val if present.
        /// </summary>
        /// <typeparam name="TResult">The type of the result of fn.</typeparam>
        /// <param name="fn">
        ///     A function to apply the the wrapped val which wraps the result in a Maybe.
        /// </param>
        /// <returns>The result of fn.</returns>
        [Pure]
        public Maybe<TResult> SelectMany<TResult>(Func<T, Maybe<TResult>> fn)
        {
            return _hasValue ? fn(_val) : new Maybe<TResult>();
        }

        /// <summary>
        ///     Map the first function over the wrapped value and the second function over the
        ///     result. If the wrapped value isn't present or the first or second functions return
        ///     no value, Nothing is returned. This variant of SelectMany enables LINQ's
        ///     syntactic sugar.
        /// </summary>
        /// <typeparam name="TInter">The result tpye of the first function.</typeparam>
        /// <typeparam name="TResult">The result type of the second function.</typeparam>
        /// <param name="firstFn">The first function to apply.</param>
        /// <param name="secondFn">The second function to apply.</param>
        /// <returns>The result of the functions combined.</returns>
        [Pure]
        public Maybe<TResult> SelectMany<TInter, TResult>(
            Func<T, Maybe<TInter>> firstFn,
            Func<T, TInter, TResult> secondFn)
        {
            return SelectMany(
                x => firstFn(x).SelectMany(y => secondFn(x, y).ToMaybe()));
        }

        /// <summary>
        ///     Call the given function, if no val is present.
        /// </summary>
        /// <param name="fn">A function to call if no val is present.</param>
        /// <returns>This instance.</returns>
        [Pure]
        public Maybe<T> Otherwise_(Action fn)
        {
            if (!_hasValue)
            {
                fn();
            }

            return this;
        }

        /// <summary>
        ///     Map either the function just over the present val or call nothing.
        /// </summary>
        /// <typeparam name="TResult">The result type of nothing and just.</typeparam>
        /// <param name="nothing">The function to call if no val is present.</param>
        /// <param name="just">The function to call with a present val.</param>
        /// <returns>The result of the either nothing or just functions.</returns>
        [Pure]
        public TResult Match<TResult>(
            Func<TResult> nothing,
            Func<T, TResult> just)
        {
            return _hasValue ? just(_val) : nothing();
        }

        /// <summary>
        ///     Map either the function just over the present val or call nothing. Ignore the result.
        /// </summary>
        /// <param name="nothing">The function to call if no val is present.</param>
        /// <param name="just">The function to call with a present val.</param>
        [Pure]
        public Maybe<T> Match_(Action nothing, Action<T> just)
        {
            if (_hasValue)
            {
                just(_val);
            } else
            {
                nothing();
            }

            return this;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        [Pure]
        public override string ToString()
        {
            return _hasValue ? string.Format("Just {0}", _val) : "Nothing";
        }
    }
}