namespace QuaStateMachine
{
    public interface ISignalCondition
    {
        bool Validate(ISignal signal);
    }
}