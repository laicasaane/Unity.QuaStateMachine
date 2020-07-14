using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace QuaStateMachine
{
    public sealed partial class Signal<TState, TTransition, TSignal>
        : Signal<TSignal>, ISignal<TState, TTransition, TSignal>
    {
        public StateMachine<TState, TTransition, TSignal> Machine
            => this.MachineI;

        public IReadOnlyList<Transition<TState, TTransition, TSignal>> SignalTo
            => this.SignalToI;

        public override IReadOnlyList<ISignalAction> Actions
            => this.actions;

        public IReadOnlyDictionary<ISignalCondition, Transition<TState, TTransition, TSignal>> TransitionConditions
            => this.TransitionConditionsI;

        /// <summary>
        /// Internal <see cref="Machine"/>
        /// </summary>
        internal StateMachine<TState, TTransition, TSignal> MachineI { get; }

        /// <summary>
        /// Internal <see cref="SignalTo"/>
        /// </summary>
        internal List<Transition<TState, TTransition, TSignal>> SignalToI { get; }

        /// <summary>
        /// Internal <see cref="EmitConditions"/>
        /// </summary>
        internal List<ISignalCondition> EmitConditionsI { get; }

        /// <summary>
        /// Internal <see cref="TransitionConditions"/>
        /// </summary>
        internal Dictionary<ISignalCondition, Transition<TState, TTransition, TSignal>> TransitionConditionsI { get; }

        private readonly SignalActionList actions;
        private readonly List<ISignalCondition> failedConditions;

        internal Signal(StateMachine<TState, TTransition, TSignal> machine, TSignal name) : base(name)
        {
            this.MachineI = machine;
            this.SignalToI = new List<Transition<TState, TTransition, TSignal>>();
            this.EmitConditionsI = new List<ISignalCondition>();
            this.TransitionConditionsI = new Dictionary<ISignalCondition, Transition<TState, TTransition, TSignal>>();
            this.actions = new SignalActionList();
            this.failedConditions = new List<ISignalCondition>();
        }

        public override bool AddAction(ISignalAction action)
        {
            if (action == null || this.actions.Contains(action))
                return false;

            switch (action)
            {
                case ISignalAction<TState, TTransition, TSignal> actionSTS:
                    actionSTS.Signal = this;
                    break;

                case ISignalAction<TSignal> actionT:
                    actionT.Signal = this;
                    break;

                default:
                    action.Signal = this;
                    break;
            }

            this.actions.Add(action);
            return true;
        }

        public override void Emit()
        {
            this.failedConditions.Clear();
            this.actions.Emit();

            #region Emit Conditions Check
            // check emit conditions, it is enough to pass if one of the conditions is met.
            // This allows to make OR logical comparisons between SignalEmitConditions.
            var emitConditionMet = this.EmitConditionsI.Count <= 0;

            if (!emitConditionMet)
            {
                foreach (var signalCondition in this.EmitConditionsI)
                {
                    if (signalCondition.Validate(this))
                    {
                        emitConditionMet = true;
                    }
                    else
                    {
                        this.failedConditions.Add(signalCondition);
                    }
                }
            }

            if (!emitConditionMet)
            {
                var args = new SignalNotProcessedArgs(SignalFailure.EmitConditionsNotMet, this.failedConditions.ToArray());
                this.actions.NotProcess(args);

                return;
            }
            #endregion

            #region Transition Conditions Check
            // check transition conditions, there must be only one valid transition.
            // If more than one, stop emitting the signal, otherwise this might cause undefined behaviour.
            var transitionConditionMetCount = this.TransitionConditionsI.Count != 0 ? 0 : 1;

            foreach (var kv in this.TransitionConditionsI)
            {
                if (kv.Key.Validate(this))
                {
                    kv.Value.CanTransitionI = true;
                    transitionConditionMetCount++;
                }
                else
                {
                    kv.Value.CanTransitionI = false;
                    this.failedConditions.Add(kv.Key);
                }
            }

            if (transitionConditionMetCount == 0)
            {
                var args = new SignalNotProcessedArgs(SignalFailure.TransitionConditionsNotMet, this.failedConditions.ToArray());
                this.actions.NotProcess(args);

                return;
            }

            if (transitionConditionMetCount > 1)
            {
                var args = new SignalNotProcessedArgs(SignalFailure.TransitionAmbiguity, this.failedConditions.ToArray());
                this.actions.NotProcess(args);

                return;
            }
            #endregion

            this.MachineI.ProcessSignal(this);
        }

        internal void DoNotProcess()
        {
            var args = new SignalNotProcessedArgs(SignalFailure.NoTransitionToState);
            this.actions.NotProcess(args);
        }

        internal void DoProcess()
        {
            this.actions.Process();
        }

        public override string ToString()
        {
            return this.Name.ToString();
        }

        internal bool AddTransition(Transition<TState, TTransition, TSignal> transition)
        {
            if (this.SignalToI.Exists(t => t.Name.Equals(transition.Name)))
            {
                return false;
            }

            this.SignalToI.Add(transition);
            return true;
        }

        internal void AddEmitCondition(ISignalCondition condition)
        {
            if (!this.EmitConditionsI.Contains(condition))
                this.EmitConditionsI.Add(condition);
        }

        internal void AddTransitionCondition(ISignalCondition condition, Transition<TState, TTransition, TSignal> transition)
        {
            if (condition == null || transition == null || !this.SignalToI.Exists(t => t.Name.Equals(transition.Name)))
            {
                return;
            }

            this.TransitionConditionsI[condition] = transition;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override IStateMachine GetMachine()
            => this.MachineI;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override IReadOnlyList<ITransition> GetSignalTo()
            => this.SignalToI;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override IReadOnlyList<ISignalCondition> GetEmitConditions()
            => this.EmitConditionsI;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override IReadOnlyDictionary<ISignalCondition, ITransition> GetTransitionConditions()
            => this.TransitionConditionsI.ToDictionary(x => x.Key, x => x.Value as ITransition);
    }
}
