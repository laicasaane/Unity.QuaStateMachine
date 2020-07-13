using System.Collections.Generic;

namespace FluentQuaStateMachine
{
    public interface ITransition
    {
        object Name { get; }

        IStateMachine Machine { get; }

        IReadOnlyList<ISignal> Signals { get; }

        IState StartState { get; }

        IState EndState { get; }

        bool CanTransition { get; }

        IReadOnlyList<ITransitionAction> Actions { get; }

        IReadOnlyList<ITransitionCondition> StartConditions { get; }

        IReadOnlyList<ITransitionCondition> FinishConditions { get; }

        bool AddAction(ITransitionAction action);

        bool AddStartCondition(ITransitionCondition condition);

        bool AddFinishCondition(ITransitionCondition condition);

        void Terminate();
    }

    public interface ITransition<T> : ITransition
    {
        new T Name { get; }
    }

    public interface ITransition<TState, TTransition, TSignal> : ITransition<TTransition>
    {
        new StateMachine<TState, TTransition, TSignal> Machine { get; }

        new IReadOnlyList<Signal<TState, TTransition, TSignal>> Signals { get; }

        new State<TState, TTransition, TSignal> StartState { get; }

        new State<TState, TTransition, TSignal> EndState { get; }
    }
}