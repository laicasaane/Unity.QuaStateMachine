namespace QuaStateMachine
{
    internal abstract class DefaultTransitionAction : ITransitionAction
    {
        public ITransition Transition
        {
            get => this.transition;
            set => SetTransition(value);
        }

        private ITransition transition;
        private bool initialized = false;

        private void Initialize()
        {
            if (this.initialized)
                return;

            OnIntialize();
            this.initialized = true;
        }

        public virtual void Invoke(TransitionArgs args)
            => Initialize();

        public virtual void Start(TransitionArgs args) { }

        public virtual void Tick() { }

        public virtual void Finish() { }

        protected void SetTransition(ITransition value)
            => this.transition = value;

        protected virtual void OnIntialize() { }
    }
}
