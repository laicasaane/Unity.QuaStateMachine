namespace QuaStateMachine
{
    public abstract class TransitionActionBase<T> : TransitionActionBase, ITransitionAction<T>
    {
        public new ITransition<T> Transition
        {
            get => this.transition;
            set => SetTransition(value);
        }

        private ITransition<T> transition;

        protected void SetTransition(ITransition<T> value)
        {
            this.transition = value as ITransition<T>;
            base.SetTransition(value);
        }
    }
}
