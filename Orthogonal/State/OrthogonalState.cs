namespace QuaStateMachine
{
    public readonly partial struct OrthogonalState<TState, TTransition, TSignal>
    {
        public Orthogonal<TState, TTransition, TSignal> Machine { get; }

        public State<TState, TTransition, TSignal> State { get; }

        public OuterOrthogonal Outer { get; }

        public OrthogonalState(Orthogonal<TState, TTransition, TSignal> machine, State<TState, TTransition, TSignal> state, in OrthogonalState<TState, TTransition, TSignal> parent = default)
        {
            this.Machine = machine;
            this.State = state;
            this.Outer = new OuterOrthogonal(parent.Machine, parent.State);
        }

        public readonly struct OuterOrthogonal
        {
            public Orthogonal<TState, TTransition, TSignal> Machine { get; }

            public State<TState, TTransition, TSignal> State { get; }

            public OuterOrthogonal(Orthogonal<TState, TTransition, TSignal> machine, State<TState, TTransition, TSignal> state)
            {
                this.Machine = machine;
                this.State = state;
            }

            public static implicit operator OrthogonalState<TState, TTransition, TSignal>(in OuterOrthogonal value)
                => new OrthogonalState<TState, TTransition, TSignal>(value.Machine, value.State);
        }
    }
}
