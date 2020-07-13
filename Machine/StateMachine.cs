namespace FluentQuaStateMachine
{
    using Transition = Transition<string, StateDirection<string>, string>;
    using Signal = Signal<string, StateDirection<string>, string>;

    public sealed class StateMachine : StateMachine<string, StateDirection<string>, string>
    {
        public StateMachine Create(in StateDirection<string> direction, out Transition transition)
        {
            Create(direction, direction.From, direction.To, out transition);
            return this;
        }

        public StateMachine Create(Transition transition, out Signal signal)
        {
            Create(transition.ToString(), transition, out signal);
            return this;
        }
    }
}