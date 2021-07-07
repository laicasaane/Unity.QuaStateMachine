using System;

namespace QuaStateMachine
{
    internal sealed class StateActionTickLate : DefaultStateAction, ILateTickable
    {
        private readonly Action<IStateAction> action;

        internal StateActionTickLate(Action<IStateAction> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void LateTick()
        {
            this.action(this);
        }
    }
}
