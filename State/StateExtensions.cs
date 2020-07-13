namespace FluentQuaStateMachine
{
    public static class StateExtensions
    {
        public static StateDirection<object> To(this IState from, IState to)
            => new StateDirection<object>(from.Name, to.Name);

        public static StateDirection<T> To<T>(this IState<T> from, IState<T> to)
            => new StateDirection<T>(from.Name, to.Name);
    }
}
