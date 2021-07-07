﻿using System;

namespace QuaStateMachine
{
    internal sealed class StateMachineActionTick : DefaultStateMachineAction, ITickable
    {
        private readonly Action<IStateMachineAction> action;

        internal StateMachineActionTick(Action<IStateMachineAction> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void Tick()
        {
            this.action(this);
        }
    }
}
