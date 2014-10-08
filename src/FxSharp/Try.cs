using JetBrains.Annotations;
using System;

namespace FxSharp
{

    public static class Try
    {
        public static Try<T, TException> Failure<T, TException>(TException exception) where TException : Exception
        {
            return new Try<T, TException>(exception);
        }

        public static Try<T, TException> Success<T, TException>(T value) where TException : Exception
        {
            return new Try<T, TException>(value);
        }
    }

    /// <summary>
    /// Represents a computation that may either result in an exception or 
    /// returns a successfully computed value.
    /// </summary>
    /// <typeparam name="T">The wrapped successful computed value type.</typeparam>
    /// <typeparam name="TException">The exception type.</typeparam>
    public struct Try<T, TException> where TException : Exception
    {
        private readonly bool _isSuccess;
        private readonly TException _exception;
        private readonly T _val;

        internal Try(T val)
        {
            _val = val;
            _isSuccess = true;
            _exception = null;
        }

        internal Try(TException exception)
        {
            _val = default(T);
            _isSuccess = false;
            _exception = exception;
        }

        public bool isSuccess()
        {
            return _isSuccess;
        }

        public bool isFailure()
        {
            return !_isSuccess;
        }

        public Try<T, TException> Failed(TException exception)
        {
            return new Try<T, TException>(exception);
        }

        [Pure]
        public Try<TResult, TException> SelectMany<TResult>([NotNull] Func<T, Try<TResult, TException>> fn)
        {
            return _isSuccess ? fn(_val) : new Try<TResult, TException>();
        }

        public Try<T, TException> Select_([NotNull] Action<T> fn)
        {
            if (_isSuccess) {
                fn(_val);
            }

            return this;
        }

        [Pure]
        public Try<T, TException> RecoverWith([NotNull] Func<Try<T,TException>> fn)
        {
            if (!_isSuccess)
            {
               return fn();
            }

            return this;
        }

        [Pure]
        public T Recover([NotNull] Func<T> fn)
        {
            if (!_isSuccess)
            {
                return fn();
            }

            return _val;
        }

        public Try<T, TException> Filter(Func<bool> predicate)
        {
            if (!predicate())
            {
                return new Try<T, TException>();
            }

            return this;
        }

        public Maybe<T> toMaybe()
        {
            return _isSuccess ? Maybe.Just<T>(_val) : Maybe.Nothing<T>();
        }
    }
}
