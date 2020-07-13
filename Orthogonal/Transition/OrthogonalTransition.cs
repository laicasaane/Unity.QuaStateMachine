namespace FluentQuaStateMachine
{
    public readonly partial struct OrthogonalTransition<TState, TTransition, TSignal>
    {
        public Orthogonal<TState, TTransition, TSignal> Machine { get; }

        public Transition<TState, TTransition, TSignal> Transition { get; }

        public OrthogonalState<TState, TTransition, TSignal> OuterState { get; }

        public OrthogonalTransition(Orthogonal<TState, TTransition, TSignal> machine,
            Transition<TState, TTransition, TSignal> transition,
            in OrthogonalState<TState, TTransition, TSignal> outerState = default)
        {
            this.Machine = machine;
            this.Transition = transition;
            this.OuterState = outerState;
        }
    }
}
