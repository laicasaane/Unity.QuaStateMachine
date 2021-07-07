using System;

namespace QuaStateMachine
{
    internal sealed class StateMachineActionTickFixedPost : DefaultStateMachineAction, IPostFixedTickable
    {
        private readonly Action<IStateMachineAction> action;

        internal StateMachineActionTickFixedPost(Action<IStateMachineAction> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void PostFixedTick()
        {
            this.action(this);
        }
    }
}
