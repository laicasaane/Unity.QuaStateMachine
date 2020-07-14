using System;

namespace QuaStateMachine
{
    internal sealed class TransitionActionFinish : DefaultTransitionAction
    {
        private readonly Action<ITransitionAction> action;

        internal TransitionActionFinish(Action<ITransitionAction> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public override void Finish()
        {
            this.action(this);
        }
    }
}
