using System;

namespace QuaStateMachine
{
    internal sealed class StateActionTerminate : DefaultStateAction
    {
        private readonly Action<IStateAction> action;

        internal StateActionTerminate(Action<IStateAction> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public override void Terminate()
        {
            this.action(this);
        }
    }
}
