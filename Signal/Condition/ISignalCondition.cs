using System.Collections.Generic;

namespace QuaStateMachine
{
    public interface ISignalCondition
    {
        IReadOnlyList<IState> Conditions { get; }

        bool IsValid { get; }
    }

    public interface ISignalCondition<TState> : ISignalCondition
    {
        new IReadOnlyList<IState<TState>> Conditions { get; }
    }

    public interface ISignalCondition<TState, TTransition, TSignal> : ISignalCondition<TState>
    {
        new IReadOnlyList<State<TState, TTransition, TSignal>> Conditions { get; }

        void AddCondition(params State<TState, TTransition, TSignal>[] conditions);
    }
}