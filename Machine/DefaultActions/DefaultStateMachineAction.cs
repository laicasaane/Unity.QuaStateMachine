﻿namespace QuaStateMachine
{
    internal abstract class DefaultStateMachineAction : IStateMachineAction
    {
        public IStateMachine Machine
        {
            get => this.machine;
            set => SetMachine(value);
        }

        private IStateMachine machine;

        public virtual void Initialize() { }

        public virtual void StateChange(IState former, IState current) { }

        public virtual void Terminate() { }

        protected void SetMachine(IStateMachine machine)
            => this.machine = machine;
    }
}
