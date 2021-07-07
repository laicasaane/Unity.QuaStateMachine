﻿using System;

namespace QuaStateMachine
{
    internal sealed class StateActionTick : DefaultStateAction, ITickable
    {
        private readonly Action<IStateAction> action;

        internal StateActionTick(Action<IStateAction> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void Tick()
        {
            this.action(this);
        }
    }
}
