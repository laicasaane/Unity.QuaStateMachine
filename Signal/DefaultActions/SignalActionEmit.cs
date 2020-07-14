using System;

namespace QuaStateMachine
{
    internal sealed class SignalActionEmit : DefaultSignalAction
    {
        private readonly Action<ISignalAction> action;

        internal SignalActionEmit(Action<ISignalAction> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public override void Emit()
        {
            this.action(this);
        }
    }
}
