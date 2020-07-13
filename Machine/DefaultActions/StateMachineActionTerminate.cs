using System;

namespace FluentQuaStateMachine
{
    internal sealed class StateMachineActionTerminate : StateMachineActionBase
    {
        private readonly Action<IStateMachineAction> action;

        internal StateMachineActionTerminate(Action<IStateMachineAction> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public override void Terminate()
        {
            this.action(this);
        }
    }
}
