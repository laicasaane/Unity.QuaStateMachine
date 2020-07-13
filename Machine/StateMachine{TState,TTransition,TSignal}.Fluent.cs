using System;

namespace FluentQuaStateMachine
{
    public partial class StateMachine<TState, TTransition, TSignal>
    {
        public StateMachine<TState, TTransition, TSignal> Build()
        {
            Initialize();
            return this;
        }

        public State<TState, TTransition, TSignal> BeginState(
            TState stateName)
        {
            return GetStateByName(stateName);
        }

        public State<TState, TTransition, TSignal> Begin(
            TState stateName)
        {
            return GetStateByName(stateName);
        }

        public State<TState, TTransition, TSignal> Begin(
            IState state)
        {
            return GetStateByName((TState)state.Name);
        }

        public State<TState, TTransition, TSignal> Begin(
            IState<TState> state)
        {
            return GetStateByName(state.Name);
        }

        public Signal<TState, TTransition, TSignal> BeginSignal(
            TSignal signalName)
        {
            return GetSignalByName(signalName);
        }

        public Signal<TState, TTransition, TSignal> Begin(
            TSignal signalName)
        {
            return GetSignalByName(signalName);
        }

        public Signal<TState, TTransition, TSignal> Begin(
            ISignal signal)
        {
            return GetSignalByName((TSignal)signal.Name);
        }

        public Signal<TState, TTransition, TSignal> Begin(
            ISignal<TSignal> signal)
        {
            return GetSignalByName(signal.Name);
        }

        public Transition<TState, TTransition, TSignal> BeginTransition(
            TTransition transitionName)
        {
            return GetTransitionByName(transitionName);
        }

        public Transition<TState, TTransition, TSignal> Begin(
            TTransition transitionName)
        {
            return GetTransitionByName(transitionName);
        }

        public Transition<TState, TTransition, TSignal> Begin(
            ITransition transition)
        {
            return GetTransitionByName((TTransition)transition.Name);
        }

        public Transition<TState, TTransition, TSignal> Begin(
            ITransition<TTransition> transition)
        {
            return GetTransitionByName(transition.Name);
        }

        public StateMachine<TState, TTransition, TSignal> Create(
            TState stateName, out State<TState, TTransition, TSignal> state)
        {
            state = CreateState(stateName);
            return this;
        }

        public StateMachine<TState, TTransition, TSignal> CreateState(
            TState stateName,
            Action<State<TState, TTransition, TSignal>> onCreated = null)
        {
            var state = CreateState(stateName);
            onCreated?.Invoke(state);

            return this;
        }

        public StateMachine<TState, TTransition, TSignal> Create(
            TState stateName,
            Action<State<TState, TTransition, TSignal>> onCreated = null)
        {
            var state = CreateState(stateName);
            onCreated?.Invoke(state);

            return this;
        }

        public StateMachine<TState, TTransition, TSignal> Create(
            TState stateName,
            out State<TState, TTransition, TSignal> state,
            Action<State<TState, TTransition, TSignal>> onCreated = null)
        {
            state = CreateState(stateName);
            onCreated?.Invoke(state);

            return this;
        }

        public StateMachine<TState, TTransition, TSignal> Create(
            TState stateName, out State<TState, TTransition, TSignal> state,
            TState outerStateName)
        {
            state = CreateState(stateName, outerStateName, null);
            return this;
        }

        public StateMachine<TState, TTransition, TSignal> CreateState(
            TState stateName, TState outerStateName, int? orthogonalIndex = null,
            Action<State<TState, TTransition, TSignal>> onCreated = null)
        {
            var state = CreateState(stateName, outerStateName, orthogonalIndex);
            onCreated?.Invoke(state);

            return this;
        }

        public StateMachine<TState, TTransition, TSignal> Create(
            TState stateName, TState outerStateName, int? orthogonalIndex = null,
            Action<State<TState, TTransition, TSignal>> onCreated = null)
        {
            var state = CreateState(stateName, outerStateName, orthogonalIndex);
            onCreated?.Invoke(state);

            return this;
        }

        public StateMachine<TState, TTransition, TSignal> Create(
            TState stateName, out State<TState, TTransition, TSignal> state,
            TState outerStateName, int? orthogonalIndex = null,
            Action<State<TState, TTransition, TSignal>> onCreated = null)
        {
            state = CreateState(stateName, outerStateName, orthogonalIndex);
            onCreated?.Invoke(state);

            return this;
        }

        public StateMachine<TState, TTransition, TSignal> Create(
            TState stateName, out State<TState, TTransition, TSignal> state,
            State<TState, TTransition, TSignal> outerState)
        {
            state = CreateState(stateName, outerState, null);
            return this;
        }

