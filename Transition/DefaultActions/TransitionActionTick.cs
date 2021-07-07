﻿using System;

namespace QuaStateMachine
{
    internal sealed class TransitionActionTick : DefaultTransitionAction, ITickable
    {
        private readonly Action<ITransitionAction> action;

        internal TransitionActionTick(Action<ITransitionAction> action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void Tick()
        {
            this.action(this);
        }
    }
}
