namespace FluentQuaStateMachine
{
    public interface ITransitionAction
    {
        ITransition Transition { get; set; }

        void Invoke(TransitionArgs args);

        void Start(TransitionArgs args);

        void Tick();

        void Finish();
    }

    public interface ITransitionAction<T> : ITransitionAction
    {
        new ITransition<T> Transition { get; set; }
    }

    public interface ITransitionAction<TState, TTransition, TSignal> : ITransitionAction<TTransition>
    {
        new Transition<TState, TTransition, TSignal> Transition { get; set; }
    }
}
