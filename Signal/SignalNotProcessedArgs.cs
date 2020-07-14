using System;
using System.Collections.Generic;

namespace QuaStateMachine
{
    public class SignalNotProcessedArgs : EventArgs
    {
        public SignalFailure FailureCause { get; }

        public IReadOnlyList<ISignalCondition> FailedConditions { get; }

        internal SignalNotProcessedArgs(SignalFailure failureCause, List<ISignalCondition> failedConditions = null)
        {
            this.FailureCause = failureCause;
            this.FailedConditions = failedConditions;
        }
    }

    public enum SignalFailure
    {
        NoTransitionToState,
        EmitConditionsNotMet,
        TransitionConditionsNotMet,
        TransitionAmbiguity
    }
}
