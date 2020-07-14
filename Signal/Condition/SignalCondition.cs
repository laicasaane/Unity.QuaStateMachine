using System.Collections.Generic;

namespace QuaStateMachine
{
    public sealed class SignalCondition<TState, TTransition, TSignal> : ISignalCondition<TState, TTransition, TSignal>
    {
        public bool IsValid
            => Validate();

        public IReadOnlyList<State<TState, TTransition, TSignal>> Conditions
            => this.conditions;

        IReadOnlyList<IState<TState>> ISignalCondition<TState>.Conditions
            => this.conditions;

        IReadOnlyList<IState> ISignalCondition.Conditions
            => this.conditions;

        private readonly List<State<TState, TTransition, TSignal>> conditions;

        internal SignalCondition()
        {
            this.conditions = new List<State<TState, TTransition, TSignal>>();
        }

        public void AddCondition(params State<TState, TTransition, TSignal>[] conditions)
        {
            foreach (var condition in conditions)
            {
                if (condition != null && !this.conditions.Exists(c => c.Name.Equals(condition.Name)))
                {
                    this.conditions.Add(condition);
                }
            }
        }

        private bool Validate()
        {
            foreach (var condition in this.conditions)
            {
                if (!condition.IsCurrentState)
                    return false;
            }

            return true;
        }
    }
}