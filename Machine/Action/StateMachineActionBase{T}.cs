namespace FluentQuaStateMachine
{
    public abstract class StateMachineActionBase<T> : StateMachineActionBase, IStateMachineAction<T>
    {
        public virtual void StateChange(IState<T> former, IState<T> current) { }

        public sealed override void StateChange(IState former, IState current) { }
    }
}
