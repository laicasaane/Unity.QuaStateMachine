using System;

namespace QuaStateMachine
{
    internal sealed class StateMachineActionTick : StateMachineActionBase
    {
        private readonly Action<IStateMachineAction> action;

        internal StateMachineActionTick(Action<IStateMachineAction> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public override void Tick()
        {
            this.action(this);
        }
    }
}
