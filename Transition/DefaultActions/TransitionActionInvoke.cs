using System;

namespace FluentQuaStateMachine
{
    internal sealed class TransitionActionInvoke : TransitionActionBase
    {
        private readonly Action<ITransitionAction, TransitionArgs> action;

        internal TransitionActionInvoke(Action<ITransitionAction, TransitionArgs> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public override void Invoke(TransitionArgs args)
        {
            base.Invoke(args);
            this.action(this, args);
        }
    }
}
