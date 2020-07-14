using System;

namespace QuaStateMachine
{
    public readonly struct StateDirection<T> : IEquatable<StateDirection<T>>
    {
        public readonly T From;
        public readonly T To;

        public StateDirection(T from, T to)
        {
            this.From = from;
            this.To = to;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = (int)2166136261;
                hash = (hash * 16777619) ^ this.From.GetHashCode();
                hash = (hash * 16777619) ^ this.To.GetHashCode();
                return hash;
            }
        }

        public override bool Equals(object obj)
            => obj is StateDirection<T> other &&
               this.From.Equals(other.From) && this.To.Equals(other.To);

        public bool Equals(StateDirection<T> other)
            => this.From.Equals(other.From) && this.To.Equals(other.To);

        public void Deconstruct(out T from, out T to)
        {
            from = this.From;
            to = this.To;
        }

        public override string ToString()
            => $"({this.From} --> {this.To})";

        public static implicit operator StateDirection<T>(in (T from, T to) value)
            => new StateDirection<T>(value.from, value.to);

        public static bool operator ==(in StateDirection<T> lhs, in StateDirection<T> rhs)
            => lhs.From.Equals(rhs.From) && lhs.To.Equals(rhs.To);

        public static bool operator !=(in StateDirection<T> lhs, in StateDirection<T> rhs)
            => !lhs.From.Equals(rhs.From) || !lhs.To.Equals(rhs.To);
    }
}
