namespace FluentQuaStateMachine
{
    public interface ITransitionCondition
    {
        bool Validate();
    }
}
