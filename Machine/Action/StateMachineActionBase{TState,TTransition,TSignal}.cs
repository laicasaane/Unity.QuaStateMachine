namespace QuaStateMachine
{
    public abstract class StateMachineActionBase<TState, TTransition, TSignal>
        : StateMachineActionBase<TState>, IStateMachineAction<TState, TTransition, TSignal>
    {
        public new StateMachine<TState, TTransition, TSignal> Machine
        {
            get => this.machine;
            set => SetMachine(value);
        }

        private StateMachine<TState, TTransition, TSignal> machine;

        protected void SetMachine(StateMachine<TState, TTransition, TSignal> machine)
        {
            this.machine = machine;
            base.SetMachine(machine);
        }

        public virtual void StateChange(State<TState, TTransition, TSignal> former, State<TState, TTransition, TSignal> current) { }

        public sealed override void StateChange(IState<TState> former, IState<TState> current) { }
    }
}
