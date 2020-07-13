using System;

namespace FluentQuaStateMachine
{
    internal sealed class StateMachineActionStateChange<TState, TTransition, TSignal>
        : StateMachineActionBase<TState, TTransition, TSignal>
    {
        private readonly Action<IStateMachineAction, State<TState, TTransition, TSignal>, State<TState, TTransition, TSignal>> action;

        internal StateMachineActionStateChange(Action<IStateMachineAction, State<TState, TTransition, TSignal>, State<TState, TTransition, TSignal>> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public override void StateChange(State<TState, TTransition, TSignal> former, State<TState, TTransition, TSignal> current)
        {
            this.action(this, former, current);
        }
    }
}
