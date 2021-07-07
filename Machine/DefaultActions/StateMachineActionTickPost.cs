using System;

namespace QuaStateMachine
{
    internal sealed class StateMachineActionTickPost : DefaultStateMachineAction, IPostTickable
    {
        private readonly Action<IStateMachineAction> action;

        internal StateMachineActionTickPost(Action<IStateMachineAction> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void PostTick()
        {
            this.action(this);
        }
    }
}
