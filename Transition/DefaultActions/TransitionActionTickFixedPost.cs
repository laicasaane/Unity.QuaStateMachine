using System;

namespace QuaStateMachine
{
    internal sealed class TransitionActionTickFixedPost : DefaultTransitionAction, IPostFixedTickable
    {
        private readonly Action<ITransitionAction> action;

        internal TransitionActionTickFixedPost(Action<ITransitionAction> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void PostFixedTick()
        {
            this.action(this);
        }
    }
}
