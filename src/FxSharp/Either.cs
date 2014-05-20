using System;

namespace FxSharp
{
    public static class Either
    {
        internal static string DefaultCtorError = "Don't use the default constructor!";

        /// <summary>
        ///     Construct an Either instance from a success val.
        /// </summary>
        /// <typeparam name="TRight">The type of the success val.</typeparam>
        /// <typeparam name="TLeft">The type of the error val.</typeparam>
        /// <param name="value">The val to wrap.</param>
        /// <returns>The val wrapped in an Either instance.</returns>
        public static Either<TRight, TLeft> Right<TRight, TLeft>(TRight value)
        {
            return new Either<TRight, TLeft>(value);
        }

        /// <summary>
        ///     Construct an Either instance from an error val.
        /// </summary>
        /// <typeparam name="TRight">The type of the success val.</typeparam>
        /// <typeparam name="TLeft">The type of the error val.</typeparam>
        /// <param name="error">The error to wrap.</param>
        /// <returns>The error wrapped in an Either instance.</returns>
        public static Either<TRight, TLeft> Left<TRight, TLeft>(TLeft error)
        {
            return new Either<TRight, TLeft>(error);
        }
    }

    /// <summary>
    ///     An Either wraps the result of a computation that can fail or the resulting error val.
    ///     The subtype 'Right' represents a successful result wrapping the val whereas the
    ///     'Left' subtype represents a failed computation with the resulting error val.
    /// </summary>
    /// <typeparam name="TRight">Type of the successfully computed val.</typeparam>
    /// <typeparam name="TLeft">Type of the error val.</typeparam>
    public struct Either<TRight, TLeft>
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
            _state = EitherState.IsSuccess;
            _val = val;
            _error = default(TLeft);
        }

        /// <summary>
        ///     Left ctor.
        /// </summary>
        /// <param name="error">The error val.</param>
        internal Either(TLeft error)
        {
            _state = EitherState.IsFailure;
            _val = default(TRight);
            _error = error;
        }

        /// <summary>
        ///     Get the stored val or the given default instead.
        /// </summary>
        /// <param name="other">The default val to return in case no val is stored.</param>
        /// <returns>Stored or given default val.</returns>
        public TRight GetOrElse(TRight other)
        {
            switch (_state)
            {
                case EitherState.IsSuccess:
                    return _val;
                case EitherState.IsFailure:
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
        public Either<TResult, TLeft> Select<TResult>(Func<TRight, TResult> fn)
        {
            switch (_state)
            {
                case EitherState.IsSuccess:
                    return new Either<TResult, TLeft>(fn(_val));
                case EitherState.IsFailure:
                    return new Either<TResult, TLeft>(_error);
                default:
                    throw new InvalidOperationException(Either.DefaultCtorError);
            }
        }

        /// <summary>
        ///     Apply the given function to the possibly present val and throw away the result.
        ///     Otherwise, don't do anything.
        /// </summary>
        /// <param name="fn">The function to apply to the val.</param>
        public void Select_(Action<TRight> fn)
        {
            switch (_state)
            {
                case EitherState.IsSuccess:
                    fn(_val);
                    break;
                case EitherState.IsFailure:
                    break;
                default:
                    throw new InvalidOperationException(Either.DefaultCtorError);
            }
        }

        public Either<TResSuccess, TLeft> SelectMany<TResSuccess>(
            Func<TRight, Either<TResSuccess, TLeft>> fn)
        {
            switch (_state)
            {
                case EitherState.IsSuccess:
                    return fn(_val);
                case EitherState.IsFailure:
                    return new Either<TResSuccess, TLeft>(_error);
                default:
                    throw new InvalidOperationException(Either.DefaultCtorError);
            }
        }

        public Either<TResSuccess, TLeft> SelectMany<TInner, TResSuccess>(
            Func<TRight, Either<TInner, TLeft>> firstFn,
            Func<TRight, TInner, TResSuccess> secondFn)
        {
            return SelectMany(x => firstFn(x).SelectMany(y =>
                new Either<TResSuccess, TLeft>(secondFn(x, y))));
        }

        /// <summary>
        ///     Apply the function success to the val if present. Otherwise, apply the function
        ///     failure to the error val.
        /// </summary>
        /// <typeparam name="TResult">The result of success and failure.</typeparam>
        /// <param name="failure">The function to apply to error.</param>
        /// <param name="success">The function to apply to val.</param>
        /// <returns>The val returned by error failure or success.</returns>
        public TResult Match<TResult>(Func<TLeft, TResult> failure, Func<TRight, TResult> success)
        {
            switch (_state)
            {
                case EitherState.IsSuccess:
                    return success(_val);
                case EitherState.IsFailure:
                    return failure(_error);
                default:
                    throw new InvalidOperationException(Either.DefaultCtorError);
            }
        }

        /// <summary>
        ///     Apply the function success to the val if present. Otherwise, apply the function
        ///     failure to the error val. Ignore the results of both.
        /// </summary>
        /// <param name="failure">The function to apply to error.</param>
        /// <param name="success">The function to apply to val.</param>
        public void Match_(Action<TLeft> failure, Action<TRight> success)
        {
            switch (_state)
            {
                case EitherState.IsSuccess:
                    success(_val);
                    break;
                case EitherState.IsFailure:
                    failure(_error);
                    break;
                default:
                    throw new InvalidOperationException(Either.DefaultCtorError);
            }
        }

        public override string ToString()
        {
            switch (_state)
            {
                case EitherState.IsSuccess:
                    return string.Format("Right {0}", _val);
                case EitherState.IsFailure:
                    return string.Format("Left {0}", _error);
                default:
                    throw new InvalidOperationException(Either.DefaultCtorError);
            }
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
            IsInvalid,
            IsSuccess,
            IsFailure
        }
    }
}