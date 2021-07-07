using System;

namespace QuaStateMachine
{
    public readonly partial struct OrthogonalTransition<TState, TTransition, TSignal>
    {
        public OrthogonalMachine<TState, TTransition, TSignal> End()
        {
            return new OrthogonalMachine<TState, TTransition, TSignal>(this.Machine,
                new OrthogonalState<TState, TTransition, TSignal>(this.OuterState.Machine, this.OuterState.State));
        }

        public OrthogonalTransition<TState, TTransition, TSignal> Signal(
            TSignal signalName, out Signal<TState, TTransition, TSignal> signal)
        {
            this.Transition.Signal(signalName, out signal);
            return this;
        }

        public OrthogonalTransition<TState, TTransition, TSignal> Signal(
            TSignal signalName, Action<Signal<TState, TTransition, TSignal>> callback)
        {
            this.Transition.Signal(signalName, callback);
            return this;
        }

        public OrthogonalTransition<TState, TTransition, TSignal> Signal(
            TSignal signalName, out Signal<TState, TTransition, TSignal> signal,
            Action<Signal<TState, TTransition, TSignal>> callback)
        {
            this.Transition.Signal(signalName, out signal, callback);
            return this;
        }

        public OrthogonalTransition<TState, TTransition, TSignal> StartWhen(
            ITransitionCondition condition)
        {
            this.Transition.StartWhen(condition);
            return this;
        }

        public OrthogonalTransition<TState, TTransition, TSignal> StartWhen<T>()
            where T : ITransitionCondition, new()
        {
            this.Transition.StartWhen<T>();
            return this;
        }

        public OrthogonalTransition<TState, TTransition, TSignal> StartWhen(
            Func<bool> callback)
        {
            this.Transition.StartWhen(callback);
            return this;
        }

        public OrthogonalTransition<TState, TTransition, TSignal> FinishWhen(
            ITransitionCondition condition)
        {
            this.Transition.StartWhen(condition);
            return this;
        }

        public OrthogonalTransition<TState, TTransition, TSignal> FinishWhen<T>()
            where T : ITransitionCondition, new()
        {
            this.Transition.FinishWhen<T>();
            return this;
        }

        public OrthogonalTransition<TState, TTransition, TSignal> FinishWhen(
            Func<bool> callback)
        {
            this.Transition.FinishWhen(callback);
            return this;
        }

        public OrthogonalTransition<TState, TTransition, TSignal> On(
            ITransitionAction action)
        {
            this.Transition.On(action);
            return this;
        }

        public OrthogonalTransition<TState, TTransition, TSignal> On<T>()
            where T : ITransitionAction, new()
        {
            this.Transition.On<T>();
            return this;
        }

        public OrthogonalTransition<TState, TTransition, TSignal> OnInvoke(
            Action<ITransitionAction, TransitionArgs> action)
        {
            this.Transition.OnInvoke(action);
            return this;
        }

        public OrthogonalTransition<TState, TTransition, TSignal> OnStart(
            Action<ITransitionAction, TransitionArgs> action)
        {
            this.Transition.OnStart(action);
            return this;
        }

        public OrthogonalTransition<TState, TTransition, TSignal> OnFinish(
            Action<ITransitionAction> action)
        {
            this.Transition.OnFinish(action);
            return this;
        }

        public OrthogonalTransition<TState, TTransition, TSignal> OnFixedTick(
            Action<ITransitionAction> action)
        {
            this.Transition.OnFixedTick(action);
            return this;
        }

        public OrthogonalTransition<TState, TTransition, TSignal> OnPostFixedTick(
            Action<ITransitionAction> action)
        {
            this.Transition.OnPostFixedTick(action);
            return this;
        }

        public OrthogonalTransition<TState, TTransition, TSignal> OnTick(
            Action<ITransitionAction> action)
        {
            this.Transition.OnTick(action);
            return this;
        }

        public OrthogonalTransition<TState, TTransition, TSignal> OnPostTick(
            Action<ITransitionAction> action)
        {
            this.Transition.OnPostTick(action);
            return this;
        }

        public OrthogonalTransition<TState, TTransition, TSignal> OnLateTick(
            Action<ITransitionAction> action)
        {
            this.Transition.OnLateTick(action);
            return this;
        }

        public OrthogonalTransition<TState, TTransition, TSignal> OnPostLateTick(
            Action<ITransitionAction> action)
        {
            this.Transition.OnPostLateTick(action);
            return this;
        }
    }
}