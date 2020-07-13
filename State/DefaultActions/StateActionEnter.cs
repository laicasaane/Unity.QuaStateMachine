using System;

namespace FluentQuaStateMachine
{
    internal sealed class StateActionEnter : StateActionBase
    {
        private readonly Action<IStateAction> action;

        internal StateActionEnter(Action<IStateAction> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public override void Enter(IState previous)
        {
            base.Enter(previous);
            this.action(this);
        }
    }
}
