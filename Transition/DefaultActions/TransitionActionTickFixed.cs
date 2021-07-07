using System;

namespace QuaStateMachine
{
    internal sealed class TransitionActionTickFixed : DefaultTransitionAction, IFixedTickable
    {
        private readonly Action<ITransitionAction> action;

        internal TransitionActionTickFixed(Action<ITransitionAction> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void FixedTick()
        {
            this.action(this);
        }
    }
}
