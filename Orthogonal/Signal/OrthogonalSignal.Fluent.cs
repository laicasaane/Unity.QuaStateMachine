using System;

namespace QuaStateMachine
{
    public readonly partial struct OrthogonalSignal<TState, TTransition, TSignal>
    {
        public OrthogonalMachine<TState, TTransition, TSignal> End()
        {
            return new OrthogonalMachine<TState, TTransition, TSignal>(this.Machine,
                new OrthogonalState<TState, TTransition, TSignal>(this.OuterState.Machine, this.OuterState.State));
        }

        public OrthogonalSignal<TState, TTransition, TSignal> EmitWhen(
            params TState[] conditionalStateNames)
        {
            this.Signal.EmitWhen(conditionalStateNames);
            return this;
        }

        public OrthogonalSignal<TState, TTransition, TSignal> EmitWhen(
            params State<TState, TTransition, TSignal>[] conditionalStates)
        {
            this.Signal.EmitWhen(conditionalStates);
            return this;
        }

        public OrthogonalSignal<TState, TTransition, TSignal> TransitionWhen(
            TTransition transitionName, params TState[] conditionalStateNames)
        {
            this.Signal.TransitionWhen(transitionName, conditionalStateNames);
            return this;
        }

        public OrthogonalSignal<TState, TTransition, TSignal> TransitionWhen(
            TTransition transitionName, params State<TState, TTransition, TSignal>[] conditionalStates)
        {
            this.Signal.TransitionWhen(transitionName, conditionalStates);
            return this;
        }

        public OrthogonalSignal<TState, TTransition, TSignal> TransitionWhen(
            Transition<TState, TTransition, TSignal> transition, params TState[] conditionalStateNames)
        {
            this.Signal.TransitionWhen(transition, conditionalStateNames);
            return this;
        }

        public OrthogonalSignal<TState, TTransition, TSignal> TransitionWhen(
            Transition<TState, TTransition, TSignal> transition, params State<TState, TTransition, TSignal>[] conditionalStates)
        {
            this.Signal.TransitionWhen(transition, conditionalStates);
            return this;
        }

        public OrthogonalSignal<TState, TTransition, TSignal> On(
            ISignalAction action)
        {
            this.Signal.On(action);
            return this;
        }

        public OrthogonalSignal<TState, TTransition, TSignal> On<T>()
            where T : ISignalAction, new()
        {
            this.Signal.On<T>();
            return this;
        }

        public OrthogonalSignal<TState, TTransition, TSignal> OnEmit(
            Action<ISignalAction> action)
        {
            this.Signal.OnEmit(action);
            return this;
        }

        public OrthogonalSignal<TState, TTransition, TSignal> OnProcess(
            Action<ISignalAction> action)
        {
            this.Signal.OnProcess(action);
            return this;
        }

        public OrthogonalSignal<TState, TTransition, TSignal> OnNotProcess(
            Action<ISignalAction, SignalNotProcessedArgs> action)
        {
            this.Signal.OnNotProcess(action);
            return this;
        }
    }
}