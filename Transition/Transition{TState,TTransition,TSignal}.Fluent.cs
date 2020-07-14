using System;

namespace QuaStateMachine
{
    public sealed partial class Transition<TState, TTransition, TSignal>
    {
        public StateMachine<TState, TTransition, TSignal> End()
        {
            return this.Machine;
        }

        public Transition<TState, TTransition, TSignal> Signal(
            TSignal signalName, out Signal<TState, TTransition, TSignal> signal)
        {
            this.Machine.ConnectSignal(signalName, this, out signal);
            return this;
        }

        public Transition<TState, TTransition, TSignal> Signal(
            TSignal signalName,
            Action<Signal<TState, TTransition, TSignal>> callback)
        {
            this.Machine.ConnectSignal(signalName, this, out Signal<TState, TTransition, TSignal> signal);
            callback?.Invoke(signal);

            return this;
        }

        public Transition<TState, TTransition, TSignal> Signal(
            TSignal signalName, out Signal<TState, TTransition, TSignal> signal,
            Action<Signal<TState, TTransition, TSignal>> callback)
        {
            this.Machine.ConnectSignal(signalName, this, out signal);
            callback?.Invoke(signal);

            return this;
        }

        public Transition<TState, TTransition, TSignal> StartWhen(
            ITransitionCondition condition)
        {
            AddStartCondition(condition);
            return this;
        }

        public Transition<TState, TTransition, TSignal> StartWhen<T>()
            where T : ITransitionCondition, new()
        {
            AddStartCondition(new T());
            return this;
        }

        public Transition<TState, TTransition, TSignal> StartWhen(
            Func<bool> callback)
        {
            AddStartCondition(new TransitionCondition(callback));
            return this;
        }

        public Transition<TState, TTransition, TSignal> FinishWhen(
            ITransitionCondition condition)
        {
            AddFinishCondition(condition);
            return this;
        }

        public Transition<TState, TTransition, TSignal> FinishWhen<T>()
            where T : ITransitionCondition, new()
        {
            AddFinishCondition(new T());
            return this;
        }

        public Transition<TState, TTransition, TSignal> FinishWhen(
            Func<bool> callback)
        {
            AddFinishCondition(new TransitionCondition(callback));
            return this;
        }

        public Transition<TState, TTransition, TSignal> On(
            ITransitionAction action)
        {
            AddAction(action);
            return this;
        }

        public Transition<TState, TTransition, TSignal> On<T>()
            where T : ITransitionAction, new()
        {
            AddAction(new T());
            return this;
        }

        public Transition<TState, TTransition, TSignal> OnInvoke(
            Action<ITransitionAction, TransitionArgs> action)
        {
            if (action == null)
                return this;

            AddAction(new TransitionActionInvoke(action));
            return this;
        }

        public Transition<TState, TTransition, TSignal> OnStart(
            Action<ITransitionAction, TransitionArgs> action)
        {
            if (action == null)
                return this;

            AddAction(new TransitionActionStart(action));
            return this;
        }

        public Transition<TState, TTransition, TSignal> OnTick(
            Action<ITransitionAction> action)
        {
            if (action == null)
                return this;

            AddAction(new TransitionActionTick(action));
            return this;
        }

        public Transition<TState, TTransition, TSignal> OnFinish(
            Action<ITransitionAction> action)
        {
            if (action == null)
                return this;

            AddAction(new TransitionActionFinish(action));
            return this;
        }
    }
}
