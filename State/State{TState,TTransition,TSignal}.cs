using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace QuaStateMachine
{
    public sealed partial class State<TState, TTransition, TSignal>
        : State<TState>, IState<TState, TTransition, TSignal>,
          IFixedTickable, IPostFixedTickable,
          ITickable, IPostTickable,
          ILateTickable, IPostLateTickable
    {
        public State<TState, TTransition, TSignal> OuterState { get; internal set; }

        public StateMachine<TState, TTransition, TSignal> Machine
            => this.MachineI;

        public IReadOnlyList<Transition<TState, TTransition, TSignal>> Transitions
            => this.TransitionsI.Keys.ToList();

        public override IReadOnlyList<IStateAction> Actions
            => this.actions;

        public IReadOnlyDictionary<int, Orthogonal<TState, TTransition, TSignal>> Orthogonals
            => this.OrthogonalsI;

        /// <summary>
        /// Internal <see cref="Machine"/>
        /// </summary>
        internal StateMachine<TState, TTransition, TSignal> MachineI { get; private set; }

        /// <summary>
        /// Internal <see cref="Transitions"/>
        /// </summary>
        internal Dictionary<Transition<TState, TTransition, TSignal>, State<TState, TTransition, TSignal>> TransitionsI { get; }

        /// <summary>
        /// Internal <see cref="Orthogonals"/>
        /// </summary>
        internal Dictionary<int, Orthogonal<TState, TTransition, TSignal>> OrthogonalsI { get; }

        private readonly StateActionList actions;
        private readonly TickableList tickableActions;

        private readonly IdleStateStatus idleStateStatus;
        private readonly CurrentStateStatus currentStateStatus;

        private Status status;

        internal State(TState name) : base(name)
        {
            this.TransitionsI = new Dictionary<Transition<TState, TTransition, TSignal>, State<TState, TTransition, TSignal>>();

            this.OrthogonalsI = new Dictionary<int, Orthogonal<TState, TTransition, TSignal>> {
                { DefaultOrthogonalIndex, new Orthogonal<TState, TTransition, TSignal>(this, DefaultOrthogonalIndex) }
            };

            this.actions = new StateActionList();
            this.tickableActions = new TickableList();

            this.idleStateStatus = new IdleStateStatus(this);
            this.currentStateStatus = new CurrentStateStatus(this);

            this.status = this.idleStateStatus;
        }

        public override bool AddAction(IStateAction action)
        {
            if (action == null || this.actions.Contains(action))
                return false;

            switch (action)
            {
                case IStateAction<TState, TTransition, TSignal> actionSTS:
                    actionSTS.State = this;
                    break;

                case IStateAction<TState> actionT:
                    actionT.State = this;
                    break;

                default:
                    action.State = this;
                    break;
            }

            this.actions.Add(action);
            this.tickableActions.Add(action);
            return true;
        }

        internal bool AddTransition(Transition<TState, TTransition, TSignal> transition, State<TState, TTransition, TSignal> state)
        {
            if (this.TransitionsI.ContainsKey(transition))
            {
                return false;
            }

            this.TransitionsI.Add(transition, state);
            return true;
        }

        internal bool AddInnerState(State<TState, TTransition, TSignal> state, int? index = null)
        {
            var indexVal = index ?? DefaultOrthogonalIndex;

            if (!this.OrthogonalsI.ContainsKey(indexVal))
            {
                if (!AddOrthogonal(indexVal))
                {
                    return false;
                }
            }

            state.HasOuterState = true;
            state.OuterState = this;

            return this.OrthogonalsI[indexVal].Machine.AddState(state);
        }

        internal bool SetInitialInnerState(State<TState, TTransition, TSignal> state, int? index = null)
        {
            if (this.OrthogonalsI.Count <= 0)
            {
                return false;
            }

            var indexVal = index ?? DefaultOrthogonalIndex;

            if (!this.OrthogonalsI.ContainsKey(indexVal))
            {
                if (!AddOrthogonal(indexVal))
                {
                    return false;
                }
            }

            return this.OrthogonalsI[indexVal].Machine.SetInitialState(state);
        }

        internal bool SetStateMachine(StateMachine<TState, TTransition, TSignal> machine)
        {
            if (this.MachineI != null || machine == null)
                return false;

            this.MachineI = machine;

            foreach (var orthogonal in this.OrthogonalsI.Values)
            {
                orthogonal.ConnectStateChangedEvent();
            }

            return true;
        }

        internal bool AddOrthogonal(int index)
        {
            if (!this.OrthogonalsI.ContainsKey(index))
            {
                this.OrthogonalsI.Add(index, new Orthogonal<TState, TTransition, TSignal>(this, index));
                return true;
            }

            return false;
        }

        internal void Enter(State<TState, TTransition, TSignal> previous)
        {
            this.actions.Enter(previous);

            foreach (var orthogonal in this.OrthogonalsI.Values)
            {
                orthogonal.Machine.Initialize();
            }
        }

        internal void Resume(State<TState, TTransition, TSignal> next)
        {
            this.actions.Resume(next);
        }

        internal void EnterComplete(State<TState, TTransition, TSignal> previous)
        {
            this.IsCurrentState = true;
            this.status = this.currentStateStatus;
            this.actions.EnterComplete(previous);
        }

        internal void Exit(State<TState, TTransition, TSignal> next)
        {
            this.IsCurrentState = false;
            this.status = this.idleStateStatus;
            this.actions.Exit(next);

            foreach (var orthogonal in this.OrthogonalsI.Values)
            {
                orthogonal.Machine.Terminate();
            }
        }

        internal void Terminate()
        {
            foreach (var orthogonal in this.OrthogonalsI.Values)
            {
                orthogonal.Machine.Terminate();
            }

            this.actions.Terminate();
            this.IsCurrentState = false;
            this.status = this.idleStateStatus;
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

        internal void PassSignal(Signal<TState, TTransition, TSignal> signal)
        {
            foreach (var orthogonal in this.OrthogonalsI.Values)
            {
                orthogonal.Machine.ProcessSignal(signal);
            }
        }

        public override string ToString()
        {
            return this.Name.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override IStateMachine GetMachine()
            => this.MachineI;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override IReadOnlyList<ITransition> GetTransitions()
            => this.TransitionsI.Keys.ToList();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override IReadOnlyDictionary<int, IOrthogonal> GetOrthogonalMachines()
            => this.OrthogonalsI.ToDictionary(x => x.Key, x => x.Value as IOrthogonal);

        private abstract class Status :
            IFixedTickable, IPostFixedTickable,
            ITickable, IPostTickable,
            ILateTickable, IPostLateTickable
        {
            protected readonly State<TState, TTransition, TSignal> state;

            public Status(State<TState, TTransition, TSignal> state)
            {
                this.state = state;
            }

            public abstract void FixedTick();

            public abstract void PostFixedTick();

            public abstract void Tick();

            public abstract void PostTick();

            public abstract void LateTick();

            public abstract void PostLateTick();
        }

        private sealed class IdleStateStatus : Status
        {
            public IdleStateStatus(State<TState, TTransition, TSignal> state) : base(state) { }

            public override void FixedTick() { }

            public override void PostFixedTick() { }

            public override void Tick() { }

            public override void PostTick() { }

            public override void LateTick() { }

            public override void PostLateTick() { }
        }

        private sealed class CurrentStateStatus : Status
        {
            public CurrentStateStatus(State<TState, TTransition, TSignal> state) : base(state) { }

            public override void FixedTick()
            {
                this.state.tickableActions.FixedTickables.FixedTick();

                foreach (var orthogonal in this.state.OrthogonalsI.Values)
                {
                    orthogonal.Machine.FixedTick();
                }
            }

            public override void PostFixedTick()
            {
                this.state.tickableActions.PostFixedTickables.PostFixedTick();

                foreach (var orthogonal in this.state.OrthogonalsI.Values)
                {
                    orthogonal.Machine.PostFixedTick();
                }
            }

            public override void Tick()
            {
                this.state.tickableActions.Tickables.Tick();

                foreach (var orthogonal in this.state.OrthogonalsI.Values)
                {
                    orthogonal.Machine.Tick();
                }
            }

            public override void PostTick()
            {
                this.state.tickableActions.PostTickables.PostTick();

                foreach (var orthogonal in this.state.OrthogonalsI.Values)
                {
                    orthogonal.Machine.PostTick();
                }
            }

            public override void LateTick()
            {
                this.state.tickableActions.LateTickables.LateTick();

                foreach (var orthogonal in this.state.OrthogonalsI.Values)
                {
                    orthogonal.Machine.LateTick();
                }
            }

            public override void PostLateTick()
            {
                this.state.tickableActions.PostLateTickables.PostLateTick();

                foreach (var orthogonal in this.state.OrthogonalsI.Values)
                {
                    orthogonal.Machine.PostLateTick();
                }
            }
        }
    }
}
