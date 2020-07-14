namespace QuaStateMachine
{
    public abstract class TransitionActionBase<TState, TTransition, TSignal> : TransitionActionBase<TTransition>, ITransitionAction<TState, TTransition, TSignal>
    {
        public new Transition<TState, TTransition, TSignal> Transition
        {
            get => this.transition;
            set => SetTransition(value);
        }

        private Transition<TState, TTransition, TSignal> transition;

        protected void SetTransition(Transition<TState, TTransition, TSignal> value)
        {
            this.transition = value as Transition<TState, TTransition, TSignal>;
            base.SetTransition(value);
        }
    }
}