        public StateMachine<TState, TTransition, TSignal> Create(
            TState stateName, State<TState, TTransition, TSignal> outerState, int? orthogonalIndex = null,
            Action<State<TState, TTransition, TSignal>> onCreated = null)
        {
            var state = CreateState(stateName, outerState, orthogonalIndex);
            onCreated?.Invoke(state);

            return this;
        }

        public StateMachine<TState, TTransition, TSignal> Create(
            TState stateName, out State<TState, TTransition, TSignal> state,
            State<TState, TTransition, TSignal> outerState, int? orthogonalIndex = null,
            Action<State<TState, TTransition, TSignal>> onCreated = null)
        {
            state = CreateState(stateName, outerState, orthogonalIndex);
            onCreated?.Invoke(state);

            return this;
        }

        public StateMachine<TState, TTransition, TSignal> Create(
            TTransition transitionName, TState startStateName, TState endStateName,
            out Transition<TState, TTransition, TSignal> transition)
        {
            transition = CreateTransition(transitionName, startStateName, endStateName);
            return this;
        }

        public StateMachine<TState, TTransition, TSignal> CreateTransition(
            TTransition transitionName, TState startStateName, TState endStateName,
            Action<Transition<TState, TTransition, TSignal>> onCreated = null)
        {
            var transition = CreateTransition(transitionName, startStateName, endStateName);
            onCreated?.Invoke(transition);

            return this;
        }

        public StateMachine<TState, TTransition, TSignal> Create(
            TTransition transitionName, TState startStateName, TState endStateName,
            Action<Transition<TState, TTransition, TSignal>> onCreated = null)
        {
            var transition = CreateTransition(transitionName, startStateName, endStateName);
            onCreated?.Invoke(transition);

            return this;
        }

        public StateMachine<TState, TTransition, TSignal> Create(
            TTransition transitionName, TState startStateName, TState endStateName,
            out Transition<TState, TTransition, TSignal> transition,
            Action<Transition<TState, TTransition, TSignal>> onCreated = null)
        {
            transition = CreateTransition(transitionName, startStateName, endStateName);
            onCreated?.Invoke(transition);

            return this;
        }

        public StateMachine<TState, TTransition, TSignal> Create(
            TTransition transitionName, State<TState, TTransition, TSignal> startState,
            State<TState, TTransition, TSignal> endState,
            out Transition<TState, TTransition, TSignal> transition)
        {
            transition = CreateTransition(transitionName, startState, endState);
            return this;
        }

        public StateMachine<TState, TTransition, TSignal> Create(
            TTransition transitionName, State<TState, TTransition, TSignal> startState,
            State<TState, TTransition, TSignal> endState,
            Action<Transition<TState, TTransition, TSignal>> onCreated = null)
        {
            var transition = CreateTransition(transitionName, startState, endState);
            onCreated?.Invoke(transition);

            return this;
        }

        public StateMachine<TState, TTransition, TSignal> Create(
            TTransition transitionName, State<TState, TTransition, TSignal> startState,
            State<TState, TTransition, TSignal> endState, out Transition<TState, TTransition, TSignal> transition,
            Action<Transition<TState, TTransition, TSignal>> onCreated = null)
        {
            transition = CreateTransition(transitionName, startState, endState);
            onCreated?.Invoke(transition);

            return this;
        }

        public StateMachine<TState, TTransition, TSignal> Create(
            TSignal signalName, TTransition transitionName, out Signal<TState, TTransition, TSignal> signal)
        {
            ConnectSignal(signalName, transitionName, out signal);
            return this;
        }

        public StateMachine<TState, TTransition, TSignal> CreateSignal(
            TSignal signalName, TTransition transitionName,
            Action<Signal<TState, TTransition, TSignal>> onCreated = null)
        {
            ConnectSignal(signalName, transitionName, out Signal<TState, TTransition, TSignal> signal);
            onCreated?.Invoke(signal);

            return this;
        }

        public StateMachine<TState, TTransition, TSignal> Create(
            TSignal signalName, TTransition transitionName,
            Action<Signal<TState, TTransition, TSignal>> onCreated = null)
        {
            ConnectSignal(signalName, transitionName, out Signal<TState, TTransition, TSignal> signal);
            onCreated?.Invoke(signal);

            return this;
        }

        public StateMachine<TState, TTransition, TSignal> Create(
            TSignal signalName, TTransition transitionName,
            out Signal<TState, TTransition, TSignal> signal,
            Action<Signal<TState, TTransition, TSignal>> onCreated = null)
        {
            ConnectSignal(signalName, transitionName, out signal);
            onCreated?.Invoke(signal);

            return this;
        }

