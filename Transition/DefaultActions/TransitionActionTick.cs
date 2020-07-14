using System;

namespace QuaStateMachine
{
    internal sealed class TransitionActionTick : DefaultTransitionAction
    {
        private readonly Action<ITransitionAction> action;

        internal TransitionActionTick(Action<ITransitionAction> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public override void Tick()
        {
            this.action(this);
        }
    }
}
