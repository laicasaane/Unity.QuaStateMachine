namespace QuaStateMachine
{
    internal abstract class DefaultSignalAction : ISignalAction
    {
        public ISignal Signal
        {
            get => this.signal;
            set => SetSignal(value);
        }

        private ISignal signal;

        public virtual void Emit() { }

        public virtual void Process() { }

        public virtual void NotProcess(SignalNotProcessedArgs args) { }

        protected void SetSignal(ISignal value)
            => this.signal = value;
    }
}
