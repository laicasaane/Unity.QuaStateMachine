using System;

namespace QuaStateMachine
{
    public sealed class TransitionCondition : ITransitionCondition
    {
        private readonly Func<bool> callback;

        internal TransitionCondition(Func<bool> callback)
        {
            this.callback = callback ?? throw new ArgumentNullException(nameof(callback));
        }

        public bool Validate()
            => this.callback();

        public void Invalidate() { }
    }
}
