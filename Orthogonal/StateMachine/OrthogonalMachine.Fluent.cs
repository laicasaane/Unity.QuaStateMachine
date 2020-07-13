using System;

namespace FluentQuaStateMachine
{
    public readonly partial struct OrthogonalMachine<TState, TTransition, TSignal>
    {
        public OrthogonalState<TState, TTransition, TSignal> EndOrthogonal()
        {
            return this.OuterState;
        }

        public OrthogonalState<TState, TTransition, TSignal> BeginState(
            TState stateName)
        {
            return this.Machine.BeginState(stateName, this.OuterState);
        }

        public OrthogonalState<TState, TTransition, TSignal> Begin(
            TState stateName)
        {
            return this.Machine.BeginState(stateName, this.OuterState);
        }

        public OrthogonalState<TState, TTransition, TSignal> Begin(
            IState state)
        {
            return this.Machine.Begin(state, this.OuterState);
        }

        public OrthogonalState<TState, TTransition, TSignal> Begin(
            IState<TState> state)
        {
            return this.Machine.Begin(state, this.OuterState);
        }

        public OrthogonalSignal<TState, TTransition, TSignal> BeginSignal(
            TSignal signalName)
        {
            return this.Machine.BeginSignal(signalName, this.OuterState);
        }

        public OrthogonalSignal<TState, TTransition, TSignal> Begin(
            TSignal signalName)
        {
            return this.Machine.BeginSignal(signalName, this.OuterState);
        }

        public OrthogonalSignal<TState, TTransition, TSignal> Begin(
            ISignal signal)
        {
            return this.Machine.Begin(signal, this.OuterState);
        }

        public OrthogonalSignal<TState, TTransition, TSignal> Begin(
            ISignal<TSignal> signal)
        {
            return this.Machine.Begin(signal, this.OuterState);
        }

        public OrthogonalTransition<TState, TTransition, TSignal> BeginTransition(
            TTransition transitionName)
        {
            return this.Machine.BeginTransition(transitionName, this.OuterState);
        }

        public OrthogonalTransition<TState, TTransition, TSignal> Begin(
            TTransition transitionName)
        {
            return this.Machine.BeginTransition(transitionName, this.OuterState);
        }

        public OrthogonalTransition<TState, TTransition, TSignal> Begin(
            ITransition transition)
        {
            return this.Machine.Begin(transition, this.OuterState);
        }

        public OrthogonalTransition<TState, TTransition, TSignal> Begin(
            ITransition<TTransition> transition)
        {
            return this.Machine.Begin(transition, this.OuterState);
        }

        public OrthogonalMachine<TState, TTransition, TSignal> Create(
            TState stateName, out State<TState, TTransition, TSignal> state)
        {
            this.Machine.Create(stateName, out state);
            return this;
        }

        public OrthogonalMachine<TState, TTransition, TSignal> CreateState(
            TState stateName, Action<State<TState, TTransition, TSignal>> onCreated = null)
        {
            this.Machine.CreateState(stateName, onCreated);
            return this;
        }

        public OrthogonalMachine<TState, TTransition, TSignal> Create(
            TState stateName, Action<State<TState, TTransition, TSignal>> onCreated = null)
        {
            this.Machine.Create(stateName, onCreated);
            return this;
        }

        public OrthogonalMachine<TState, TTransition, TSignal> Create(
            TState stateName, out State<TState, TTransition, TSignal> state,
            Action<State<TState, TTransition, TSignal>> onCreated = null)
        {
            this.Machine.Create(stateName, out state, onCreated);
            return this;
        }

        public OrthogonalMachine<TState, TTransition, TSignal> Create(
            TState stateName, out State<TState, TTransition, TSignal> state,
            TState outerStateName, int? orthogonalIndex = null)
        {
            this.Machine.Create(stateName, out state, outerStateName, orthogonalIndex);
            return this;
        }

        public OrthogonalMachine<TState, TTransition, TSignal> CreateState(
            TState stateName, TState outerStateName, int? orthogonalIndex = null,
            Action<State<TState, TTransition, TSignal>> onCreated = null)
        {
            this.Machine.CreateState(stateName, outerStateName, orthogonalIndex, onCreated);
            return this;
        }

        public OrthogonalMachine<TState, TTransition, TSignal> Create(
            TState stateName, TState outerStateName, int? orthogonalIndex = null,
            Action<State<TState, TTransition, TSignal>> onCreated = null)
        {
            this.Machine.Create(stateName, outerStateName, orthogonalIndex, onCreated);
            return this;
        }

        public OrthogonalMachine<TState, TTransition, TSignal> Create(
            TState stateName, out State<TState, TTransition, TSignal> state,
            TState outerStateName, int? orthogonalIndex = null,
            Action<State<TState, TTransition, TSignal>> onCreated = null)
        {
            this.Machine.Create(stateName, out state, outerStateName, orthogonalIndex, onCreated);
            return this;
        }

        public OrthogonalMachine<TState, TTransition, TSignal> Create(
            TState stateName, out State<TState, TTransition, TSignal> state,
            State<TState, TTransition, TSignal> outerState, int? orthogonalIndex = null)
        {
            this.Machine.Create(stateName, out state, outerState, orthogonalIndex);
            return this;
        }

        public OrthogonalMachine<TState, TTransition, TSignal> Create(
            TState stateName, State<TState, TTransition, TSignal> outerState, int? orthogonalIndex = null,
            Action<State<TState, TTransition, TSignal>> onCreated = null)
        {
            this.Machine.Create(stateName, outerState, orthogonalIndex, onCreated);
            return this;
        }

        public OrthogonalMachine<TState, TTransition, TSignal> Create(
            TState stateName, out State<TState, TTransition, TSignal> state,
            State<TState, TTransition, TSignal> outerState, int? orthogonalIndex = null,
            Action<State<TState, TTransition, TSignal>> onCreated = null)
        {
            this.Machine.Create(stateName, out state, outerState, orthogonalIndex, onCreated);
            return this;
        }

        public OrthogonalMachine<TState, TTransition, TSignal> Create(
            TTransition transitionName, TState startStateName, TState endStateName,
            out Transition<TState, TTransition, TSignal> transition)
        {
            this.Machine.Create(transitionName, startStateName, endStateName, out transition);
            return this;
        }

        public OrthogonalMachine<TState, TTransition, TSignal> CreateTransition(
            TTransition transitionName, TState startStateName, TState endStateName,
            Action<Transition<TState, TTransition, TSignal>> onCreated = null)
        {
            this.Machine.CreateTransition(transitionName, startStateName, endStateName, onCreated);
            return this;
        }

        public OrthogonalMachine<TState, TTransition, TSignal> Create(
            TTransition transitionName, TState startStateName, TState endStateName,
            Action<Transition<TState, TTransition, TSignal>> onCreated = null)
        {
            this.Machine.Create(transitionName, startStateName, endStateName, onCreated);
            return this;
        }

        public OrthogonalMachine<TState, TTransition, TSignal> Create(
            TTransition transitionName, TState startStateName, TState endStateName,
            out Transition<TState, TTransition, TSignal> transition,
            Action<Transition<TState, TTransition, TSignal>> onCreated = null)
        {
            this.Machine.Create(transitionName, startStateName, endStateName, out transition, onCreated);
            return this;
        }

        public OrthogonalMachine<TState, TTransition, TSignal> Create(
            TTransition transitionName,
            State<TState, TTransition, TSignal> startState, State<TState, TTransition, TSignal> endState,
            out Transition<TState, TTransition, TSignal> transition)
        {
            this.Machine.Create(transitionName, startState, endState, out transition);
            return this;
        }

        public OrthogonalMachine<TState, TTransition, TSignal> Create(
            TTransition transitionName,
            State<TState, TTransition, TSignal> startState, State<TState, TTransition, TSignal> endState,
            Action<Transition<TState, TTransition, TSignal>> onCreated = null)
        {
            this.Machine.Create(transitionName, startState, endState, onCreated);
            return this;
        }

        public OrthogonalMachine<TState, TTransition, TSignal> Create(
            TTransition transitionName,
            State<TState, TTransition, TSignal> startState, State<TState, TTransition, TSignal> endState,
            out Transition<TState, TTransition, TSignal> transition,
            Action<Transition<TState, TTransition, TSignal>> onCreated = null)
        {
            this.Machine.Create(transitionName, startState, endState, out transition, onCreated);
            return this;
        }

        public OrthogonalMachine<TState, TTransition, TSignal> Create(
            TSignal signalName, TTransition transitionName,
            out Signal<TState, TTransition, TSignal> signal)
        {
            this.Machine.Create(signalName, transitionName, out signal);
            return this;
        }

        public OrthogonalMachine<TState, TTransition, TSignal> CreateSignal(
            TSignal signalName, TTransition transitionName,
            Action<Signal<TState, TTransition, TSignal>> onCreated = null)
        {
            this.Machine.CreateSignal(signalName, transitionName, onCreated);
            return this;
        }

        public OrthogonalMachine<TState, TTransition, TSignal> Create(
            TSignal signalName, TTransition transitionName,
            Action<Signal<TState, TTransition, TSignal>> onCreated = null)
        {
            this.Machine.Create(signalName, transitionName, onCreated);
            return this;
        }

        public OrthogonalMachine<TState, TTransition, TSignal> Create(
            TSignal signalName, TTransition transitionName,
            out Signal<TState, TTransition, TSignal> signal,
            Action<Signal<TState, TTransition, TSignal>> onCreated = null)
        {
            this.Machine.Create(signalName, transitionName, out signal, onCreated);
            return this;
        }

        public OrthogonalMachine<TState, TTransition, TSignal> Create(
            TSignal signalName, Transition<TState, TTransition, TSignal> transition,
            out Signal<TState, TTransition, TSignal> signal)
        {
            this.Machine.Create(signalName, transition, out signal);
            return this;
        }

        public OrthogonalMachine<TState, TTransition, TSignal> Create(
            TSignal signalName, Transition<TState, TTransition, TSignal> transition,
            Action<Signal<TState, TTransition, TSignal>> onCreated = null)
        {
            this.Machine.Create(signalName, transition, onCreated);
            return this;
        }

        public OrthogonalMachine<TState, TTransition, TSignal> Create(
            TSignal signalName, Transition<TState, TTransition, TSignal> transition,
            out Signal<TState, TTransition, TSignal> signal,
            Action<Signal<TState, TTransition, TSignal>> onCreated = null)
        {
            this.Machine.Create(signalName, transition, out signal, onCreated);
            return this;
        }

        public OrthogonalMachine<TState, TTransition, TSignal> Connect(
            TSignal signalName, TTransition transitionName)
        {
            this.Machine.Connect(signalName, transitionName);
            return this;
        }

        public OrthogonalMachine<TState, TTransition, TSignal> Connect(
            TSignal signalName, Transition<TState, TTransition, TSignal> transition)
        {
            this.Machine.Connect(signalName, transition);
            return this;
        }

        public OrthogonalMachine<TState, TTransition, TSignal> Connect(
            Signal<TState, TTransition, TSignal> signal, TTransition transitionName)
        {
            this.Machine.Connect(signal, transitionName);
            return this;
        }

        public OrthogonalMachine<TState, TTransition, TSignal> Connect(
            Signal<TState, TTransition, TSignal> signal, Transition<TState, TTransition, TSignal> transition)
        {
            this.Machine.Connect(signal, transition);
            return this;
        }

        public OrthogonalMachine<TState, TTransition, TSignal> Initial(
            TState stateName)
        {
            this.Machine.Initial(stateName);
            return this;
        }

        public OrthogonalMachine<TState, TTransition, TSignal> Initial(
            State<TState, TTransition, TSignal> state)
        {
            this.Machine.Initial(state);
            return this;
        }

        public OrthogonalMachine<TState, TTransition, TSignal> Initial(
            TState stateName, TState outerStateName, int? orthogonalIndex = null)
        {
            this.Machine.Initial(stateName, outerStateName, orthogonalIndex);
            return this;
        }

        public OrthogonalMachine<TState, TTransition, TSignal> Initial(
            TState stateName, State<TState, TTransition, TSignal> outerState, int? orthogonalIndex = null)
        {
            this.Machine.Initial(stateName, outerState, orthogonalIndex);
            return this;
        }

        public OrthogonalMachine<TState, TTransition, TSignal> Initial(
            State<TState, TTransition, TSignal> state, State<TState, TTransition, TSignal> outerState, int? orthogonalIndex = null)
        {
            this.Machine.Initial(state, outerState, orthogonalIndex);
            return this;
        }

        public OrthogonalMachine<TState, TTransition, TSignal> On(
            IStateMachineAction action)
        {
            this.Machine.On(action);
            return this;
        }

        public OrthogonalMachine<TState, TTransition, TSignal> On<T>()
            where T : IStateMachineAction, new()
        {
            this.Machine.On<T>();
            return this;
        }

        public OrthogonalMachine<TState, TTransition, TSignal> OnInitialize(
            Action<IStateMachineAction> action)
        {
            this.Machine.OnInitialize(action);
            return this;
        }

        public OrthogonalMachine<TState, TTransition, TSignal> OnTerminate(
            Action<IStateMachineAction> action)
        {
            this.Machine.OnTerminate(action);
            return this;
        }

        public OrthogonalMachine<TState, TTransition, TSignal> OnStateChange(
            Action<IStateMachineAction, IState, IState> action)
        {
            this.Machine.OnStateChange(action);
            return this;
        }

        public OrthogonalMachine<TState, TTransition, TSignal> OnStateChange(
            Action<IStateMachineAction, IState<TState>, IState<TState>> action)
        {
            this.Machine.OnStateChange(action);
            return this;
        }

        public OrthogonalMachine<TState, TTransition, TSignal> OnStateChange(
            Action<IStateMachineAction, State<TState, TTransition, TSignal>, State<TState, TTransition, TSignal>> action)
        {
            this.Machine.OnStateChange(action);
            return this;
        }
    }
}
