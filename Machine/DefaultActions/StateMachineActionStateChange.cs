using System;

namespace QuaStateMachine
{
    internal sealed class StateMachineActionStateChange : DefaultStateMachineAction
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
