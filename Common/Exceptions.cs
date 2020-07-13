using System;

namespace FluentQuaStateMachine
{
    internal class InvalidTransitionException<TState, TTransition> : Exception
    {
        public override string Message { get; }

        internal InvalidTransitionException(TState sourceStateName, TState targetStateName, TTransition transitionName)
        {
            this.Message = $"Transition between different state levels are not allowed.\n" +
                           $"Source State: {sourceStateName}\n" +
                           $"Target State: {targetStateName}\n" +
                           $"Transition Name: {transitionName}";
        }

        internal InvalidTransitionException()
        { }

        internal InvalidTransitionException(string message) : base(message)
        { }
    }
}
