using System;
using System.Collections.Generic;

namespace QuaStateMachine
{
    public readonly struct TransitionCondition : ITransitionCondition, IEquatable<TransitionCondition>, IEqualityComparer<TransitionCondition>
    {
        private readonly Func<bool> callback;

        public TransitionCondition(Func<bool> callback)
        {
            this.callback = callback ?? throw new ArgumentNullException(nameof(callback));
        }

        public override int GetHashCode()
            => this.callback.GetHashCode();

        public override bool Equals(object obj)
            => obj is TransitionCondition other ? this.callback.Equals(other.callback) : false;

        public bool Equals(TransitionCondition other)
            => this.callback.Equals(other.callback);

        public bool Equals(TransitionCondition x, TransitionCondition y)
            => x.callback.Equals(y.callback);

        public int GetHashCode(TransitionCondition obj)
            => obj.callback.GetHashCode();

        public bool Validate()
        {
            return this.callback();
        }
    }
}
