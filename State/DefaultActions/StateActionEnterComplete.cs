using System;

namespace QuaStateMachine
{
    internal sealed class StateActionEnterComplete : StateActionBase
    {
        private readonly Action<IStateAction> action;

        internal StateActionEnterComplete(Action<IStateAction> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public override void EnterComplete(IState previous)
        {
            this.action(this);
        }
    }
}
