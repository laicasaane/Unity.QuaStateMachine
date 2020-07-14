using System.Collections.Generic;

namespace QuaStateMachine
{
    internal sealed class TransitionConditionList : List<ITransitionCondition>
    {
        internal TransitionConditionList() : base() { }

        internal TransitionConditionList(int capacity) : base(capacity) { }

        internal TransitionConditionList(IEnumerable<ITransitionCondition> collection) : base(collection) { }

        internal bool Validate()
        {
            var result = true;

            for (var i = 0; i < this.Count; i++)
            {
                result &= this[i].Validate();
            }

            return result;
        }
    }
}
