using System.Collections.Generic;
using System.Linq;

namespace QuaStateMachine
{
    public partial class StateMachine<TState, TTransition, TSignal>
        : IStateMachine<TState, TTransition, TSignal>
    {
        public bool Initialized { get; private set; }

        public State<TState, TTransition, TSignal> CurrentState
            => this.CurrentStateI;

        public State<TState, TTransition, TSignal> InitialState
            => this.InitialStateI;

        public Transition<TState, TTransition, TSignal> CurrentTransition
            => this.CurrentTransitionI;

        public IReadOnlyDictionary<TState, State<TState, TTransition, TSignal>> StateMap
            => this.StateMapI;

        public IReadOnlyDictionary<TSignal, Signal<TState, TTransition, TSignal>> SignalMap
            => this.SignalMapI;

        public IReadOnlyDictionary<TTransition, Transition<TState, TTransition, TSignal>> TransitionMap
            => this.TransitionMapI;

        public IReadOnlyList<State<TState, TTransition, TSignal>> States
            => this.StateMapI.Values.ToList();

        public IReadOnlyList<Signal<TState, TTransition, TSignal>> Signals
            => this.SignalMapI.Values.ToList();

        public IReadOnlyList<Transition<TState, TTransition, TSignal>> Transitions
            => this.TransitionMapI.Values.ToList();

        public IReadOnlyList<IStateMachineAction> Actions
            => this.actions;

        IState IStateMachine.InitialState
            => this.InitialStateI;

        IState IStateMachine.CurrentState
            => this.CurrentStateI;

        ITransition IStateMachine.CurrentTransition
            => this.CurrentTransitionI;

        IReadOnlyList<IState> IStateMachine.States
            => this.States;

        IReadOnlyList<ISignal> IStateMachine.Signals
            => this.Signals;

        IReadOnlyList<ITransition> IStateMachine.Transitions
            => this.Transitions;

        /// <summary>
        /// Internal <see cref="InitialState"/>
        /// </summary>
        internal State<TState, TTransition, TSignal> InitialStateI { get; private set; }

        /// <summary>
        /// Internal <see cref="CurrentState"/>
        /// </summary>
        internal State<TState, TTransition, TSignal> CurrentStateI { get; private set; }

        /// <summary>
        /// Internal <see cref="CurrentTransition"/>
        /// </summary>
        internal Transition<TState, TTransition, TSignal> CurrentTransitionI { get; private set; }

        /// <summary>
        /// Internal <see cref="StateMap"/>
        /// </summary>
        internal Dictionary<TState, State<TState, TTransition, TSignal>> StateMapI { get; }

        /// <summary>
        /// Internal <see cref="SignalMap"/>
        /// </summary>
        internal Dictionary<TSignal, Signal<TState, TTransition, TSignal>> SignalMapI { get; }

        /// <summary>
        /// Internal <see cref="TransitionMap"/>
        /// </summary>
        internal Dictionary<TTransition, Transition<TState, TTransition, TSignal>> TransitionMapI { get; }

        private readonly StateMachineActionList actions;

        public StateMachine()
        {
            this.StateMapI = new Dictionary<TState, State<TState, TTransition, TSignal>>();
            this.SignalMapI = new Dictionary<TSignal, Signal<TState, TTransition, TSignal>>();
            this.TransitionMapI = new Dictionary<TTransition, Transition<TState, TTransition, TSignal>>();
            this.actions = new StateMachineActionList();
        }

        #region State Creation

        internal bool AddState(State<TState, TTransition, TSignal> state)
        {
            if (state == null || this.StateMapI.ContainsKey(state.Name))
            {
                return false;
            }

            state.SetStateMachine(this);
            this.StateMapI.Add(state.Name, state);
            return true;
        }

        public State<TState, TTransition, TSignal> CreateState(TState stateName)
        {
            var state = new State<TState, TTransition, TSignal>(stateName);

            if (AddState(state))
                return state;

            return null;
        }

        public State<TState, TTransition, TSignal> CreateState(TState stateName, TState outerStateName,
                                                               int? orthogonalIndex = null)
        {
            var state = this.StateMapI[outerStateName];
            return CreateState(stateName, state, orthogonalIndex);
        }

        public State<TState, TTransition, TSignal> CreateState(TState stateName, State<TState, TTransition, TSignal> outerState,
                                                               int? orthogonalIndex = null)
        {
            var state = new State<TState, TTransition, TSignal>(stateName);

            if (!outerState.AddInnerState(state, orthogonalIndex))
                return null;

            this.StateMapI.Add(stateName, state);
            return state;
        }

        public bool TryCreateState(TState stateName, out State<TState, TTransition, TSignal> state)
        {
            if (this.StateMapI.ContainsKey(stateName))
            {
                state = null;
                return false;
            }

            state = CreateState(stateName);
            return state != null;
        }

        public bool TryCreateState(TState stateName, out State<TState, TTransition, TSignal> state,
                                   TState outerStateName, int? orthogonalIndex = null)
        {
            if (!this.StateMapI.ContainsKey(outerStateName) || this.StateMapI.ContainsKey(stateName))
            {
                state = null;
                return false;
            }

            var outerState = this.StateMapI[outerStateName];
            return TryCreateState(stateName, out state, outerState, orthogonalIndex);
        }

        public bool TryCreateState(TState stateName, out State<TState, TTransition, TSignal> state,
                                   State<TState, TTransition, TSignal> outerState, int? orthogonalIndex = null)
        {
            if (outerState == null)
            {
                state = null;
                return false;
            }

            state = CreateState(stateName, outerState, orthogonalIndex);
            return state != null;
        }

        #endregion

        #region Transition Creation

        public Transition<TState, TTransition, TSignal> CreateTransition(TTransition transitionName,
                                                                         TState startStateName, TState endStateName)
        {
            var startState = this.StateMapI[startStateName];
            var endState = this.StateMapI[endStateName];

            if (startState.MachineI != endState.MachineI)
            {
                throw new InvalidTransitionException<TState, TTransition>(startState.Name, endState.Name, transitionName);
            }

            return CreateTransition(transitionName, startState, endState);
        }

        public Transition<TState, TTransition, TSignal> CreateTransition(TTransition transitionName,
                                                                         State<TState, TTransition, TSignal> startState,
                                                                         State<TState, TTransition, TSignal> endState)
        {
            if (startState == null || endState == null)
            {
                return null;
            }

            if (startState.MachineI != endState.MachineI)
            {
                throw new InvalidTransitionException<TState, TTransition>(startState.Name, endState.Name, transitionName);
            }

            var machine = startState.MachineI;
            var transition = new Transition<TState, TTransition, TSignal>(machine, transitionName, startState, endState);
            machine.TransitionMapI.Add(transitionName, transition);

            if (machine != this)
                this.TransitionMapI.Add(transitionName, transition);

            startState.AddTransition(transition, endState);

            return transition;
        }

        public bool TryCreateTransition(TTransition transitionName, TState startStateName, TState endStateName,
                                        out Transition<TState, TTransition, TSignal> transition)
        {
            if (this.TransitionMapI.ContainsKey(transitionName) ||
                !this.StateMapI.ContainsKey(startStateName) ||
                !this.StateMapI.ContainsKey(endStateName))
            {
                transition = null;
                return false;
            }

            var startState = this.StateMapI[startStateName];
            var endState = this.StateMapI[endStateName];
            return TryCreateTransition(transitionName, startState, endState, out transition);
        }

        public bool TryCreateTransition(TTransition transitionName,
                                        State<TState, TTransition, TSignal> startState,
                                        State<TState, TTransition, TSignal> endState,
                                        out Transition<TState, TTransition, TSignal> transition)
        {
            if (startState == null ||
                endState == null ||
                this.TransitionMapI.ContainsKey(transitionName))
            {
                transition = null;
                return false;
            }

            if (startState.MachineI != endState.MachineI)
            {
                throw new InvalidTransitionException<TState, TTransition>(startState.Name, endState.Name, transitionName);
            }

            transition = CreateTransition(transitionName, startState, endState);
            return transition != null;
        }

        #endregion

        #region Signal Creation And Connection

        public bool ConnectSignal(TSignal signalName, TTransition transitionName)
        {
            if (!this.TransitionMapI.ContainsKey(transitionName))
            {
                return false;
            }

            var transition = this.TransitionMapI[transitionName];
            return ConnectSignal(signalName, transition);
        }

        public bool ConnectSignal(TSignal signalName, TTransition transitionName, out Signal<TState, TTransition, TSignal> signal)
        {
            if (!this.TransitionMapI.ContainsKey(transitionName))
            {
                signal = null;
                return false;
            }

            var transition = this.TransitionMapI[transitionName];
            return ConnectSignal(signalName, transition, out signal);
        }

        public bool ConnectSignal(TSignal signalName, Transition<TState, TTransition, TSignal> transition)
        {
            if (transition == null)
            {
                return false;
            }

            if (!this.SignalMapI.ContainsKey(signalName))
            {
                CreateSignal(signalName, transition);
                return true;
            }

            var signal = this.SignalMapI[signalName];

            return ConnectSignal(signal, transition);
        }

        public bool ConnectSignal(TSignal signalName, Transition<TState, TTransition, TSignal> transition,
                                  out Signal<TState, TTransition, TSignal> signal)
        {
            if (transition == null)
            {
                signal = null;
                return false;
            }

            if (this.SignalMapI.ContainsKey(signalName))
            {
                signal = this.SignalMapI[signalName];
                return ConnectSignal(signal, transition);
            }

            signal = CreateSignal(signalName, transition);
            return signal != null;
        }

        public bool ConnectSignal(Signal<TState, TTransition, TSignal> signal, TTransition transitionName)
        {
            if (signal == null || !this.TransitionMapI.ContainsKey(transitionName))
            {
                return false;
            }

            var transition = this.TransitionMapI[transitionName];
            return ConnectSignal(signal, transition);
        }

        public bool ConnectSignal(Signal<TState, TTransition, TSignal> signal, Transition<TState, TTransition, TSignal> transition)
        {
            if (signal == null || transition == null)
            {
                return false;
            }

            return signal.AddTransition(transition) && transition.AddSignal(signal);
        }

        public Signal<TState, TTransition, TSignal> CreateSignal(TSignal signalName, Transition<TState, TTransition, TSignal> transition)
        {
            if (transition == null)
            {
                return null;
            }

            var machine = transition.MachineI;
            var signal = new Signal<TState, TTransition, TSignal>(machine, signalName);
            signal.AddTransition(transition);
            transition.AddSignal(signal);
            machine.SignalMapI.Add(signal.Name, signal);

            if (machine != this)
                this.SignalMapI.Add(signal.Name, signal);

            return signal;
        }

        #endregion

        #region Set Initial State

        public bool SetInitialState(TState stateName)
        {
            if (!this.StateMapI.ContainsKey(stateName))
            {
                return false;
            }

            var state = this.StateMapI[stateName];
            return SetInitialState(state);
        }

        public bool SetInitialState(State<TState, TTransition, TSignal> state)
        {
            if (this.InitialStateI != null ||
                this.CurrentStateI != null ||
                this.Initialized ||
                state == null)
            {
                return false;
            }

            this.InitialStateI = state;
            return true;
        }

        public bool SetInitialState(TState stateName, TState outerStateName, int? orthogonalIndex = null)
        {
            if (!this.StateMapI.ContainsKey(outerStateName))
            {
                return false;
            }

            var outerState = this.StateMapI[outerStateName];
            return SetInitialState(stateName, outerState, orthogonalIndex);
        }

        public bool SetInitialState(TState stateName, State<TState, TTransition, TSignal> outerState, int? orthogonalIndex = null)
        {
            if (!this.StateMapI.ContainsKey(stateName))
            {
                return false;
            }

            var innerState = this.StateMapI[stateName];
            return SetInitialState(innerState, outerState, orthogonalIndex);
        }

        public bool SetInitialState(State<TState, TTransition, TSignal> state, State<TState, TTransition, TSignal> outerState,
                                    int? orthogonalIndex = null)
        {
            if (state == null || outerState == null)
            {
                return false;
            }

            return outerState.SetInitialInnerState(state, orthogonalIndex);
        }

        #endregion

        #region Create Condition

        public bool CreateEmitCondition(TSignal signalName, params TState[] conditionalStateNames)
        {
            if (!this.SignalMapI.ContainsKey(signalName))
            {
                return false;
            }

            var signal = this.SignalMapI[signalName];
            return CreateEmitCondition(signal, conditionalStateNames);
        }

        public bool CreateEmitCondition(Signal<TState, TTransition, TSignal> signal, params TState[] conditionalStateNames)
        {
            if (signal == null || !this.SignalMapI.ContainsKey(signal.Name))
            {
                return false;
            }

            var conditionalStates = new State<TState, TTransition, TSignal>[conditionalStateNames.Length];
            for (var i = 0; i < conditionalStateNames.Length; i++)
            {
                if (!this.StateMapI.ContainsKey(conditionalStateNames[i]))
                {
                    return false;
                }

                conditionalStates[i] = this.StateMapI[conditionalStateNames[i]];
            }

            return CreateEmitCondition(signal, conditionalStates);
        }

        public bool CreateEmitCondition(Signal<TState, TTransition, TSignal> signal, params State<TState, TTransition, TSignal>[] conditionalStates)
        {
            if (signal == null || !this.SignalMapI.ContainsKey(signal.Name))
            {
                return false;
            }

            foreach (var conditionalState in conditionalStates)
            {
                if (conditionalState == null || !this.StateMapI.ContainsKey(conditionalState.Name))
                {
                    return false;
                }
            }

            var signalCondition = new StateSignalCondition<TState, TTransition, TSignal>();
            signalCondition.AddCondition(conditionalStates);
            signal.AddEmitCondition(signalCondition);
            return true;
        }

        public bool CreateEmitCondition(Signal<TState, TTransition, TSignal> signal, ISignalCondition condition)
        {
            if (signal == null || !this.SignalMapI.ContainsKey(signal.Name))
            {
                return false;
            }

            signal.AddEmitCondition(condition);
            return true;
        }

        public bool CreateTransitionCondition(TSignal signalName, TTransition transitionName, params TState[] conditionalStateNames)
        {
            if (!this.SignalMapI.ContainsKey(signalName) ||
                !this.TransitionMapI.ContainsKey(transitionName))
            {
                return false;
            }

            var signal = this.SignalMapI[signalName];
            var transition = this.TransitionMapI[transitionName];
            return CreateTransitionCondition(signal, transition, conditionalStateNames);
        }

        public bool CreateTransitionCondition(Signal<TState, TTransition, TSignal> signal, TTransition transitionName, params TState[] conditionalStateNames)
        {
            if (signal == null || !this.SignalMapI.ContainsKey(signal.Name) ||
                !this.TransitionMapI.ContainsKey(transitionName))
            {
                return false;
            }

            var transition = this.TransitionMapI[transitionName];
            return CreateTransitionCondition(signal, transition, conditionalStateNames);
        }

        public bool CreateTransitionCondition(Signal<TState, TTransition, TSignal> signal, TTransition transitionName, params State<TState, TTransition, TSignal>[] conditionalStates)
        {
            if (signal == null || !this.SignalMapI.ContainsKey(signal.Name) ||
                !this.TransitionMapI.ContainsKey(transitionName))
            {
                return false;
            }

            var transition = this.TransitionMapI[transitionName];
            return CreateTransitionCondition(signal, transition, conditionalStates);
        }

        public bool CreateTransitionCondition(Signal<TState, TTransition, TSignal> signal, Transition<TState, TTransition, TSignal> transition, params TState[] conditionalStateNames)
        {
            if (signal == null || !this.SignalMapI.ContainsKey(signal.Name) ||
                transition == null || !this.TransitionMapI.ContainsKey(transition.Name))
            {
                return false;
            }

            var conditionalStates = new State<TState, TTransition, TSignal>[conditionalStateNames.Length];
            for (var i = 0; i < conditionalStateNames.Length; i++)
            {
                if (!this.StateMapI.ContainsKey(conditionalStateNames[i]))
                {
                    return false;
                }

                conditionalStates[i] = this.StateMapI[conditionalStateNames[i]];
            }

            return CreateTransitionCondition(signal, transition, conditionalStates);
        }

        public bool CreateTransitionCondition(Signal<TState, TTransition, TSignal> signal, Transition<TState, TTransition, TSignal> transition, params State<TState, TTransition, TSignal>[] conditionalStates)
        {
            if (signal == null || !this.SignalMapI.ContainsKey(signal.Name) ||
                transition == null || !this.TransitionMapI.ContainsKey(transition.Name))
            {
                return false;
            }

            foreach (var conditionalState in conditionalStates)
            {
                if (conditionalState == null || !this.StateMapI.ContainsKey(conditionalState.Name))
                {
                    return false;
                }
            }

            var signalCondition = new StateSignalCondition<TState, TTransition, TSignal>();
            signalCondition.AddCondition(conditionalStates);
            signal.AddTransitionCondition(signalCondition, transition);
            return true;
        }

        public bool CreateTransitionCondition(TSignal signalName, TTransition transitionName, ISignalCondition condition)
        {
            if (!this.SignalMapI.ContainsKey(signalName) ||
                !this.TransitionMapI.ContainsKey(transitionName))
            {
                return false;
            }

            var signal = this.SignalMapI[signalName];
            var transition = this.TransitionMapI[transitionName];
            return CreateTransitionCondition(signal, transition, condition);
        }

        public bool CreateTransitionCondition(Signal<TState, TTransition, TSignal> signal, TTransition transitionName, ISignalCondition condition)
        {
            if (signal == null || !this.SignalMapI.ContainsKey(signal.Name) ||
                !this.TransitionMapI.ContainsKey(transitionName))
            {
                return false;
            }

            var transition = this.TransitionMapI[transitionName];
            return CreateTransitionCondition(signal, transition, condition);
        }

        public bool CreateTransitionCondition(Signal<TState, TTransition, TSignal> signal, Transition<TState, TTransition, TSignal> transition, ISignalCondition condition)
        {
            if (signal == null || !this.SignalMapI.ContainsKey(signal.Name) ||
                transition == null || !this.TransitionMapI.ContainsKey(transition.Name))
            {
                return false;
            }

            signal.AddTransitionCondition(condition, transition);
            return true;
        }

        #endregion

        public bool AddAction(IStateMachineAction action)
        {
            if (action == null || this.actions.Contains(action))
                return false;

            switch (action)
            {
                case IStateMachineAction<TState, TTransition, TSignal> actionSTS:
                    actionSTS.Machine = this;
                    break;

                default:
                    action.Machine = this;
                    break;
            }

            this.actions.Add(action);
            return true;
        }

        public void Initialize()
        {
            if (!this.Initialized && this.CurrentStateI == null && this.InitialStateI != null)
            {
                this.actions.Initialize();
                this.Initialized = true;
                this.CurrentStateI = this.InitialStateI;
                this.CurrentStateI.Enter(default);
                this.CurrentStateI.EnterComplete(default);
            }
        }

        public void Terminate()
        {
            if (!this.Initialized)
                return;

            this.Initialized = false;
            this.CurrentStateI?.Terminate();
            this.CurrentStateI = null;
            this.actions.Terminate();
        }

        public void Tick()
        {
            if (!this.Initialized)
                return;

            this.actions.Tick();
            this.CurrentStateI?.Tick();
            this.CurrentTransitionI?.Tick();
        }

        public void EmitSignal(object signalName)
        {
            if (!this.Initialized)
                return;

            if (signalName is TSignal name)
            {
                EmitSignal(name);
            }
        }

        public void EmitSignal(TSignal signalName)
        {
            if (!this.Initialized)
                return;

            if (!this.SignalMapI.ContainsKey(signalName))
                return;

            this.SignalMapI[signalName].Emit();
        }

        public State<TState, TTransition, TSignal> GetStateByName(TState stateName)
        {
            return this.StateMapI[stateName];
        }

        public Transition<TState, TTransition, TSignal> GetTransitionByName(TTransition transitionName)
        {
            return this.TransitionMapI[transitionName];
        }

        public Signal<TState, TTransition, TSignal> GetSignalByName(TSignal signalName)
        {
            return this.SignalMapI[signalName];
        }

        public List<TState> GetAllActiveStateNames()
        {
            return this.StateMapI.Values.Where(s => s.IsCurrentState).Select(s => s.Name).ToList();
        }

        public List<string> GetAllActiveStateNamesAsString()
        {
            return this.StateMapI.Values.Where(s => s.IsCurrentState).Select(s => s.Name.ToString()).ToList();
        }

        public List<State<TState, TTransition, TSignal>> GetAllActiveStates()
        {
            return this.StateMapI.Values.Where(s => s.IsCurrentState).ToList();
        }

        List<IState> IStateMachine.GetAllActiveStates()
        {
            return this.StateMapI.Values.Where(s => s.IsCurrentState).ToList<IState>();
        }

        internal void FireOnStateChanged(State<TState, TTransition, TSignal> former, State<TState, TTransition, TSignal> current)
        {
            if (!this.Initialized)
                return;

            this.actions.StateChange(former, current);
        }

        internal void ProcessSignal(Signal<TState, TTransition, TSignal> signal)
        {
            if (!this.Initialized)
                return;

            if (this.CurrentTransitionI != null)
                return;

            foreach (var transition in signal.SignalToI)
            {
                if (this.CurrentStateI.TransitionsI.ContainsKey(transition) && transition.CanTransitionI)
                {
                    this.CurrentTransitionI = transition;
                    break;
                }
            }

            if (this.CurrentTransitionI == null)
            {
                this.CurrentStateI.PassSignal(signal);
                return;
            }

            if (!this.CurrentTransitionI.Invoke(signal))
            {
                signal.DoNotProcess();
                this.CurrentTransitionI = null;
            }
        }

        internal void TerminateTransition(Transition<TState, TTransition, TSignal> transition, Signal<TState, TTransition, TSignal> signal)
        {
            if (!this.Initialized)
                return;

            if (this.CurrentTransitionI != transition)
                return;

            if (this.CurrentStateI != transition.EndStateI)
                signal.DoNotProcess();

            this.CurrentTransitionI = null;
        }

        internal void StartTransition(Transition<TState, TTransition, TSignal> transition, Signal<TState, TTransition, TSignal> signal)
        {
            if (!this.Initialized)
                return;

            if (this.CurrentTransitionI != transition)
                return;

            this.CurrentStateI.Exit(transition.EndStateI);

            if (!transition.Start())
            {
                this.CurrentStateI.Resume(transition.EndStateI);
                transition.Terminate();
                return;
            }

            this.CurrentStateI = transition.EndStateI;
            this.CurrentStateI.Enter(transition.StartStateI);

            FireOnStateChanged(transition.StartStateI, transition.EndStateI);
            signal.DoProcess();
        }

        internal void FinishTransition(Transition<TState, TTransition, TSignal> transition)
        {
            if (!this.Initialized)
                return;

            if (this.CurrentTransitionI != transition)
                return;

            transition.Finish();
            this.CurrentTransitionI = null;

            this.CurrentStateI.EnterComplete(transition.StartStateI);
        }
    }
}
