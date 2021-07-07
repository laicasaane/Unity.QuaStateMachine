using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace QuaStateMachine
{
    public sealed partial class Transition<TState, TTransition, TSignal>
        : Transition<TTransition>, ITransition<TState, TTransition, TSignal>,
          IFixedTickable, IPostFixedTickable,
          ITickable, IPostTickable,
          ILateTickable, IPostLateTickable
    {
        public override bool CanTransition
            => this.CanTransitionI;

        public StateMachine<TState, TTransition, TSignal> Machine
            => this.MachineI;

        public IReadOnlyList<Signal<TState, TTransition, TSignal>> Signals
            => this.SignalsI;

        public State<TState, TTransition, TSignal> StartState
            => this.StartStateI;

        public State<TState, TTransition, TSignal> EndState
            => this.EndStateI;

        public override IReadOnlyList<ITransitionAction> Actions
            => this.actions;

        public override IReadOnlyList<ITransitionCondition> StartConditions
            => this.startConditions;

        public override IReadOnlyList<ITransitionCondition> FinishConditions
            => this.finishConditions;

        /// <summary>
        /// Internal <see cref="Machine"/>
        /// </summary>
        internal StateMachine<TState, TTransition, TSignal> MachineI { get; }

        /// <summary>
        /// Internal <see cref="Signals"/>
        /// </summary>
        internal List<Signal<TState, TTransition, TSignal>> SignalsI { get; }

        /// <summary>
        /// Internal <see cref="StartState"/>
        /// </summary>
        internal State<TState, TTransition, TSignal> StartStateI { get; private set; }

        /// <summary>
        /// Internal <see cref="EndState"/>
        /// </summary>
        internal State<TState, TTransition, TSignal> EndStateI { get; private set; }

        /// <summary>
        /// Internal <see cref="CanTransition"/>
        /// </summary>
        internal bool CanTransitionI { get; set; }

        private readonly TransitionActionList actions;
        private readonly TickableList tickableActions;

        private readonly TransitionConditionList startConditions;
        private readonly TransitionConditionList finishConditions;

        private readonly IdleStatus idleStatus;
        private readonly StartingStatus startingStatus;
        private readonly FinishingStatus finishingStatus;

        private Status status;
        private Signal<TState, TTransition, TSignal> signal;

        internal Transition(StateMachine<TState, TTransition, TSignal> machine, TTransition name, TickType tickType) : base(name)
        {
            this.MachineI = machine;
            this.CanTransitionI = true;
            this.SignalsI = new List<Signal<TState, TTransition, TSignal>>();

            this.actions = new TransitionActionList();
            this.tickableActions = new TickableList();

            this.startConditions = new TransitionConditionList();
            this.finishConditions = new TransitionConditionList();

            this.idleStatus = new IdleStatus(this, tickType);
            this.startingStatus = new StartingStatus(this, tickType);
            this.finishingStatus = new FinishingStatus(this, tickType);

            this.status = this.idleStatus;
            this.signal = null;
        }

        internal Transition(StateMachine<TState, TTransition, TSignal> machine, TTransition name,
                            State<TState, TTransition, TSignal> startState, State<TState, TTransition, TSignal> endState,
                            TickType tickType)
            : this(machine, name, tickType)
        {
            SetTransition(startState, endState);
        }

        public override bool AddAction(ITransitionAction action)
        {
            if (action == null || this.actions.Contains(action))
                return false;

            switch (action)
            {
                case ITransitionAction<TState, TTransition, TSignal> actionSTS:
                    actionSTS.Transition = this;
                    break;

                case ITransitionAction<TTransition> actionT:
                    actionT.Transition = this;
                    break;

                default:
                    action.Transition = this;
                    break;
            }

            this.actions.Add(action);
            this.tickableActions.Add(action);
            return true;
        }

        public override bool AddStartCondition(ITransitionCondition condition)
        {
            if (condition == null || this.startConditions.Contains(condition))
                return false;

            this.startConditions.Add(condition);
            return true;
        }

        public override bool AddFinishCondition(ITransitionCondition condition)
        {
            if (condition == null || this.finishConditions.Contains(condition))
                return false;

            this.finishConditions.Add(condition);
            return true;
        }

        public override void Terminate()
        {
            var signal = this.signal;
            this.signal = null;

            this.MachineI.TerminateTransition(this, signal);
        }

        internal bool SetTransition(State<TState, TTransition, TSignal> startState, State<TState, TTransition, TSignal> endState)
        {
            if (this.StartStateI == null && this.EndStateI == null)
            {
                this.StartStateI = startState;
                this.EndStateI = endState;
                return true;
            }

            return false;
        }

        internal bool AddSignal(Signal<TState, TTransition, TSignal> signal)
        {
            if (this.SignalsI.Exists(ts => ts.Name.Equals(signal.Name)))
                return false;

            this.SignalsI.Add(signal);
            return true;
        }

        internal bool Invoke(Signal<TState, TTransition, TSignal> signal)
        {
            var args = new TransitionArgs();
            this.actions.Invoke(args);

            if (args.CancelTransition)
                return false;

            this.signal = signal;
            this.status = this.startingStatus;
            return true;
        }

        public void FixedTick()
        {
            this.status.FixedTick();
        }

        public void PostFixedTick()
        {
            this.status.PostFixedTick();
        }

        public void Tick()
        {
            this.status.Tick();
        }

        public void PostTick()
        {
            this.status.PostTick();
        }

        public void LateTick()
        {
            this.status.LateTick();
        }

        public void PostLateTick()
        {
            this.status.PostLateTick();
        }

        internal bool Start()
        {
            var args = new TransitionArgs();
            this.actions.Start(args);

            if (args.CancelTransition)
                return false;

            return true;
        }

        internal void Finish()
        {
            this.status = this.idleStatus;
            this.signal = null;
            this.actions.Finish();
        }

        public override string ToString()
        {
            return this.Name.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override IStateMachine GetMachine()
            => this.MachineI;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override IReadOnlyList<ISignal> GetSignals()
            => this.SignalsI;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override IState GetStartState()
            => this.StartStateI;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override IState GetEndState()
            => this.EndStateI;

        private abstract class Status :
            IFixedTickable, IPostFixedTickable,
            ITickable, IPostTickable,
            ILateTickable, IPostLateTickable
        {
            protected readonly Transition<TState, TTransition, TSignal> transition;
            protected readonly TickType tickType;

            public Status(Transition<TState, TTransition, TSignal> transition, TickType tickType)
            {
                this.transition = transition;
                this.tickType = tickType;
            }

            public abstract void FixedTick();

            public abstract void PostFixedTick();

            public abstract void Tick();

            public abstract void PostTick();

            public abstract void LateTick();

            public abstract void PostLateTick();
        }

        private sealed class IdleStatus : Status
        {
            public IdleStatus(Transition<TState, TTransition, TSignal> transition, TickType tickType)
                : base(transition, tickType) { }

            public override void FixedTick() { }

            public override void PostFixedTick() { }

            public override void Tick() { }

            public override void PostTick() { }

            public override void LateTick() { }

            public override void PostLateTick() { }
        }

        private sealed class StartingStatus : Status
        {
            public StartingStatus(Transition<TState, TTransition, TSignal> transition, TickType tickType)
                : base(transition, tickType) { }

            private void StartTransition(TickType tickType)
            {
                if (this.tickType != tickType ||
                    !this.transition.startConditions.Validate(this.transition))
                    return;

                this.transition.startConditions.Invalidate(this.transition);
                this.transition.MachineI.StartTransition(this.transition, this.transition.signal);
                this.transition.status = this.transition.finishingStatus;
            }

            public override void FixedTick()
            {
                StartTransition(TickType.FixedTick);
                this.transition.tickableActions.FixedTickables.FixedTick();
            }

            public override void PostFixedTick()
            {
                StartTransition(TickType.PostFixedTick);
                this.transition.tickableActions.PostFixedTickables.PostFixedTick();
            }

            public override void Tick()
            {
                StartTransition(TickType.Tick);
                this.transition.tickableActions.Tickables.Tick();
            }

            public override void PostTick()
            {
                StartTransition(TickType.PostTick);
                this.transition.tickableActions.PostTickables.PostTick();
            }

            public override void LateTick()
            {
                StartTransition(TickType.LateTick);
                this.transition.tickableActions.LateTickables.LateTick();
            }

            public override void PostLateTick()
            {
                StartTransition(TickType.PostLateTick);
                this.transition.tickableActions.PostLateTickables.PostLateTick();
            }
        }

        private sealed class FinishingStatus : Status
        {
            public FinishingStatus(Transition<TState, TTransition, TSignal> transition, TickType tickType)
                : base(transition, tickType) { }

            private void FinishTransition(TickType tickType)
            {
                if (this.tickType != tickType ||
                    !this.transition.finishConditions.Validate(this.transition))
                    return;

                this.transition.finishConditions.Invalidate(this.transition);
                this.transition.MachineI.FinishTransition(this.transition);
            }

            public override void FixedTick()
            {
                FinishTransition(TickType.FixedTick);
                this.transition.tickableActions.FixedTickables.FixedTick();
            }

            public override void PostFixedTick()
            {
                FinishTransition(TickType.PostFixedTick);
                this.transition.tickableActions.PostFixedTickables.PostFixedTick();
            }

            public override void Tick()
            {
                FinishTransition(TickType.Tick);
                this.transition.tickableActions.Tickables.Tick();
            }

            public override void PostTick()
            {
                FinishTransition(TickType.PostTick);
                this.transition.tickableActions.PostTickables.PostTick();
            }

            public override void LateTick()
            {
                FinishTransition(TickType.LateTick);
                this.transition.tickableActions.LateTickables.LateTick();
            }

            public override void PostLateTick()
            {
                FinishTransition(TickType.PostLateTick);
                this.transition.tickableActions.PostLateTickables.PostLateTick();
            }
        }
    }
}