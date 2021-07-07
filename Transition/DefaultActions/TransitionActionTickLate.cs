using System;

namespace QuaStateMachine
{
    internal sealed class TransitionActionTickLate : DefaultTransitionAction, ILateTickable
    {
        private readonly Action<ITransitionAction> action;

        internal TransitionActionTickLate(Action<ITransitionAction> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void LateTick()
        {
            this.action(this);
        }
    }
}
