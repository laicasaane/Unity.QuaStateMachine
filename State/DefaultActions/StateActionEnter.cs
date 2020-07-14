using System;

namespace QuaStateMachine
{
    internal sealed class StateActionEnter : DefaultStateAction
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
