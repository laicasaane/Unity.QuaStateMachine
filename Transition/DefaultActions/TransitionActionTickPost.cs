using System;

namespace QuaStateMachine
{
    internal sealed class TransitionActionTickPost : DefaultTransitionAction, IPostTickable
    {
        private readonly Action<ITransitionAction> action;

        internal TransitionActionTickPost(Action<ITransitionAction> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void PostTick()
        {
            this.action(this);
        }
    }
}
