namespace QuaStateMachine
{
    public readonly partial struct OrthogonalMachine<TState, TTransition, TSignal>
    {
        public Orthogonal<TState, TTransition, TSignal> Machine { get; }

        public OrthogonalState<TState, TTransition, TSignal> OuterState { get; }

        public OrthogonalMachine(Orthogonal<TState, TTransition, TSignal> machine, in OrthogonalState<TState, TTransition, TSignal> outerState)
        {
            this.Machine = machine;
            this.OuterState = outerState;
        }
    }
}