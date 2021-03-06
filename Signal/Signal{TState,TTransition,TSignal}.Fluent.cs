﻿using System;

namespace QuaStateMachine
{
    public sealed partial class Signal<TState, TTransition, TSignal>
    {
        public StateMachine<TState, TTransition, TSignal> End()
        {
            return this.Machine;
        }

        public Signal<TState, TTransition, TSignal> EmitWhen(
            params TState[] conditionalStateNames)
        {
            this.Machine.CreateEmitCondition(this, conditionalStateNames);
            return this;
        }

        public Signal<TState, TTransition, TSignal> EmitWhen(
            params State<TState, TTransition, TSignal>[] conditionalStates)
        {
            this.Machine.CreateEmitCondition(this, conditionalStates);
            return this;
        }

        public Signal<TState, TTransition, TSignal> EmitWhen(
            ISignalCondition condition)
        {
            this.Machine.CreateEmitCondition(this, condition);
            return this;
        }

        public Signal<TState, TTransition, TSignal> EmitWhen<T>()
            where T : ISignalCondition, new()
        {
            this.Machine.CreateEmitCondition(this, new T());
            return this;
        }

        public Signal<TState, TTransition, TSignal> EmitWhen(
            Func<bool> condition)
        {
            this.Machine.CreateEmitCondition(this, new SignalCondition(condition));
            return this;
        }

        public Signal<TState, TTransition, TSignal> TransitionWhen(
            TTransition transitionName, params TState[] conditionalStateNames)
        {
            this.Machine.CreateTransitionCondition(this, transitionName, conditionalStateNames);
            return this;
        }

        public Signal<TState, TTransition, TSignal> TransitionWhen(
            TTransition transitionName, params State<TState, TTransition, TSignal>[] conditionalStates)
        {
            this.Machine.CreateTransitionCondition(this, transitionName, conditionalStates);
            return this;
        }

        public Signal<TState, TTransition, TSignal> TransitionWhen(
            Transition<TState, TTransition, TSignal> transition, params TState[] conditionalStateNames)
        {
            this.Machine.CreateTransitionCondition(this, transition, conditionalStateNames);
            return this;
        }

        public Signal<TState, TTransition, TSignal> TransitionWhen(
            Transition<TState, TTransition, TSignal> transition, params State<TState, TTransition, TSignal>[] conditionalStates)
        {
            this.Machine.CreateTransitionCondition(this, transition, conditionalStates);
            return this;
        }

        public Signal<TState, TTransition, TSignal> TransitionWhen(
            TTransition transitionName, ISignalCondition condition)
        {
            this.Machine.CreateTransitionCondition(this, transitionName, condition);
            return this;
        }

        public Signal<TState, TTransition, TSignal> TransitionWhen(
            Transition<TState, TTransition, TSignal> transition, ISignalCondition condition)
        {
            this.Machine.CreateTransitionCondition(this, transition, condition);
            return this;
        }

        public Signal<TState, TTransition, TSignal> TransitionWhen<T>(
            TTransition transitionName) where T : ISignalCondition, new()
        {
            this.Machine.CreateTransitionCondition(this, transitionName, new T());
            return this;
        }

        public Signal<TState, TTransition, TSignal> TransitionWhen<T>(
            Transition<TState, TTransition, TSignal> transition) where T : ISignalCondition, new()
        {
            this.Machine.CreateTransitionCondition(this, transition, new T());
            return this;
        }

        public Signal<TState, TTransition, TSignal> TransitionWhen(
            TTransition transitionName, Func<bool> condition)
        {
            this.Machine.CreateTransitionCondition(this, transitionName, new SignalCondition(condition));
            return this;
        }

        public Signal<TState, TTransition, TSignal> TransitionWhen(
            Transition<TState, TTransition, TSignal> transition, Func<bool> condition)
        {
            this.Machine.CreateTransitionCondition(this, transition, new SignalCondition(condition));
            return this;
        }

        public Signal<TState, TTransition, TSignal> TransitionWhen(
            TTransition transitionName, Predicate<ISignal> condition)
        {
            this.Machine.CreateTransitionCondition(this, transitionName, new ParamSignalCondition(condition));
            return this;
        }

        public Signal<TState, TTransition, TSignal> TransitionWhen(
            Transition<TState, TTransition, TSignal> transition, Predicate<ISignal> condition)
        {
            this.Machine.CreateTransitionCondition(this, transition, new ParamSignalCondition(condition));
            return this;
        }

        public Signal<TState, TTransition, TSignal> On(
            ISignalAction action)
        {
            AddAction(action);
            return this;
        }

        public Signal<TState, TTransition, TSignal> On<T>()
            where T : ISignalAction, new()
        {
            AddAction(new T());
            return this;
        }

        public Signal<TState, TTransition, TSignal> OnEmit(
            Action<ISignalAction> action)
        {
            if (action == null)
                return this;

            AddAction(new SignalActionEmit(action));
            return this;
        }

        public Signal<TState, TTransition, TSignal> OnProcess(
            Action<ISignalAction> action)
        {
            if (action == null)
                return this;

            AddAction(new SignalActionProcess(action));
            return this;
        }

        public Signal<TState, TTransition, TSignal> OnNotProcess(
            Action<ISignalAction, SignalNotProcessedArgs> action)
        {
            if (action == null)
                return this;

            AddAction(new SignalActionNotProcess(action));
            return this;
        }
    }
}
