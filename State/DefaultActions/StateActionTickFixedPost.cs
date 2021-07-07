using System;

namespace QuaStateMachine
{
    internal sealed class StateActionTickFixedPost : DefaultStateAction, IPostFixedTickable
    {
        private readonly Action<IStateAction> action;

        internal StateActionTickFixedPost(Action<IStateAction> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void PostFixedTick()
        {
            this.action(this);
        }
    }
}
