using System.Collections.Generic;

namespace QuaStateMachine
{
    public interface IStateMachine
    {
        bool Initialized { get; }

        IState InitialState { get; }

        IState CurrentState { get; }

        ITransition CurrentTransition { get; }

        IReadOnlyList<IState> States { get; }

        IReadOnlyList<ISignal> Signals { get; }

        IReadOnlyList<ITransition> Transitions { get; }

        IReadOnlyList<IStateMachineAction> Actions { get; }

        bool AddAction(IStateMachineAction action);

        List<string> GetAllActiveStateNamesAsString();

        List<IState> GetAllActiveStates();

        void Initialize();

        void Terminate();

        void EmitSignal(object signalName);
    }
}