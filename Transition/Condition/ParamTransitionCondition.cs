using System;

namespace QuaStateMachine
{
    internal sealed class ParamTransitionCondition : ITransitionCondition
    {
        private readonly Predicate<ITransition> condition;

        internal ParamTransitionCondition(Predicate<ITransition> condition)
        {
            this.condition = condition ?? throw new ArgumentNullException(nameof(condition));
        }

        public bool Validate(ITransition transition)
            => this.condition(transition);

        public void Invalidate(ITransition transition) { }
    }
}
