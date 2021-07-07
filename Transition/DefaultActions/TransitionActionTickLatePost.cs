using System;

namespace QuaStateMachine
{
    internal sealed class TransitionActionTickLatePost : DefaultTransitionAction, IPostLateTickable
    {
        private readonly Action<ITransitionAction> action;

        internal TransitionActionTickLatePost(Action<ITransitionAction> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void PostLateTick()
        {
            this.action(this);
        }
    }
}
