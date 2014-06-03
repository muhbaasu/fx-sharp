using System;
using JetBrains.Annotations;

namespace FxSharp
{
    public static class Either
    {
        internal const string DefaultCtorError = "Don't use the default constructor!";

        /// <summary>
        ///     Construct an Either instance from a success val.
        /// </summary>
        /// <typeparam name="TRight">The type of the success val.</typeparam>
        /// <typeparam name="TLeft">The type of the error val.</typeparam>
        /// <param name="value">The val to wrap.</param>
        /// <returns>The val wrapped in an Either instance.</returns>
        public static Either<TLeft, TRight> Right<TLeft, TRight>(TRight value)
        {
            return new Either<TLeft, TRight>(value);
        }

        /// <summary>
        ///     Construct an Either instance from an error val.
        /// </summary>
        /// <typeparam name="TRight">The type of the success val.</typeparam>
        /// <typeparam name="TLeft">The type of the error val.</typeparam>
        /// <param name="error">The error to wrap.</param>
        /// <returns>The error wrapped in an Either instance.</returns>
        public static Either<TLeft, TRight> Left<TLeft, TRight>(TLeft error)
        {
            return new Either<TLeft, TRight>(error);
        }
    }

    /// <summary>
    ///     An Either wraps the result of a computation that can fail or the resulting error val.
    ///     The subtype 'Right' represents a successful result wrapping the val whereas the
    ///     'Left' subtype represents a failed computation with the resulting error val.
    /// </summary>
    /// <typeparam name="TLeft">Type of the error val.</typeparam>
    /// <typeparam name="TRight">Type of the successfully computed val.</typeparam>
    public struct Either<TLeft, TRight>
    {
        /// <summary>
        ///     The possibly present error.
        /// </summary>
        private readonly TLeft _error;

        /// <summary>
        ///     The internal state. NoValue represents an invalid state.
        /// </summary>
        private readonly EitherState _state;

        /// <summary>
        ///     The possibly present val.
        /// </summary>
        private readonly TRight _val;

        /// <summary>
        ///     Right ctor.
        /// </summary>
        /// <param name="val">The success val.</param>
        internal Either(TRight val)
        {
            _state = EitherState.IsRight;
            _val = val;
            _error = default(TLeft);
        }

        /// <summary>
        ///     Left ctor.
        /// </summary>
        /// <param name="error">The error val.</param>
        internal Either(TLeft error)
        {
            _state = EitherState.IsLeft;
            _val = default(TRight);
            _error = error;
        }

        /// <summary>
        ///     Get the stored val or the given default instead.
        /// </summary>
        /// <param name="other">The default val to return in case no val is stored.</param>
        /// <returns>Stored or given default val.</returns>
        [Pure]
        public TRight GetOrElse(TRight other)
        {
            switch (_state)
            {
                case EitherState.IsRight:
                    return _val;
                case EitherState.IsLeft:
                    return other;
                default:
                    throw new InvalidOperationException(Either.DefaultCtorError);
            }
        }

        /// <summary>
        ///     Apply the given function to the possibly present val and wrap it again.
        ///     Return the wrapped error val otherwise.
        /// </summary>
        /// <typeparam name="TResult">The result type of fn.</typeparam>
        /// <param name="fn">The function to apply to the val.</param>
        /// <returns>A new Either instance.</returns>
        [Pure]
        public Either<TLeft, TResult> Select<TResult>([NotNull] Func<TRight, TResult> fn)
        {
            switch (_state)
            {
                case EitherState.IsRight:
                    return new Either<TLeft, TResult>(fn(_val));
                case EitherState.IsLeft:
                    return new Either<TLeft, TResult>(_error);
                default:
                    throw new InvalidOperationException(Either.DefaultCtorError);
            }
        }

