using System.Collections.Generic;

namespace FluentQuaStateMachine
{
    public interface IState
    {
        object Name { get; }

        IStateMachine Machine { get; }

        IReadOnlyList<ITransition> Transitions { get; }

        IReadOnlyDictionary<int, IOrthogonal> Orthogonals { get; }

        bool IsCurrentState { get; }

        bool HasOuterState { get; }

        IReadOnlyList<IStateAction> Actions { get; }

        bool AddAction(IStateAction action);
    }

    public interface IState<T> : IState
    {
        new T Name { get; }
    }

    public interface IState<TState, TTransition, TSignal> : IState<TState>
    {
        new StateMachine<TState, TTransition, TSignal> Machine { get; }

        new IReadOnlyList<Transition<TState, TTransition, TSignal>> Transitions { get; }

        new IReadOnlyDictionary<int, Orthogonal<TState, TTransition, TSignal>> Orthogonals { get; }
    }
}