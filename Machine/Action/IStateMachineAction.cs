namespace QuaStateMachine
{
    public interface IStateMachineAction
    {
        IStateMachine Machine { get; set; }

        void Initialize();

        void StateChange(IState former, IState current);

        void Tick();

        void Terminate();
    }

    public interface IStateMachineAction<T> : IStateMachineAction
    {
        void StateChange(IState<T> former, IState<T> current);
    }

    public interface IStateMachineAction<TState, TTransition, TSignal> : IStateMachineAction<TState>
    {
        new StateMachine<TState, TTransition, TSignal> Machine { get; set; }

        void StateChange(State<TState, TTransition, TSignal> former, State<TState, TTransition, TSignal> current);
    }
}
