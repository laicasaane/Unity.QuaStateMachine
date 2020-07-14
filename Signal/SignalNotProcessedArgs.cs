using System;

namespace QuaStateMachine
{
    public class SignalNotProcessedArgs : EventArgs
    {
        public SignalFailure FailureCause { get; }

        public ISignalCondition[] FailedConditions { get; }

        internal SignalNotProcessedArgs(SignalFailure failureCause, ISignalCondition[] failedConditions = null)
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
