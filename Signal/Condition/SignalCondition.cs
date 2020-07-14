using System;

namespace QuaStateMachine
{
    internal sealed class SignalCondition : ISignalCondition
    {
        private readonly Func<bool> condition;

        internal SignalCondition(Func<bool> condition)
        {
            this.condition = condition ?? throw new ArgumentNullException(nameof(condition));
        }

        public bool Validate(ISignal signal)
            => this.condition();
    }
}
