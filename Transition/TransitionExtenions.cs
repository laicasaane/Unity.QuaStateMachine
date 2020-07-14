namespace QuaStateMachine
{
    public static class TransitionExtenions
    {
        public static StateDirection<object> GetDirection(this ITransition transition)
            => new StateDirection<object>(transition.StartState.Name, transition.EndState.Name);

        public static StateDirection<TState> GetDirection<TState>(this ITransition transition)
            => new StateDirection<TState>((TState)transition.StartState.Name, (TState)transition.EndState.Name);

        public static StateDirection<TState> GetDirection<TState, TTransition, TSignal>(this ITransition<TState, TTransition, TSignal> transition)
            => new StateDirection<TState>(transition.StartState.Name, transition.EndState.Name);
    }
}
