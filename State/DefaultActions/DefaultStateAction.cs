namespace QuaStateMachine
{
    internal abstract class DefaultStateAction : IStateAction
    {
        public IState State
        {
            get => this.state;
            set => SetState(value);
        }

        private bool initialized = false;
        private IState state;

        private void Initialize()
        {
            if (this.initialized)
                return;

            OnInitialize();
            this.initialized = true;
        }

        public virtual void Enter(IState previous)
            => Initialize();

        public virtual void Resume(IState next) { }

        public virtual void EnterComplete(IState previous) { }

        public virtual void Exit(IState next) { }

        public virtual void Tick() { }

        public virtual void Terminate() { }

        protected void SetState(IState state)
            => this.state = state;

        protected virtual void OnInitialize() { }
    }
}
