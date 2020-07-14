using System.Collections.Generic;

namespace QuaStateMachine
{
    public interface ISignal
    {
        object Name { get; }

        IStateMachine Machine { get; }

        IReadOnlyList<ITransition> SignalTo { get; }

        IReadOnlyList<ISignalCondition> EmitConditions { get; }

        IReadOnlyDictionary<ISignalCondition, ITransition> TransitionConditions { get; }

        IReadOnlyList<ISignalAction> Actions { get; }

        bool AddAction(ISignalAction action);

        void Emit();
    }

    public interface ISignal<T> : ISignal
    {
        new T Name { get; }
    }

    public interface ISignal<TState, TTransition, TSignal> : ISignal<TSignal>
    {
        new StateMachine<TState, TTransition, TSignal> Machine { get; }

        new IReadOnlyList<Transition<TState, TTransition, TSignal>> SignalTo { get; }

        new IReadOnlyDictionary<ISignalCondition, Transition<TState, TTransition, TSignal>> TransitionConditions { get; }
    }
}