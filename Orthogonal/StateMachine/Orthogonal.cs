namespace FluentQuaStateMachine
{
    public sealed partial class Orthogonal<TState, TTransition, TSignal>
        : IOrthogonal<TState, TTransition, TSignal>
    {
        public int Index { get; }

        public StateMachine<TState, TTransition, TSignal> Machine { get; }

        public State<TState, TTransition, TSignal> OuterState { get; }

        public State<TState, TTransition, TSignal> CurrentState
            => this.Machine.CurrentStateI;

        internal Orthogonal(State<TState, TTransition, TSignal> parentState, int index)
        {
            this.Index = index;
            this.OuterState = parentState;
            this.Machine = new StateMachine<TState, TTransition, TSignal>();

            ConnectStateChangedEvent();
        }

        private Orthogonal(StateMachine<TState, TTransition, TSignal> machine)
        {
            this.Machine = machine;
        }

        internal void ConnectStateChangedEvent()
        {
            if (this.OuterState.MachineI != null)
                this.Machine.AddAction(new FireOnStateChangedAction(this.OuterState.MachineI));
        }

        public static explicit operator Orthogonal<TState, TTransition, TSignal>(StateMachine<TState, TTransition, TSignal> machine)
            => new Orthogonal<TState, TTransition, TSignal>(machine);

        private sealed class FireOnStateChangedAction : StateMachineActionBase
        {
            private readonly StateMachine<TState, TTransition, TSignal> machine;

            public FireOnStateChangedAction(StateMachine<TState, TTransition, TSignal> machine)
            {
                this.machine = machine ?? throw new System.ArgumentNullException(nameof(machine));
            }

            public void FireOnStateChanged(State<TState, TTransition, TSignal> former,
                                           State<TState, TTransition, TSignal> current)
                => this.machine.FireOnStateChanged(former, current);
        }
    }
}