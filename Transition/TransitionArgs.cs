using System;

namespace QuaStateMachine
{
    public class TransitionArgs : EventArgs
    {
        public bool CancelTransition { get; set; }
    }
}