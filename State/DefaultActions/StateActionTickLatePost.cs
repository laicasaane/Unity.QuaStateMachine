using System;

namespace QuaStateMachine
{
    internal sealed class StateActionTickLatePost : DefaultStateAction, IPostLateTickable
    {
        private readonly Action<IStateAction> action;

        internal StateActionTickLatePost(Action<IStateAction> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void PostLateTick()
        {
            this.action(this);
        }
    }
}
