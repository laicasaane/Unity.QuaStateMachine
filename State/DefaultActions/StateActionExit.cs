using System;

namespace QuaStateMachine
{
    internal sealed class StateActionExit : DefaultStateAction
    {
        private readonly Action<IStateAction> action;

        internal StateActionExit(Action<IStateAction> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public override void Exit(IState next)
        {
            this.action(this);
        }
    }
}