        /// <summary>
        ///     Apply the given function to the possibly present val and throw away the result.
        ///     Otherwise, don't do anything.
        /// </summary>
        /// <param name="fn">The function to apply to the val.</param>
        /// <returns>This.</returns>
        [Pure]
        public Either<TLeft, TRight> Select_([NotNull] Action<TRight> fn)
        {
            switch (_state)
            {
                case EitherState.IsRight:
                    fn(_val);
                    return this;
                case EitherState.IsLeft:
                    return this;
                default:
                    throw new InvalidOperationException(Either.DefaultCtorError);
            }
        }

        [Pure]
        public Either<TLeft, TResSuccess> SelectMany<TResSuccess>(
            [NotNull] Func<TRight, Either<TLeft, TResSuccess>> fn)
        {
            switch (_state)
            {
                case EitherState.IsRight:
                    return fn(_val);
                case EitherState.IsLeft:
                    return new Either<TLeft, TResSuccess>(_error);
                default:
                    throw new InvalidOperationException(Either.DefaultCtorError);
            }
        }

        [Pure]
        public Either<TLeft, TResSuccess> SelectMany<TInner, TResSuccess>(
            [NotNull] Func<TRight, Either<TLeft, TInner>> firstFn,
            [NotNull] Func<TRight, TInner, TResSuccess> secondFn)
        {
            return SelectMany(x => firstFn(x).SelectMany(y =>
                new Either<TLeft, TResSuccess>(secondFn(x, y))));
        }

        /// <summary>
        ///     Apply the function success to the val if present. Otherwise, apply the function
        ///     failure to the error val.
        /// </summary>
        /// <typeparam name="TResult">The result of success and failure.</typeparam>
        /// <param name="right">The function to apply to error.</param>
        /// <param name="left">The function to apply to val.</param>
        /// <returns>The val returned by error failure or success.</returns>
        [Pure]
        public TResult Match<TResult>(
            [NotNull] Func<TRight, TResult> right,
            [NotNull] Func<TLeft, TResult> left)
        {
            switch (_state)
            {
                case EitherState.IsRight:
                    return right(_val);
                case EitherState.IsLeft:
                    return left(_error);
                default:
                    throw new InvalidOperationException(Either.DefaultCtorError);
            }
        }

        /// <summary>
        ///     Apply the function success to the val if present. Otherwise, apply the function
        ///     failure to the error val. Ignore the results of both.
        /// </summary>
        /// <param name="left">The function to apply to error.</param>
        /// <param name="right">The function to apply to val.</param>
        /// <returns>This.</returns>
        [Pure]
        public Either<TLeft, TRight> Match_(
            [NotNull] Action<TRight> right,
            [NotNull] Action<TLeft> left)
        {
            switch (_state)
            {
                case EitherState.IsRight:
                    right(_val);
                    return this;
                case EitherState.IsLeft:
                    left(_error);
                    return this;
                default:
                    throw new InvalidOperationException(Either.DefaultCtorError);
            }
        }

        [Pure]
        public override string ToString()
        {
            switch (_state)
            {
                case EitherState.IsRight:
                    return string.Format("Right {0}", _val);
                case EitherState.IsLeft:
                    return string.Format("Left {0}", _error);
                default:
                    return "Invalid Either";
            }
        }

        /// <summary>
        /// Convert either to Maybe.
        /// </summary>
        /// <returns>Maybe.Nothing(T) when Left.</returns>
        [Pure]
        public Maybe<TRight> ToMaybe()
        {
            return Match(
                left: _ => Maybe.Nothing<TRight>(),
                right: Maybe.Just);
        }

        /// <summary>
        ///     The possible internal states.
        /// </summary>
        private enum EitherState
        {
            // Ugluk: "Get back, scum! The prisoners go to Saruman. Alive and unspoiled."
            // Grishnakh: "Alive? Why alive? Do they give good sport?"
            // Ugluk: "They have something. An Elvish weapon. The master wants it for the war."
            // Ugluk: "We need this unused member as well. The master needs it for correct execution."
            // (Ok, I made up the last line. We nevertheless need it for the switch-case-statements
            // to fall into the default case as well as to detect the use of the default 
            // constructor which makes no sense in this context).
            // ReSharper disable once UnusedMember.Local
            IsUninitialized,
            IsRight,
            IsLeft
        }
    }
}