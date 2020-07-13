using System;

namespace FluentQuaStateMachine
{
    internal sealed class StateActionResume : StateActionBase
    {
        private readonly Action<IStateAction> action;

        internal StateActionResume(Action<IStateAction> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public override void Resume(IState next)
        {
            this.action(this);
        }
    }
}
