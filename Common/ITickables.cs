namespace QuaStateMachine
{
    public interface IFixedTickable
    {
        void FixedTick();
    }

    public interface IPostFixedTickable
    {
        void PostFixedTick();
    }

    public interface ITickable
    {
        void Tick();
    }

    public interface IPostTickable
    {
        void PostTick();
    }

    public interface ILateTickable
    {
        void LateTick();
    }

    public interface IPostLateTickable
    {
        void PostLateTick();
    }
}