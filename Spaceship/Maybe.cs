using System;
using System.Collections;
using System.Collections.Generic;

namespace Spaceship
{
    public struct Maybe<T> : IEnumerable<T>, IEquatable<Maybe<T>>
    {
        private readonly T _value;
        public bool HasValue { get; }

        public Maybe(T value)
        {
            _value = value;
            HasValue = true;
        }

        public T ValueOr(T alternativeValue) => HasValue ? _value : alternativeValue;

        public IEnumerator<T> GetEnumerator()
        {
            if (HasValue)
            {
                yield return _value;
            }
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override bool Equals(object obj) =>
            obj is Maybe<T> && Equals((Maybe<T>)obj);

        public bool Equals(Maybe<T> other) =>
            HasValue || other.HasValue ?
                EqualityComparer<T>.Default.Equals(_value, other._value) && HasValue == other.HasValue :
                true;

        public override int GetHashCode()
        {
            var hashCode = 1814622215;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + HasValue.GetHashCode();
            if (HasValue)
            {
                hashCode = hashCode * -1521134295 + EqualityComparer<T>.Default.GetHashCode(_value);
            }
            return hashCode;
        }

        public static bool operator ==(Maybe<T> left, Maybe<T> right) => left.Equals(right);
        public static bool operator !=(Maybe<T> left, Maybe<T> right) => !(left == right);
    }

    public static class MaybeEx
    {
        public static Maybe<T> ToMaybe<T>(this T value) => new Maybe<T>(value);
    }
}