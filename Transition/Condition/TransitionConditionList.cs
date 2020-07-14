using System.Collections.Generic;

namespace QuaStateMachine
{
    internal sealed class TransitionConditionList : List<ITransitionCondition>
    {
        internal TransitionConditionList() : base() { }

        internal TransitionConditionList(int capacity) : base(capacity) { }

        internal TransitionConditionList(IEnumerable<ITransitionCondition> collection) : base(collection) { }

        internal bool Validate(ITransition transition)
        {
            var result = true;

            for (var i = 0; i < this.Count; i++)
            {
                result &= this[i].Validate(transition);
            }

            return result;
        }

        internal void Invalidate(ITransition transition)
        {
            for (var i = 0; i < this.Count; i++)
            {
                this[i].Invalidate(transition);
            }
        }
    }
}
