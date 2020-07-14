namespace QuaStateMachine
{
    public abstract class SignalActionBase<T> : SignalActionBase, ISignalAction<T>
    {
        public new ISignal<T> Signal
        {
            get => this.signal;
            set => SetSignal(value);
        }

        private ISignal<T> signal;

        protected void SetSignal(ISignal<T> value)
        {
            this.signal = value;
            base.SetSignal(value);
        }
    }
}
