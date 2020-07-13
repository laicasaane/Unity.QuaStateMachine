using System;

namespace FluentQuaStateMachine
{
    public class TransitionArgs : EventArgs
    {
        public bool CancelTransition { get; set; }
    }
}