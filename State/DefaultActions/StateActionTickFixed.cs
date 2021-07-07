using System;

namespace QuaStateMachine
{
    internal sealed class StateActionTickFixed : DefaultStateAction, IFixedTickable
    {
        private readonly Action<IStateAction> action;

        internal StateActionTickFixed(Action<IStateAction> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void FixedTick()
        {
            this.action(this);
        }
    }
}
