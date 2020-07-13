using System;

namespace FluentQuaStateMachine
{
    internal sealed class StateActionTick : StateActionBase
    {
        private readonly Action<IStateAction> action;

        internal StateActionTick(Action<IStateAction> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public override void Tick()
        {
            this.action(this);
        }
    }
}
