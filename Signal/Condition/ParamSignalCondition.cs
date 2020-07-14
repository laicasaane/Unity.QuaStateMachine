using System;

namespace QuaStateMachine
{
    internal sealed class ParamSignalCondition : ISignalCondition
    {
        private readonly Predicate<ISignal> condition;

        internal ParamSignalCondition(Predicate<ISignal> condition)
        {
            this.condition = condition ?? throw new ArgumentNullException(nameof(condition));
        }

        public bool Validate(ISignal signal)
            => this.condition(signal);
    }
}
