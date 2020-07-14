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

        public IReadOnlyList<SignalCondition<TState, TTransition, TSignal>> EmitConditions
            => this.EmitConditionsI;

        public IReadOnlyDictionary<SignalCondition<TState, TTransition, TSignal>, Transition<TState, TTransition, TSignal>> TransitionConditions
            => this.TransitionConditionsI;

        public override IReadOnlyList<ISignalAction> Actions
            => this.actions;

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
        internal List<SignalCondition<TState, TTransition, TSignal>> EmitConditionsI { get; }

        /// <summary>
        /// Internal <see cref="TransitionConditions"/>
        /// </summary>
        internal Dictionary<SignalCondition<TState, TTransition, TSignal>, Transition<TState, TTransition, TSignal>> TransitionConditionsI { get; }

        private readonly SignalActionList actions;

        internal Signal(StateMachine<TState, TTransition, TSignal> machine, TSignal name) : base(name)
        {
            this.MachineI = machine;
            this.SignalToI = new List<Transition<TState, TTransition, TSignal>>();
            this.EmitConditionsI = new List<SignalCondition<TState, TTransition, TSignal>>();
            this.TransitionConditionsI = new Dictionary<SignalCondition<TState, TTransition, TSignal>,
                                                        Transition<TState, TTransition, TSignal>>();
            this.actions = new SignalActionList();
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
            this.actions.Emit();

            #region Emit Conditions Check
            // check emit conditions, it is enough to pass if one of the conditions is met.
            // This allows to make OR logical comparisons between SignalEmitConditions.
            var emitConditionMet = this.EmitConditionsI.Count <= 0;

            foreach (var signalCondition in this.EmitConditionsI)
            {
                if (signalCondition.IsValid)
                {
                    emitConditionMet = true;
                }
            }

            if (!emitConditionMet)
            {
                var failedConditions = this.EmitConditionsI.ToList<ISignalCondition>();
                var args = new SignalNotProcessedArgs(SignalFailure.EmitConditionsNotMet, failedConditions);
                this.actions.NotProcess(args);

                return;
            }
            #endregion

            #region Transition Conditions Check
            // check transition conditions, there must be only one valid transition.
            // If more than one, stop emitting the signal, otherwise this might cause undefined behaviour.
            var conditionMetCount = this.TransitionConditionsI.Count != 0 ? 0 : 1;

            foreach (var kv in this.TransitionConditionsI)
            {
                if (kv.Key.IsValid)
                {
                    kv.Value.CanTransitionI = true;
                    conditionMetCount++;
                }
                else
                {
                    kv.Value.CanTransitionI = false;
                }
            }

            if (conditionMetCount == 0)
            {
                var failedConditions = this.TransitionConditionsI.Keys.ToList<ISignalCondition>();
                var args = new SignalNotProcessedArgs(SignalFailure.TransitionConditionsNotMet, failedConditions);
                this.actions.NotProcess(args);

                return;
            }

            if (conditionMetCount > 1)
            {
                var failedConditions = this.TransitionConditionsI.Keys.ToList<ISignalCondition>();
                var args = new SignalNotProcessedArgs(SignalFailure.TransitionAmbiguity, failedConditions);
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

        internal void AddEmitCondition(SignalCondition<TState, TTransition, TSignal> condition)
        {
            this.EmitConditionsI.Add(condition);
        }

        internal void AddTransitionCondition(SignalCondition<TState, TTransition, TSignal> condition, Transition<TState, TTransition, TSignal> transition)
        {
            if (condition == null || transition == null || !this.SignalToI.Exists(t => t.Name.Equals(transition.Name)))
            {
                return;
            }

            this.TransitionConditionsI.Add(condition, transition);
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
            => this.TransitionConditionsI.ToDictionary(x => x.Key as ISignalCondition, x => x.Value as ITransition);
    }
}
