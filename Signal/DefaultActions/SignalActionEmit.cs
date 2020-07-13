using System;

namespace FluentQuaStateMachine
{
    internal sealed class SignalActionEmit : SignalActionBase
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
