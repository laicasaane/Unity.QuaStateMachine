namespace QuaStateMachine
{
    internal abstract class DefaultStateMachineAction<T> : DefaultStateMachineAction, IStateMachineAction<T>
    {
        public virtual void StateChange(IState<T> former, IState<T> current) { }

        public sealed override void StateChange(IState former, IState current) { }
    }
}
