using System;

namespace QuaStateMachine
{
    internal sealed class StateMachineActionInitialize : StateMachineActionBase
    {
        private readonly Action<IStateMachineAction> action;

        internal StateMachineActionInitialize(Action<IStateMachineAction> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public override void Initialize()
        {
            this.action(this);
        }
    }
}
