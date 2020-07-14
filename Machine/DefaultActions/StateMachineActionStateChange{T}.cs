using System;

namespace QuaStateMachine
{
    internal sealed class StateMachineActionStateChange<T> : StateMachineActionBase<T>
    {
        private readonly Action<IStateMachineAction, IState<T>, IState<T>> action;

        internal StateMachineActionStateChange(Action<IStateMachineAction, IState<T>, IState<T>> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public override void StateChange(IState<T> former, IState<T> current)
        {
            this.action(this, former, current);
        }
    }
}
