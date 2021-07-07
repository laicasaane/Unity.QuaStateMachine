using System;

namespace QuaStateMachine
{
    internal sealed class StateActionTickPost : DefaultStateAction, IPostTickable
    {
        private readonly Action<IStateAction> action;

        internal StateActionTickPost(Action<IStateAction> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void PostTick()
        {
            this.action(this);
        }
    }
}
