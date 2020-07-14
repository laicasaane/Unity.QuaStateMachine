using System;

namespace QuaStateMachine
{
    internal sealed class StateActionResume : DefaultStateAction
    {
        private readonly Action<IStateAction> action;

        internal StateActionResume(Action<IStateAction> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public override void Resume(IState next)
        {
            this.action(this);
        }
    }
}
