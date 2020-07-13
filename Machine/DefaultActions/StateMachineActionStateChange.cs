using System;

namespace FluentQuaStateMachine
{
    internal sealed class StateMachineActionStateChange : StateMachineActionBase
    {
        private readonly Action<IStateMachineAction, IState, IState> action;

        internal StateMachineActionStateChange(Action<IStateMachineAction, IState, IState> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public override void StateChange(IState former, IState current)
        {
            this.action(this, former, current);
        }
    }
}
