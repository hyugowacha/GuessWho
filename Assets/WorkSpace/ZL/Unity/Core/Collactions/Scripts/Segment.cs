using System;

using System.Collections.Generic;

using UnityEngine;

namespace ZL.Unity.Collections
{
    [Serializable]

    public struct Segment<T> : IEquatable<Segment<T>>

        where T : IEquatable<T>
    {
        [SerializeField]

        private T start;

        public readonly T Start => start;

        [SerializeField]

        private T end;

        public readonly T End => end;

        public Segment(T start, T end)
        {
            this.start = start;

            this.end = end;
        }

        public override bool Equals(object obj)
        {
            if (obj is Segment<T> other)
            {
                return Equals(other);
            }

            return false;
        }

        public bool Equals(Segment<T> other)
        {
            return (start.Equals(other.start) && end.Equals(other.end)) || (start.Equals(other.end) && end.Equals(other.start));
        }

        public override readonly int GetHashCode()
        {
            return start.GetHashCode() ^ end.GetHashCode();
        }

        public override readonly string ToString()
        {
            return $"[{start}, {end}]";
        }

        public sealed class EqualityComparer : IEqualityComparer<Segment<T>>
        {
            public bool Equals(Segment<T> x, Segment<T> y)
            {
                return x.Equals(y);
            }

            public int GetHashCode(Segment<T> obj)
            {
                return obj.GetHashCode();
            }
        }
    }
}