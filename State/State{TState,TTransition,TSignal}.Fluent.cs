using System;

namespace FluentQuaStateMachine
{
    public sealed partial class State<TState, TTransition, TSignal>
    {
        public StateMachine<TState, TTransition, TSignal> End()
        {
            return this.Machine;
        }

        public OrthogonalMachine<TState, TTransition, TSignal> BeginOrthogonal(
            int? index = null)
        {
            var indexVal = index ?? DefaultOrthogonalIndex;
            var machine = this.OrthogonalsI[indexVal];

            return new OrthogonalMachine<TState, TTransition, TSignal>(machine,
                new OrthogonalState<TState, TTransition, TSignal>((Orthogonal<TState, TTransition, TSignal>)this.Machine, this));
        }

        public State<TState, TTransition, TSignal> On(
            IStateAction action)
        {
            AddAction(action);
            return this;
        }

        public State<TState, TTransition, TSignal> On<T>()
            where T : IStateAction, new()
        {
            AddAction(new T());
            return this;
        }

        public State<TState, TTransition, TSignal> OnEnter(
            Action<IStateAction> action)
        {
            if (action == null)
                return this;

            AddAction(new StateActionEnter(action));
            return this;
        }

        public State<TState, TTransition, TSignal> OnResume(
            Action<IStateAction> action)
        {
            if (action == null)
                return this;

            AddAction(new StateActionResume(action));
            return this;
        }

        public State<TState, TTransition, TSignal> OnEnterComplete(
            Action<IStateAction> action)
        {
            if (action == null)
                return this;

            AddAction(new StateActionEnterComplete(action));
            return this;
        }

        public State<TState, TTransition, TSignal> OnExit(
            Action<IStateAction> action)
        {
            if (action == null)
                return this;

            AddAction(new StateActionExit(action));
            return this;
        }

        public State<TState, TTransition, TSignal> OnTick(
            Action<IStateAction> action)
        {
            if (action == null)
                return this;

            AddAction(new StateActionTick(action));
            return this;
        }

        public State<TState, TTransition, TSignal> OnTerminate(
            Action<IStateAction> action)
        {
            if (action == null)
                return this;

            AddAction(new StateActionTerminate(action));
            return this;
        }
    }
}