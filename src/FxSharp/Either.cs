using System;

namespace FxSharp
{
    public static class Either
    {
        internal static string DefaultCtorError = "Don't use the default constructor!";

        /// <summary>
        ///     Construct an Either instance from a success value.
        /// </summary>
        /// <typeparam name="TSuccess">The type of the success value.</typeparam>
        /// <typeparam name="TFailure">The type of the error value.</typeparam>
        /// <param name="value">The value to wrap.</param>
        /// <returns>The value wrapped in an Either instance.</returns>
        public static Either<TSuccess, TFailure> Success<TSuccess, TFailure>(TSuccess value)
        {
            return new Either<TSuccess, TFailure>(value);
        }

        /// <summary>
        ///     Construct an Either instance from an error value.
        /// </summary>
        /// <typeparam name="TSuccess">The type of the success value.</typeparam>
        /// <typeparam name="TFailure">The type of the error value.</typeparam>
        /// <param name="error">The error to wrap.</param>
        /// <returns>The error wrapped in an Either instance.</returns>
        public static Either<TSuccess, TFailure> Failure<TSuccess, TFailure>(TFailure error)
        {
            return new Either<TSuccess, TFailure>(error);
        }
    }

    /// <summary>
    ///     An Either wraps the result of a computation that can fail or the resulting error value.
    ///     The subtype 'Success' represents a successful result wrapping the value whereas the
    ///     'Failure' subtype represents a failed computation with the resulting error value.
    /// </summary>
    /// <typeparam name="TSuccess">Type of the successfully computed value.</typeparam>
    /// <typeparam name="TFailure">Type of the error value.</typeparam>
    public struct Either<TSuccess, TFailure>
    {
        /// <summary>
        ///     The possibly present error.
        /// </summary>
        private readonly TFailure _error;

        /// <summary>
        ///     The internal state. NoValue represents an invalid state.
        /// </summary>
        private readonly EitherState _state;

        /// <summary>
        ///     The possibly present value.
        /// </summary>
        private readonly TSuccess _value;

        /// <summary>
        ///     Success ctor.
        /// </summary>
        /// <param name="value">The success value.</param>
        internal Either(TSuccess value)
        {
            _state = EitherState.IsSuccess;
            _value = value;
            _error = default(TFailure);
        }

        /// <summary>
        ///     Failure ctor.
        /// </summary>
        /// <param name="error">The error value.</param>
        internal Either(TFailure error)
        {
            _state = EitherState.IsFailure;
            _value = default(TSuccess);
            _error = error;
        }

        /// <summary>
        ///     Get the stored value or the given default instead.
        /// </summary>
        /// <param name="other">The default value to return in case no value is stored.</param>
        /// <returns>Stored or given default value.</returns>
        public TSuccess GetOrElse(TSuccess other)
        {
            switch (_state)
            {
                case EitherState.IsSuccess:
                    return _value;
                case EitherState.IsFailure:
                    return other;
                default:
                    throw new InvalidOperationException(Either.DefaultCtorError);
            }
        }

        /// <summary>
        ///     Apply the given function to the possibly present value and wrap it again.
        ///     Return the wrapped error value otherwise.
        /// </summary>
        /// <typeparam name="TResult">The result type of fn.</typeparam>
        /// <param name="fn">The function to apply to the value.</param>
        /// <returns>A new Either instance.</returns>
        public Either<TResult, TFailure> Select<TResult>(Func<TSuccess, TResult> fn)
        {
            switch (_state)
            {
                case EitherState.IsSuccess:
                    return new Either<TResult, TFailure>(fn(_value));
                case EitherState.IsFailure:
                    return new Either<TResult, TFailure>(_error);
                default:
                    throw new InvalidOperationException(Either.DefaultCtorError);
            }
        }

        /// <summary>
        ///     Apply the given function to the possibly present value and throw away the result.
        ///     Otherwise, don't do anything.
        /// </summary>
        /// <param name="fn">The function to apply to the value.</param>
        public void Select_(Action<TSuccess> fn)
        {
            switch (_state)
            {
                case EitherState.IsSuccess:
                    fn(_value);
                    break;
                case EitherState.IsFailure:
                    break;
                default:
                    throw new InvalidOperationException(Either.DefaultCtorError);
            }
        }

        /// <summary>
        ///     Apply the function success to the value if present. Otherwise, apply the function
        ///     failure to the error value.
        /// </summary>
        /// <typeparam name="TResult">The result of success and failure.</typeparam>
        /// <param name="failure">The function to apply to error.</param>
        /// <param name="success">The function to apply to value.</param>
        /// <returns>The value returned by error failure or success.</returns>
        public TResult Match<TResult>(
            Func<TFailure, TResult> failure,
            Func<TSuccess, TResult> success)
        {
            switch (_state)
            {
                case EitherState.IsSuccess:
                    return success(_value);
                case EitherState.IsFailure:
                    return failure(_error);
                default:
                    throw new InvalidOperationException(Either.DefaultCtorError);
            }
        }

        /// <summary>
        ///     Apply the function success to the value if present. Otherwise, apply the function
        ///     failure to the error value. Ignore the results of both.
        /// </summary>
        /// <param name="failure">The function to apply to error.</param>
        /// <param name="success">The function to apply to value.</param>
        public void Match_(Action<TFailure> failure, Action<TSuccess> success)
        {
            switch (_state)
            {
                case EitherState.IsSuccess:
                    success(_value);
                    break;
                case EitherState.IsFailure:
                    failure(_error);
                    break;
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