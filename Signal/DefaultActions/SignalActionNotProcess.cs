using System;

namespace QuaStateMachine
{
    internal sealed class SignalActionNotProcess : SignalActionBase
    {
        private readonly Action<ISignalAction, SignalNotProcessedArgs> action;

        internal SignalActionNotProcess(Action<ISignalAction, SignalNotProcessedArgs> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public override void NotProcess(SignalNotProcessedArgs args)
        {
            this.action(this, args);
        }
    }
}
