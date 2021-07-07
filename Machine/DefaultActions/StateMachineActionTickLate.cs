using System;

namespace QuaStateMachine
{
    internal sealed class StateMachineActionTickLate : DefaultStateMachineAction, ILateTickable
    {
        private readonly Action<IStateMachineAction> action;

        internal StateMachineActionTickLate(Action<IStateMachineAction> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void LateTick()
        {
            this.action(this);
        }
    }
}
