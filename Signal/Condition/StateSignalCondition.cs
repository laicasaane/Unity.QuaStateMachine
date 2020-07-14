using System.Collections.Generic;

namespace QuaStateMachine
{
    public sealed class StateSignalCondition<TState, TTransition, TSignal> : ISignalCondition
    {
        public IReadOnlyList<State<TState, TTransition, TSignal>> Conditions
            => this.conditions;

        private readonly List<State<TState, TTransition, TSignal>> conditions;

        internal StateSignalCondition()
        {
            this.conditions = new List<State<TState, TTransition, TSignal>>();
        }

        internal void AddCondition(params State<TState, TTransition, TSignal>[] conditions)
        {
            foreach (var condition in conditions)
            {
                if (condition != null && !this.conditions.Exists(c => c.Name.Equals(condition.Name)))
                {
                    this.conditions.Add(condition);
                }
            }
        }

        public bool Validate(ISignal signal)
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