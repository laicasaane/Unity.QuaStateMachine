using System.Collections.Generic;

namespace QuaStateMachine
{
    public interface IStateMachine<TState, TTransition, TSignal> : IStateMachine
    {
        new State<TState, TTransition, TSignal> CurrentState { get; }

        new State<TState, TTransition, TSignal> InitialState { get; }

        new Transition<TState, TTransition, TSignal> CurrentTransition { get; }

        IReadOnlyDictionary<TState, State<TState, TTransition, TSignal>> StateMap { get; }

        IReadOnlyDictionary<TSignal, Signal<TState, TTransition, TSignal>> SignalMap { get; }

        IReadOnlyDictionary<TTransition, Transition<TState, TTransition, TSignal>> TransitionMap { get; }

        bool ConnectSignal(Signal<TState, TTransition, TSignal> signal, Transition<TState, TTransition, TSignal> transition);

        bool ConnectSignal(Signal<TState, TTransition, TSignal> signal, TTransition transitionName);

        bool ConnectSignal(TSignal signalName, Transition<TState, TTransition, TSignal> transition);

        bool ConnectSignal(TSignal signalName, Transition<TState, TTransition, TSignal> transition, out Signal<TState, TTransition, TSignal> signal);

        bool ConnectSignal(TSignal signalName, TTransition transitionName);

        bool ConnectSignal(TSignal signalName, TTransition transitionName, out Signal<TState, TTransition, TSignal> signal);

        bool CreateEmitCondition(Signal<TState, TTransition, TSignal> signal, params State<TState, TTransition, TSignal>[] conditionalStates);

        bool CreateEmitCondition(Signal<TState, TTransition, TSignal> signal, params TState[] conditionalStateNames);

        bool CreateEmitCondition(TSignal signalName, params TState[] conditionalStateNames);

        Signal<TState, TTransition, TSignal> CreateSignal(TSignal signalName, Transition<TState, TTransition, TSignal> transition);

        State<TState, TTransition, TSignal> CreateState(TState stateName);

        State<TState, TTransition, TSignal> CreateState(TState stateName, State<TState, TTransition, TSignal> outerState, int? orthogonalIndex = null);

        State<TState, TTransition, TSignal> CreateState(TState stateName, TState outerStateName, int? orthogonalIndex = null);

        Transition<TState, TTransition, TSignal> CreateTransition(TTransition transitionName, State<TState, TTransition, TSignal> startState, State<TState, TTransition, TSignal> endState);

        Transition<TState, TTransition, TSignal> CreateTransition(TTransition transitionName, TState startStateName, TState endStateName);

        bool CreateTransitionCondition(Signal<TState, TTransition, TSignal> signal, Transition<TState, TTransition, TSignal> transition, params State<TState, TTransition, TSignal>[] conditionalStates);

        bool CreateTransitionCondition(Signal<TState, TTransition, TSignal> signal, Transition<TState, TTransition, TSignal> transition, params TState[] conditionalStateNames);

        bool CreateTransitionCondition(Signal<TState, TTransition, TSignal> signal, TTransition transitionName, params State<TState, TTransition, TSignal>[] conditionalStates);

        bool CreateTransitionCondition(Signal<TState, TTransition, TSignal> signal, TTransition transitionName, params TState[] conditionalStateNames);

        bool CreateTransitionCondition(TSignal signalName, TTransition transitionName, params TState[] conditionalStateNames);

        List<TState> GetAllActiveStateNames();

        new List<string> GetAllActiveStateNamesAsString();

        new List<State<TState, TTransition, TSignal>> GetAllActiveStates();

        Signal<TState, TTransition, TSignal> GetSignalByName(TSignal signalName);

        State<TState, TTransition, TSignal> GetStateByName(TState stateName);

        Transition<TState, TTransition, TSignal> GetTransitionByName(TTransition transitionName);

        bool SetInitialState(State<TState, TTransition, TSignal> state);

        bool SetInitialState(State<TState, TTransition, TSignal> state, State<TState, TTransition, TSignal> outerState, int? orthogonalIndex = null);

        bool SetInitialState(TState stateName);

        bool SetInitialState(TState stateName, State<TState, TTransition, TSignal> outerState, int? orthogonalIndex = null);

        bool SetInitialState(TState stateName, TState outerStateName, int? orthogonalIndex = null);

        bool TryCreateState(TState stateName, out State<TState, TTransition, TSignal> state);

        bool TryCreateState(TState stateName, out State<TState, TTransition, TSignal> state, State<TState, TTransition, TSignal> outerState, int? orthogonalIndex = null);

        bool TryCreateState(TState stateName, out State<TState, TTransition, TSignal> state, TState outerStateName, int? orthogonalIndex = null);

        bool TryCreateTransition(TTransition transitionName, State<TState, TTransition, TSignal> startState, State<TState, TTransition, TSignal> endState, out Transition<TState, TTransition, TSignal> transition);

        bool TryCreateTransition(TTransition transitionName, TState startStateName, TState endStateName, out Transition<TState, TTransition, TSignal> transition);

        void EmitSignal(TSignal signalName);
    }
}