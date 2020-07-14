using System;
using System.Runtime.CompilerServices;

namespace QuaStateMachine
{
    public sealed partial class Orthogonal<TState, TTransition, TSignal>
    {
        public State<TState, TTransition, TSignal> EndOrthogonal()
        {
            return this.OuterState;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private OrthogonalState<TState, TTransition, TSignal> State(
            State<TState, TTransition, TSignal> value, in OrthogonalState<TState, TTransition, TSignal> parent = default)
        {
            return new OrthogonalState<TState, TTransition, TSignal>(this, value, parent);
        }

        public OrthogonalState<TState, TTransition, TSignal> BeginState(
            TState stateName)
        {
            return State(this.Machine.Begin(stateName));
        }

        public OrthogonalState<TState, TTransition, TSignal> Begin(
            TState stateName)
        {
            return State(this.Machine.Begin(stateName));
        }

        public OrthogonalState<TState, TTransition, TSignal> Begin(
            IState state)
        {
            return State(this.Machine.Begin(state));
        }

        public OrthogonalState<TState, TTransition, TSignal> Begin(
            IState<TState> state)
        {
            return State(this.Machine.Begin(state));
        }

        internal OrthogonalState<TState, TTransition, TSignal> BeginState(
            TState stateName, in OrthogonalState<TState, TTransition, TSignal> parent)
        {
            return State(this.Machine.Begin(stateName), parent);
        }

        internal OrthogonalState<TState, TTransition, TSignal> Begin(
            TState stateName, in OrthogonalState<TState, TTransition, TSignal> parent)
        {
            return State(this.Machine.Begin(stateName), parent);
        }

        internal OrthogonalState<TState, TTransition, TSignal> Begin(
            IState state, in OrthogonalState<TState, TTransition, TSignal> parent)
        {
            return State(this.Machine.Begin(state), parent);
        }

        internal OrthogonalState<TState, TTransition, TSignal> Begin(
            IState<TState> state, in OrthogonalState<TState, TTransition, TSignal> parent)
        {
            return State(this.Machine.Begin(state), parent);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private OrthogonalSignal<TState, TTransition, TSignal> Signal(
            Signal<TState, TTransition, TSignal> value,
            in OrthogonalState<TState, TTransition, TSignal> parent = default)
        {
            return new OrthogonalSignal<TState, TTransition, TSignal>(this, value, parent);
        }

        public OrthogonalSignal<TState, TTransition, TSignal> BeginSignal(
            TSignal signalName)
        {
            return Signal(this.Machine.BeginSignal(signalName));
        }

        public OrthogonalSignal<TState, TTransition, TSignal> Begin(
            TSignal signalName)
        {
            return Signal(this.Machine.BeginSignal(signalName));
        }

        public OrthogonalSignal<TState, TTransition, TSignal> Begin(
            ISignal signal)
        {
            return Signal(this.Machine.Begin(signal));
        }

        public OrthogonalSignal<TState, TTransition, TSignal> Begin(
            ISignal<TSignal> signal)
        {
            return Signal(this.Machine.Begin(signal));
        }

        internal OrthogonalSignal<TState, TTransition, TSignal> BeginSignal(
            TSignal signalName, in OrthogonalState<TState, TTransition, TSignal> parent)
        {
            return Signal(this.Machine.BeginSignal(signalName), parent);
        }

        internal OrthogonalSignal<TState, TTransition, TSignal> Begin(
            TSignal signalName, in OrthogonalState<TState, TTransition, TSignal> parent)
        {
            return Signal(this.Machine.BeginSignal(signalName), parent);
        }

        internal OrthogonalSignal<TState, TTransition, TSignal> Begin(
            ISignal signal, in OrthogonalState<TState, TTransition, TSignal> parent)
        {
            return Signal(this.Machine.Begin(signal), parent);
        }

        internal OrthogonalSignal<TState, TTransition, TSignal> Begin(
            ISignal<TSignal> signal, in OrthogonalState<TState, TTransition, TSignal> parent)
        {
            return Signal(this.Machine.Begin(signal), parent);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private OrthogonalTransition<TState, TTransition, TSignal> Transition(
            Transition<TState, TTransition, TSignal> value,
            in OrthogonalState<TState, TTransition, TSignal> parent = default)
        {
            return new OrthogonalTransition<TState, TTransition, TSignal>(this, value, parent);
        }

        public OrthogonalTransition<TState, TTransition, TSignal> BeginTransition(
            TTransition transitionName)
        {
            return Transition(this.Machine.BeginTransition(transitionName));
        }

        public OrthogonalTransition<TState, TTransition, TSignal> Begin(
            TTransition transitionName)
        {
            return Transition(this.Machine.BeginTransition(transitionName));
        }

        public OrthogonalTransition<TState, TTransition, TSignal> Begin(
            ITransition transition)
        {
            return Transition(this.Machine.Begin(transition));
        }

        public OrthogonalTransition<TState, TTransition, TSignal> Begin(
            ITransition<TTransition> transition)
        {
            return Transition(this.Machine.Begin(transition));
        }

        internal OrthogonalTransition<TState, TTransition, TSignal> BeginTransition(
            TTransition transitionName, in OrthogonalState<TState, TTransition, TSignal> parent)
        {
            return Transition(this.Machine.BeginTransition(transitionName), parent);
        }

        internal OrthogonalTransition<TState, TTransition, TSignal> Begin(
            TTransition transitionName, in OrthogonalState<TState, TTransition, TSignal> parent)
        {
            return Transition(this.Machine.BeginTransition(transitionName), parent);
        }

        internal OrthogonalTransition<TState, TTransition, TSignal> Begin(
            ITransition transition, in OrthogonalState<TState, TTransition, TSignal> parent)
        {
            return Transition(this.Machine.Begin(transition), parent);
        }

        internal OrthogonalTransition<TState, TTransition, TSignal> Begin(
            ITransition<TTransition> transition, in OrthogonalState<TState, TTransition, TSignal> parent)
        {
            return Transition(this.Machine.Begin(transition), parent);
        }

        public Orthogonal<TState, TTransition, TSignal> Create(
            TState stateName, out State<TState, TTransition, TSignal> state)
        {
            this.Machine.Create(stateName, out state);
            return this;
        }

        public Orthogonal<TState, TTransition, TSignal> CreateState(
            TState stateName, Action<State<TState, TTransition, TSignal>> onCreated = null)
        {
            this.Machine.CreateState(stateName, onCreated);
            return this;
        }

        public Orthogonal<TState, TTransition, TSignal> Create(
            TState stateName, Action<State<TState, TTransition, TSignal>> onCreated = null)
        {
            this.Machine.Create(stateName, onCreated);
            return this;
        }

        public Orthogonal<TState, TTransition, TSignal> Create(
            TState stateName, out State<TState, TTransition, TSignal> state,
            Action<State<TState, TTransition, TSignal>> onCreated = null)
        {
            this.Machine.Create(stateName, out state, onCreated);
            return this;
        }

        public Orthogonal<TState, TTransition, TSignal> Create(
            TState stateName, out State<TState, TTransition, TSignal> state,
            TState outerStateName, int? orthogonalIndex = null)
        {
            this.Machine.Create(stateName, out state, outerStateName, orthogonalIndex);
            return this;
        }

        public Orthogonal<TState, TTransition, TSignal> CreateState(
            TState stateName, TState outerStateName, int? orthogonalIndex = null,
            Action<State<TState, TTransition, TSignal>> onCreated = null)
        {
            this.Machine.CreateState(stateName, outerStateName, orthogonalIndex, onCreated);
            return this;
        }

        public Orthogonal<TState, TTransition, TSignal> Create(
            TState stateName, TState outerStateName, int? orthogonalIndex = null,
            Action<State<TState, TTransition, TSignal>> onCreated = null)
        {
            this.Machine.Create(stateName, outerStateName, orthogonalIndex, onCreated);
            return this;
        }

        public Orthogonal<TState, TTransition, TSignal> Create(
            TState stateName, out State<TState, TTransition, TSignal> state,
            TState outerStateName, int? orthogonalIndex = null,
            Action<State<TState, TTransition, TSignal>> onCreated = null)
        {
            this.Machine.Create(stateName, out state, outerStateName, orthogonalIndex, onCreated);
            return this;
        }

        public Orthogonal<TState, TTransition, TSignal> Create(
            TState stateName, out State<TState, TTransition, TSignal> state,
            State<TState, TTransition, TSignal> outerState, int? orthogonalIndex = null)
        {
            this.Machine.Create(stateName, out state, outerState, orthogonalIndex);
            return this;
        }

        public Orthogonal<TState, TTransition, TSignal> Create(
            TState stateName, State<TState, TTransition, TSignal> outerState, int? orthogonalIndex = null,
            Action<State<TState, TTransition, TSignal>> onCreated = null)
        {
            this.Machine.Create(stateName, outerState, orthogonalIndex, onCreated);
            return this;
        }

        public Orthogonal<TState, TTransition, TSignal> Create(
            TState stateName, out State<TState, TTransition, TSignal> state,
            State<TState, TTransition, TSignal> outerState, int? orthogonalIndex = null,
            Action<State<TState, TTransition, TSignal>> onCreated = null)
        {
            this.Machine.Create(stateName, out state, outerState, orthogonalIndex, onCreated);
            return this;
        }

        public Orthogonal<TState, TTransition, TSignal> Create(
            TTransition transitionName, TState startStateName, TState endStateName,
            out Transition<TState, TTransition, TSignal> transition)
        {
            this.Machine.Create(transitionName, startStateName, endStateName, out transition);
            return this;
        }

        public Orthogonal<TState, TTransition, TSignal> CreateTransition(
            TTransition transitionName, TState startStateName, TState endStateName,
            Action<Transition<TState, TTransition, TSignal>> onCreated = null)
        {
            this.Machine.CreateTransition(transitionName, startStateName, endStateName, onCreated);
            return this;
        }

        public Orthogonal<TState, TTransition, TSignal> Create(
            TTransition transitionName, TState startStateName, TState endStateName,
            Action<Transition<TState, TTransition, TSignal>> onCreated = null)
        {
            this.Machine.Create(transitionName, startStateName, endStateName, onCreated);
            return this;
        }

        public Orthogonal<TState, TTransition, TSignal> Create(
            TTransition transitionName, TState startStateName, TState endStateName,
            out Transition<TState, TTransition, TSignal> transition,
            Action<Transition<TState, TTransition, TSignal>> onCreated = null)
        {
            this.Machine.Create(transitionName, startStateName, endStateName, out transition, onCreated);
            return this;
        }

        public Orthogonal<TState, TTransition, TSignal> Create(
            TTransition transitionName, State<TState, TTransition, TSignal> startState,
            State<TState, TTransition, TSignal> endState,
            out Transition<TState, TTransition, TSignal> transition)
        {
            this.Machine.Create(transitionName, startState, endState, out transition);
            return this;
        }

        public Orthogonal<TState, TTransition, TSignal> Create(
            TTransition transitionName, State<TState, TTransition, TSignal> startState,
            State<TState, TTransition, TSignal> endState,
            Action<Transition<TState, TTransition, TSignal>> onCreated = null)
        {
            this.Machine.Create(transitionName, startState, endState, onCreated);
            return this;
        }

        public Orthogonal<TState, TTransition, TSignal> Create(
            TTransition transitionName, State<TState, TTransition, TSignal> startState,
            State<TState, TTransition, TSignal> endState,
            out Transition<TState, TTransition, TSignal> transition,
            Action<Transition<TState, TTransition, TSignal>> onCreated = null)
        {
            this.Machine.Create(transitionName, startState, endState, out transition, onCreated);
            return this;
        }

        public Orthogonal<TState, TTransition, TSignal> Create(
            TSignal signalName, TTransition transitionName,
            out Signal<TState, TTransition, TSignal> signal)
        {
            this.Machine.Create(signalName, transitionName, out signal);
            return this;
        }

        public Orthogonal<TState, TTransition, TSignal> CreateSignal(
            TSignal signalName, TTransition transitionName,
            Action<Signal<TState, TTransition, TSignal>> onCreated = null)
        {
            this.Machine.CreateSignal(signalName, transitionName, onCreated);
            return this;
        }

        public Orthogonal<TState, TTransition, TSignal> Create(
            TSignal signalName, TTransition transitionName,
            Action<Signal<TState, TTransition, TSignal>> onCreated = null)
        {
            this.Machine.Create(signalName, transitionName, onCreated);
            return this;
        }

        public Orthogonal<TState, TTransition, TSignal> Create(
            TSignal signalName, TTransition transitionName,
            out Signal<TState, TTransition, TSignal> signal,
            Action<Signal<TState, TTransition, TSignal>> onCreated = null)
        {
            this.Machine.Create(signalName, transitionName, out signal, onCreated);
            return this;
        }

        public Orthogonal<TState, TTransition, TSignal> Create(
            TSignal signalName, Transition<TState, TTransition, TSignal> transition,
            out Signal<TState, TTransition, TSignal> signal)
        {
            this.Machine.Create(signalName, transition, out signal);
            return this;
        }

        public Orthogonal<TState, TTransition, TSignal> Create(
            TSignal signalName, Transition<TState, TTransition, TSignal> transition,
            Action<Signal<TState, TTransition, TSignal>> onCreated = null)
        {
            this.Machine.Create(signalName, transition, onCreated);
            return this;
        }

        public Orthogonal<TState, TTransition, TSignal> Create(
            TSignal signalName, Transition<TState, TTransition, TSignal> transition,
            out Signal<TState, TTransition, TSignal> signal,
            Action<Signal<TState, TTransition, TSignal>> onCreated = null)
        {
            this.Machine.Create(signalName, transition, out signal, onCreated);
            return this;
        }

        public Orthogonal<TState, TTransition, TSignal> Connect(
            TSignal signalName, TTransition transitionName)
        {
            this.Machine.Connect(signalName, transitionName);
            return this;
        }

        public Orthogonal<TState, TTransition, TSignal> Connect(
            TSignal signalName, Transition<TState, TTransition, TSignal> transition)
        {
            this.Machine.Connect(signalName, transition);
            return this;
        }

        public Orthogonal<TState, TTransition, TSignal> Connect(
            Signal<TState, TTransition, TSignal> signal, TTransition transitionName)
        {
            this.Machine.Connect(signal, transitionName);
            return this;
        }

        public Orthogonal<TState, TTransition, TSignal> Connect(
            Signal<TState, TTransition, TSignal> signal, Transition<TState, TTransition, TSignal> transition)
        {
            this.Machine.Connect(signal, transition);
            return this;
        }

        public Orthogonal<TState, TTransition, TSignal> Initial(
            TState stateName)
        {
            this.Machine.Initial(stateName);
            return this;
        }

        public Orthogonal<TState, TTransition, TSignal> Initial(
            State<TState, TTransition, TSignal> state)
        {
            this.Machine.Initial(state);
            return this;
        }

        public Orthogonal<TState, TTransition, TSignal> Initial(
            TState stateName, TState outerStateName, int? orthogonalIndex = null)
        {
            this.Machine.Initial(stateName, outerStateName, orthogonalIndex);
            return this;
        }

        public Orthogonal<TState, TTransition, TSignal> Initial(
            TState stateName, State<TState, TTransition, TSignal> outerState, int? orthogonalIndex = null)
        {
            this.Machine.Initial(stateName, outerState, orthogonalIndex);
            return this;
        }

        public Orthogonal<TState, TTransition, TSignal> Initial(
            State<TState, TTransition, TSignal> state, State<TState, TTransition, TSignal> outerState,
            int? orthogonalIndex = null)
        {
            this.Machine.Initial(state, outerState, orthogonalIndex);
            return this;
        }

        public Orthogonal<TState, TTransition, TSignal> On(
            IStateMachineAction action)
        {
            this.Machine.On(action);
            return this;
        }

        public Orthogonal<TState, TTransition, TSignal> On<T>()
            where T : IStateMachineAction, new()
        {
            this.Machine.On<T>();
            return this;
        }

        public Orthogonal<TState, TTransition, TSignal> OnInitialize(
            Action<IStateMachineAction> action)
        {
            this.Machine.OnInitialize(action);
            return this;
        }

        public Orthogonal<TState, TTransition, TSignal> OnTerminate(
            Action<IStateMachineAction> action)
        {
            this.Machine.OnTerminate(action);
            return this;
        }

        public Orthogonal<TState, TTransition, TSignal> OnStateChange(
            Action<IStateMachineAction, IState, IState> action)
        {
            this.Machine.OnStateChange(action);
            return this;
        }

        public Orthogonal<TState, TTransition, TSignal> OnStateChange(
            Action<IStateMachineAction, IState<TState>, IState<TState>> action)
        {
            this.Machine.OnStateChange(action);
            return this;
        }

        public Orthogonal<TState, TTransition, TSignal> OnStateChange(
            Action<IStateMachineAction, State<TState, TTransition, TSignal>, State<TState, TTransition, TSignal>> action)
        {
            this.Machine.OnStateChange(action);
            return this;
        }
    }
}