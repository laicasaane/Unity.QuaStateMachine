using System.Collections.Generic;

namespace QuaStateMachine
{
    internal sealed class SignalActionList : List<ISignalAction>
    {
        internal SignalActionList() : base() { }

        internal SignalActionList(int capacity) : base(capacity) { }

        internal SignalActionList(IEnumerable<ISignalAction> collection) : base(collection) { }

        internal void Emit()
        {
            for (var i = 0; i < this.Count; i++)
            {
                this[i].Emit();
            }
        }

        internal void Process()
        {
            for (var i = 0; i < this.Count; i++)
            {
                this[i].Process();
            }
        }

        internal void NotProcess(SignalNotProcessedArgs args)
        {
            for (var i = 0; i < this.Count; i++)
            {
                this[i].NotProcess(args);
            }
        }
    }
}
