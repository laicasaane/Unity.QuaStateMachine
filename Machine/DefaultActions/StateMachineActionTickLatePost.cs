using System;

namespace QuaStateMachine
{
    internal sealed class StateMachineActionTickLatePost : DefaultStateMachineAction, IPostLateTickable
    {
        private readonly Action<IStateMachineAction> action;

        internal StateMachineActionTickLatePost(Action<IStateMachineAction> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void PostLateTick()
        {
            this.action(this);
        }
    }
}