        public StateMachine<TState, TTransition, TSignal> Create(
            TSignal signalName, Transition<TState, TTransition, TSignal> transition,
            out Signal<TState, TTransition, TSignal> signal)
        {
            ConnectSignal(signalName, transition, out signal);
            return this;
        }

        public StateMachine<TState, TTransition, TSignal> Create(
            TSignal signalName, Transition<TState, TTransition, TSignal> transition,
            Action<Signal<TState, TTransition, TSignal>> onCreated = null)
        {
            ConnectSignal(signalName, transition, out Signal<TState, TTransition, TSignal> signal);
            onCreated?.Invoke(signal);

            return this;
        }

        public StateMachine<TState, TTransition, TSignal> Create(
            TSignal signalName, Transition<TState, TTransition, TSignal> transition,
            out Signal<TState, TTransition, TSignal> signal,
            Action<Signal<TState, TTransition, TSignal>> onCreated = null)
        {
            ConnectSignal(signalName, transition, out signal);
            onCreated?.Invoke(signal);

            return this;
        }

        public StateMachine<TState, TTransition, TSignal> Connect(
            TSignal signalName, TTransition transitionName)
        {
            ConnectSignal(signalName, transitionName);
            return this;
        }

        public StateMachine<TState, TTransition, TSignal> Connect(
            TSignal signalName, Transition<TState, TTransition, TSignal> transition)
        {
            ConnectSignal(signalName, transition);
            return this;
        }

        public StateMachine<TState, TTransition, TSignal> Connect(
            Signal<TState, TTransition, TSignal> signal, TTransition transitionName)
        {
            ConnectSignal(signal, transitionName);
            return this;
        }

        public StateMachine<TState, TTransition, TSignal> Connect(
            Signal<TState, TTransition, TSignal> signal, Transition<TState, TTransition, TSignal> transition)
        {
            ConnectSignal(signal, transition);
            return this;
        }

        public StateMachine<TState, TTransition, TSignal> Initial(
            TState stateName)
        {
            SetInitialState(stateName);
            return this;
        }

        public StateMachine<TState, TTransition, TSignal> Initial(
            State<TState, TTransition, TSignal> state)
        {
            SetInitialState(state);
            return this;
        }

        public StateMachine<TState, TTransition, TSignal> Initial(
            TState stateName, TState outerStateName, int? orthogonalIndex = null)
        {
            SetInitialState(stateName, outerStateName, orthogonalIndex);
            return this;
        }

        public StateMachine<TState, TTransition, TSignal> Initial(
            TState stateName, State<TState, TTransition, TSignal> outerState, int? orthogonalIndex = null)
        {
            SetInitialState(stateName, outerState, orthogonalIndex);
            return this;
        }

        public StateMachine<TState, TTransition, TSignal> Initial(
            State<TState, TTransition, TSignal> state,
            State<TState, TTransition, TSignal> outerState, int? orthogonalIndex = null)
        {
            SetInitialState(state, outerState, orthogonalIndex);
            return this;
        }

        public StateMachine<TState, TTransition, TSignal> On(
            IStateMachineAction action)
        {
            AddAction(action);
            return this;
        }

        public StateMachine<TState, TTransition, TSignal> On<T>()
            where T : IStateMachineAction, new()
        {
            AddAction(new T());
            return this;
        }

        public StateMachine<TState, TTransition, TSignal> OnInitialize(
            Action<IStateMachineAction> action)
        {
            if (action == null)
                return this;

            AddAction(new StateMachineActionInitialize( action));
            return this;
        }

        public StateMachine<TState, TTransition, TSignal> OnTick(
            Action<IStateMachineAction> action)
        {
            if (action == null)
                return this;

            AddAction(new StateMachineActionTick(action));
            return this;
        }

        public StateMachine<TState, TTransition, TSignal> OnTerminate(
            Action<IStateMachineAction> action)
        {
            if (action == null)
                return this;

            AddAction(new StateMachineActionTerminate(action));
            return this;
        }

        public StateMachine<TState, TTransition, TSignal> OnStateChange(
            Action<IStateMachineAction, IState, IState> action)
        {
            if (action == null)
                return this;

            AddAction(new StateMachineActionStateChange(action));
            return this;
        }

        public StateMachine<TState, TTransition, TSignal> OnStateChange(
            Action<IStateMachineAction, IState<TState>, IState<TState>> action)
        {
            if (action == null)
                return this;

            AddAction(new StateMachineActionStateChange<TState>( action));
            return this;
        }

        public StateMachine<TState, TTransition, TSignal> OnStateChange(
            Action<IStateMachineAction, State<TState, TTransition, TSignal>, State<TState, TTransition, TSignal>> action)
        {
            if (action == null)
                return this;

            AddAction(new StateMachineActionStateChange<TState, TTransition, TSignal>(action));
            return this;
        }
    }
}
