using System;

namespace QuaStateMachine
{
    internal sealed class TransitionActionStart : DefaultTransitionAction
    {
        private readonly Action<ITransitionAction, TransitionArgs> action;

        internal TransitionActionStart(Action<ITransitionAction, TransitionArgs> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public override void Start(TransitionArgs args)
        {
            this.action(this, args);
        }
    }
}
