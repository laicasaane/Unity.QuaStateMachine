using System;

namespace QuaStateMachine
{
    internal sealed class StateMachineActionTickFixed : DefaultStateMachineAction, IFixedTickable
    {
        private readonly Action<IStateMachineAction> action;

        internal StateMachineActionTickFixed(Action<IStateMachineAction> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void FixedTick()
        {
            this.action(this);
        }
    }
}
