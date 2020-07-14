using System;

namespace QuaStateMachine
{
    internal sealed class SignalActionProcess : SignalActionBase
    {
        private readonly Action<ISignalAction> action;

        internal SignalActionProcess(Action<ISignalAction> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public override void Process()
        {
            this.action(this);
        }
    }
}
