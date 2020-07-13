namespace FluentQuaStateMachine
{
    public interface ISignalAction
    {
        ISignal Signal { get; set; }

        void Emit();

        void Process();

        void NotProcess(SignalNotProcessedArgs args);
    }

    public interface ISignalAction<T> : ISignalAction
    {
        new ISignal<T> Signal { get; set; }
    }

    public interface ISignalAction<TState, TTransition, TSignal> : ISignalAction<TSignal>
    {
        new Signal<TState, TTransition, TSignal> Signal { get; set; }
    }
}
