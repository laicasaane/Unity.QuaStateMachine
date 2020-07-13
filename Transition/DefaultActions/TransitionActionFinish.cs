using System;

namespace FluentQuaStateMachine
{
    internal sealed class TransitionActionFinish : TransitionActionBase
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
