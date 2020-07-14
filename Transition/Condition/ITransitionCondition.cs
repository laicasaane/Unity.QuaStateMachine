namespace QuaStateMachine
{
    public interface ITransitionCondition
    {
        bool Validate(ITransition transition);

        void Invalidate(ITransition transition);
    }
}
