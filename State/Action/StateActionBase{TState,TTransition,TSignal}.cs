namespace FluentQuaStateMachine
{
    public abstract class StateActionBase<TState, TTransition, TSignal> : StateActionBase<TState>, IStateAction<TState, TTransition, TSignal>
    {
        public new State<TState, TTransition, TSignal> State
        {
            get => this.state;
            set => SetState(value);
        }

        private State<TState, TTransition, TSignal> state;

        protected void SetState(State<TState, TTransition, TSignal> state)
        {
            this.state = state;
            base.SetState(state);
        }
    }
}