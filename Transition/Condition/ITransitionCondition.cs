namespace QuaStateMachine
{
    public interface ITransitionCondition
    {
        bool Validate();

        void Invalidate();
    }
}
