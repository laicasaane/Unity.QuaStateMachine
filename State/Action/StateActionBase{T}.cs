namespace FluentQuaStateMachine
{
    public abstract class StateActionBase<T> : StateActionBase, IStateAction<T>
    {
        public new IState<T> State
        {
            get => this.state;
            set => SetState(value);
        }

        private IState<T> state;

        protected void SetState(IState<T> state)
        {
            this.state = state;
            base.SetState(state);
        }
    }
}