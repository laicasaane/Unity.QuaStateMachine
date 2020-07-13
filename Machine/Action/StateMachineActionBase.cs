namespace FluentQuaStateMachine
{
    public abstract class StateMachineActionBase : IStateMachineAction
    {
        public IStateMachine Machine
        {
            get => this.machine;
            set => SetMachine(value);
        }

        private IStateMachine machine;

        public virtual void Initialize() { }

        public virtual void StateChange(IState former, IState current) { }

        public virtual void Tick() { }

        public virtual void Terminate() { }

        protected void SetMachine(IStateMachine machine)
            => this.machine = machine;
    }
}
