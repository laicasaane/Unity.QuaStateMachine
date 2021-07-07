using System.Collections.Generic;

namespace QuaStateMachine
{
    internal sealed class StateMachineActionList : List<IStateMachineAction>
    {
        internal StateMachineActionList() : base() { }

        internal StateMachineActionList(int capacity) : base(capacity) { }

        internal StateMachineActionList(IEnumerable<IStateMachineAction> collection) : base(collection) { }

        internal void Initialize()
        {
            for (var i = 0; i < this.Count; i++)
            {
                this[i].Initialize();
            }
        }

        internal void StateChange<TState, TTransition, TSignal>(State<TState, TTransition, TSignal> former, State<TState, TTransition, TSignal> current)
        {
            for (var i = 0; i < this.Count; i++)
            {
                switch (this[i])
                {
                    case IStateMachineAction<TState, TTransition, TSignal> actionSTS:
                        actionSTS.StateChange(former, current);
                        break;

                    case IStateMachineAction<TState> actionT:
                        actionT.StateChange(former, current);
                        break;

                    default:
                        this[i].StateChange(former, current);
                        break;
                }
            }
        }

        internal void Terminate()
        {
            for (var i = 0; i < this.Count; i++)
            {
                this[i].Terminate();
            }
        }
    }
}
