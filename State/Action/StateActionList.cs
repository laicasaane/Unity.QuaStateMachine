using System.Collections.Generic;

namespace FluentQuaStateMachine
{
    internal sealed class StateActionList : List<IStateAction>
    {
        internal StateActionList() : base() { }

        internal StateActionList(int capacity) : base(capacity) { }

        internal StateActionList(IEnumerable<IStateAction> collection) : base(collection) { }

        internal void Enter(IState previous)
        {
            for (var i = 0; i < this.Count; i++)
            {
                this[i].Enter(previous);
            }
        }

        internal void Resume(IState next)
        {
            for (var i = 0; i < this.Count; i++)
            {
                this[i].Resume(next);
            }
        }

        internal void EnterComplete(IState previous)
        {
            for (var i = 0; i < this.Count; i++)
            {
                this[i].EnterComplete(previous);
            }
        }

        internal void Exit(IState next)
        {
            for (var i = 0; i < this.Count; i++)
            {
                this[i].Exit(next);
            }
        }

        internal void Tick()
        {
            for (var i = 0; i < this.Count; i++)
            {
                this[i].Tick();
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
