﻿using System;

namespace QuaStateMachine
{
    internal sealed class TransitionCondition : ITransitionCondition
    {
        private readonly Func<bool> condition;

        internal TransitionCondition(Func<bool> condition)
        {
            this.condition = condition ?? throw new ArgumentNullException(nameof(condition));
        }

        public bool Validate(ITransition transition)
            => this.condition();

        public void Invalidate(ITransition transition) { }
    }
}
