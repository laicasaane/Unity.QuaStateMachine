using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace QuaStateMachine
{
    public abstract class Signal<T> : ISignal<T>, IEquatable<Signal<T>>
    {
        public T Name { get; }

        object ISignal.Name
            => this.Name;

        T ISignal<T>.Name
            => this.Name;

        IStateMachine ISignal.Machine
            => GetMachine();

        IReadOnlyList<ITransition> ISignal.SignalTo
            => GetSignalTo();

        public IReadOnlyList<ISignalCondition> EmitConditions
            => GetEmitConditions();

        IReadOnlyDictionary<ISignalCondition, ITransition> ISignal.TransitionConditions
            => GetTransitionConditions();

        public abstract IReadOnlyList<ISignalAction> Actions { get; }

        protected Signal(T name)
        {
            this.Name = name;
        }

        public override bool Equals(object obj)
            => obj is Signal<T> other && Equals(this.Name, other.Name);

        public bool Equals(Signal<T> other)
            => other != null && Equals(this.Name, other.Name);

        public bool Equals(T other)
            => other != null && Equals(this.Name, other);

        public override int GetHashCode()
            => this.Name.GetHashCode();

        public abstract bool AddAction(ISignalAction action);

        public abstract void Emit();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract IStateMachine GetMachine();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract IReadOnlyList<ITransition> GetSignalTo();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract IReadOnlyList<ISignalCondition> GetEmitConditions();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract IReadOnlyDictionary<ISignalCondition, ITransition> GetTransitionConditions();
    }
}
