using System;
using System.Runtime.CompilerServices;

namespace QuaStateMachine
{
    public readonly partial struct OrthogonalState<TState, TTransition, TSignal>
    {
        public State<TState, TTransition, TSignal> ReturnState()
        {
            return this.State;
        }

        public OrthogonalMachine<TState, TTransition, TSignal> End()
        {
            return new OrthogonalMachine<TState, TTransition, TSignal>(this.Machine,
                new OrthogonalState<TState, TTransition, TSignal>(this.Outer.Machine, this.State.OuterState));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public OrthogonalMachine<TState, TTransition, TSignal> BeginOrthogonal(int? index = null)
        {
            var indexVal = index ?? State<TState, TTransition, TSignal>.DefaultOrthogonalIndex;
            return new OrthogonalMachine<TState, TTransition, TSignal>(this.State.OrthogonalsI[indexVal], this);
        }

        public OrthogonalState<TState, TTransition, TSignal> On(
            IStateAction action)
        {
            this.State.On(action);
            return this;
        }

        public OrthogonalState<TState, TTransition, TSignal> On<T>()
            where T : IStateAction, new()
        {
            this.State.On<T>();
            return this;
        }

        public OrthogonalState<TState, TTransition, TSignal> OnEnter(
            Action<IStateAction> action)
        {
            this.State.OnEnter(action);
            return this;
        }

        public OrthogonalState<TState, TTransition, TSignal> OnResume(
            Action<IStateAction> action)
        {
            this.State.OnResume(action);
            return this;
        }

        public OrthogonalState<TState, TTransition, TSignal> OnEnterComplete(
            Action<IStateAction> action)
        {
            this.State.OnEnterComplete(action);
            return this;
        }

        public OrthogonalState<TState, TTransition, TSignal> OnExit(
            Action<IStateAction> action)
        {
            this.State.OnExit(action);
            return this;
        }

        public OrthogonalState<TState, TTransition, TSignal> OnTerminate(
            Action<IStateAction> action)
        {
            this.State.OnTerminate(action);
            return this;
        }

        public OrthogonalState<TState, TTransition, TSignal> OnFixedTick(
            Action<IStateAction> action)
        {
            this.State.OnFixedTick(action);
            return this;
        }

        public OrthogonalState<TState, TTransition, TSignal> OnPostFixedTick(
            Action<IStateAction> action)
        {
            this.State.OnPostFixedTick(action);
            return this;
        }

        public OrthogonalState<TState, TTransition, TSignal> OnTick(
            Action<IStateAction> action)
        {
            this.State.OnTick(action);
            return this;
        }

        public OrthogonalState<TState, TTransition, TSignal> OnPostTick(
            Action<IStateAction> action)
        {
            this.State.OnPostTick(action);
            return this;
        }

        public OrthogonalState<TState, TTransition, TSignal> OnLateTick(
            Action<IStateAction> action)
        {
            this.State.OnLateTick(action);
            return this;
        }

        public OrthogonalState<TState, TTransition, TSignal> OnPostLateTick(
            Action<IStateAction> action)
        {
            this.State.OnPostLateTick(action);
            return this;
        }
    }
}