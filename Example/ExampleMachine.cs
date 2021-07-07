using System;
using System.Collections.Generic;
using UnityEngine;

namespace QuaStateMachine.Examples
{
    using State = State<string, StateDirection<string>, string>;
    using Transition = Transition<string, StateDirection<string>, string>;
    using Signal = Signal<string, StateDirection<string>, string>;

    public static class MainState
    {
        public const string A = nameof(A);
        public const string B = nameof(B);
        public const string C = nameof(C);
        public const string D = nameof(D);
        public const string E = nameof(E);
    }

    public static class BSubState
    {
        public const string B1 = nameof(B1);
        public const string B2 = nameof(B2);
    }

    public static class B2SubState
    {
        public const string B21 = nameof(B21);
        public const string B22 = nameof(B22);
    }

    public static class BSubSignal
    {
        public const string ToggleB = nameof(ToggleB);
    }

    public static class B2SubSignal
    {
        public const string ToggleB2 = nameof(ToggleB2);
    }

    public static class ExampleMachine
    {
        private static class MainStates
        {
            public static State A;
            public static State B;
            public static State C;
            public static State D;
            public static State E;
        }

        private static class MainTransitions
        {
            public static Transition A_B;
            public static Transition B_C;
            public static Transition B_D;
            public static Transition C_E;
            public static Transition D_E;
            public static Transition E_A;
        }

        private static class MainSignals
        {
            public static Signal A_B;
            public static Signal B_C;
            public static Signal B_D;
            public static Signal C_E;
            public static Signal D_E;
            public static Signal E_A;
        }

        private static class BSubStates
        {
            public static State B1;
            public static State B2;
        }

        private static class BSubTransitions
        {
            public static Transition B1_B2;
            public static Transition B2_B1;
        }

        private static class BSubSignals
        {
            public static Signal ToggleB;
        }

        private static class B2SubStates
        {
            public static State B21;
            public static State B22;
        }

        private static class B2SubTransitions
        {
            public static Transition B21_B22;
            public static Transition B22_B21;
        }

        private static class B2SubSignals
        {
            public static Signal ToggleB2;
        }

        public static void CreateStates(StateMachine machine)
        {
            machine
                .Create(MainState.A, out MainStates.A)
                .Create(MainState.B, out MainStates.B)
                .Create(MainState.C, out MainStates.C)
                .Create(MainState.D, out MainStates.D)
                .Create(MainState.E, out MainStates.E)

                .Initial(MainState.A)
            ;
        }

        public static void CreateTransitions(StateMachine machine)
        {
            machine
                .Create(MainStates.A.To(MainStates.B), out MainTransitions.A_B)
                .Create(MainStates.B.To(MainStates.C), out MainTransitions.B_C)
                .Create(MainStates.B.To(MainStates.D), out MainTransitions.B_D)
                .Create(MainStates.C.To(MainStates.E), out MainTransitions.C_E)
                .Create(MainStates.D.To(MainStates.E), out MainTransitions.D_E)
                .Create(MainStates.E.To(MainStates.A), out MainTransitions.E_A)
            ;
        }

        public static void CreateSignals(StateMachine machine)
        {
            machine
                .Create(MainTransitions.A_B, out MainSignals.A_B)
                .Create(MainTransitions.B_C, out MainSignals.B_C)
                .Create(MainTransitions.B_D, out MainSignals.B_D)
                .Create(MainTransitions.C_E, out MainSignals.C_E)
                .Create(MainTransitions.D_E, out MainSignals.D_E)
                .Create(MainTransitions.E_A, out MainSignals.E_A)
            ;
        }

        public static void CreateBSubStates(StateMachine machine)
        {
            machine
                .Create(BSubState.B1, out BSubStates.B1, MainStates.B)
                .Create(BSubState.B2, out BSubStates.B2, MainStates.B)

                .Initial(BSubState.B1, MainStates.B)
            ;
        }

        public static void CreateBTransitions(StateMachine machine)
        {
            machine
                .Create(BSubStates.B1.To(BSubStates.B2), out BSubTransitions.B1_B2)
                .Create(BSubStates.B2.To(BSubStates.B1), out BSubTransitions.B2_B1)
            ;
        }

        public static void CreateBSignals(StateMachine machine)
        {
            machine
                .Create(BSubSignal.ToggleB, BSubTransitions.B1_B2, out BSubSignals.ToggleB)
                .Connect(BSubSignals.ToggleB, BSubTransitions.B2_B1)
            ;
        }

        public static void CreateB2SubStates(StateMachine machine)
        {
            machine
                .Create(B2SubState.B21, out B2SubStates.B21, BSubStates.B2)
                .Create(B2SubState.B22, out B2SubStates.B22, BSubStates.B2)

                .Initial(B2SubState.B21, BSubStates.B2)
            ;
        }

        public static void CreateB2Transitions(StateMachine machine)
        {
            machine
                .Create(B2SubStates.B21.To(B2SubStates.B22), out B2SubTransitions.B21_B22)
                .Create(B2SubStates.B22.To(B2SubStates.B21), out B2SubTransitions.B22_B21)
            ;
        }

        public static void CreateB2Signals(StateMachine machine)
        {
            machine
                .Create(B2SubSignal.ToggleB2, B2SubTransitions.B21_B22, out B2SubSignals.ToggleB2)
                .Connect(B2SubSignals.ToggleB2, B2SubTransitions.B22_B21)
            ;
        }

        public static void ConfigureStateMachine(StateMachine machine, Example example)
        {
            machine
                .OnInitialize(x => Debug.Log("Initialize Machine"))
                .OnStateChange((_, x, y) => Debug.Log($"State Change ({x.Name} --> {y.Name})"))
                .OnTerminate(x => Debug.Log("Terminate Machine"))
                .OnTick(example.UpdateTimeText)
            ;
        }

        public static void ConfigureStates(StateMachine machine, Example example)
        {
            machine
                .Begin(MainStates.A)
                    .On(new NextStateAction(MainSignals.A_B))
                    .OnEnterComplete(example.UpdateMainStateName)
                    .OnTick(example.UpdateMainStateTime)
                .End()
                .Begin(MainStates.B)
                    .On(new BranchedStateAction((KeyCode.C, MainSignals.B_C),
                                                (KeyCode.D, MainSignals.B_D)))
                    .OnEnterComplete(example.UpdateMainStateName)
                    .OnTick(example.UpdateMainStateTime)
                .End()
                .Begin(MainStates.C)
                    .On(new NextStateAction(MainSignals.C_E))
                    .OnEnterComplete(example.UpdateMainStateName)
                    .OnTick(example.UpdateMainStateTime)
                .End()
                .Begin(MainStates.D)
                    .On(new NextStateAction(MainSignals.D_E))
                    .OnEnterComplete(example.UpdateMainStateName)
                    .OnTick(example.UpdateMainStateTime)
                .End()
                .Begin(MainStates.E)
                    .On(new NextStateAction(MainSignals.E_A))
                    .OnEnterComplete(example.UpdateMainStateName)
                    .OnTick(example.UpdateMainStateTime)
                .End()
            ;
        }

        public static void ConfigureTransitions(StateMachine machine, Example example)
        {
            var transitionTime = example.TotalTransitionTime;
            Action<ITransition> onInvalidate = example.OnInvalidateTransition;

            machine
                .Begin(MainTransitions.A_B)
                    .OnStart(example.UpdateMainTransitionName)
                    .OnFinish(example.RemoveMainTransitionName)
                    .OnTick(example.UpdateMainTransitionTime)
                    .FinishWhen(new DelayedTransitionCondition(transitionTime, onInvalidate))
                .End()
                .Begin(MainTransitions.B_C)
                    .OnStart(example.UpdateMainTransitionName)
                    .OnFinish(example.RemoveMainTransitionName)
                    .OnTick(example.UpdateMainTransitionTime)
                    .FinishWhen(new DelayedTransitionCondition(transitionTime, onInvalidate))
                .End()
                .Begin(MainTransitions.B_D)
                    .OnStart(example.UpdateMainTransitionName)
                    .OnFinish(example.RemoveMainTransitionName)
                    .OnTick(example.UpdateMainTransitionTime)
                    .FinishWhen(new DelayedTransitionCondition(transitionTime, onInvalidate))
                .End()
                .Begin(MainTransitions.C_E)
                    .OnStart(example.UpdateMainTransitionName)
                    .OnFinish(example.RemoveMainTransitionName)
                    .OnTick(example.UpdateMainTransitionTime)
                    .FinishWhen(new DelayedTransitionCondition(transitionTime, onInvalidate))
                .End()
                .Begin(MainTransitions.D_E)
                    .OnStart(example.UpdateMainTransitionName)
                    .OnFinish(example.RemoveMainTransitionName)
                    .OnTick(example.UpdateMainTransitionTime)
                    .FinishWhen(new DelayedTransitionCondition(transitionTime, onInvalidate))
                .End()
                .Begin(MainTransitions.E_A)
                    .OnStart(example.UpdateMainTransitionName)
                    .OnFinish(example.RemoveMainTransitionName)
                    .OnTick(example.UpdateMainTransitionTime)
                    .FinishWhen(new DelayedTransitionCondition(transitionTime, onInvalidate))
                .End()
            ;
        }

        public static void ConfigureSignals(StateMachine machine)
        {
            machine
                .Begin(MainSignals.A_B)
                .End()
            ;
        }

        public static void TestMachineStructure(StateMachine machine)
        {
            Debug.Log("Test Machine Structure");

            machine
                .Begin(MainStates.B)
                    .Run(x => Debug.Log($"Enter State [{x.Name}]"))
                    .BeginOrthogonal()
                        .Run(x => Debug.Log($"Enter Orthogonal [{x.Machine.Index}] of State [{x.OuterState.State.Name}]"))
                        .Run(x => Debug.Log("---"))
                        .Begin(BSubStates.B2)
                            .Run(x => Debug.Log($"Enter State: {x.State.Name}"))
                            .BeginOrthogonal()
                                .Run(x => Debug.Log($"Enter Orthogonal [{x.Machine.Index}] of State [{x.OuterState.State.Name}]"))
                                .Run(x => Debug.Log("---"))
                                .Begin(B2SubStates.B21)
                                    .Run(x => Debug.Log($"Inner-most State [{x.State.Name}]"))
                                .End()
                                .Begin(B2SubSignals.ToggleB2)
                                    .Run(x => Debug.Log($"Inner-most Signal [{x.Signal.Name}]"))
                                .End()
                            .EndOrthogonal()
                            .Run(x => Debug.Log("---"))
                            .Run(x => Debug.Log($"Exit State [{x.State.Name}]"))
                        .End()
                    .EndOrthogonal()
                    .Run(x => Debug.Log($"Exit State [{x.State.Name}]"))
                    .ReturnState()
                .End()
            ;
        }
    }

    public static class ObjectExtensions
    {
        public static T Run<T>(this T obj, Action<T> action)
        {
            action?.Invoke(obj);
            return obj;
        }
    }

    public class DefaultStateAction : IStateAction
    {
        public IState State { get; set; }

        void IStateAction.Enter(IState previous)
        {
            Debug.Log($"Enter State [{this.State.Name}]");
        }

        void IStateAction.Exit(IState next)
        {
            Debug.Log($"Exit State [{this.State.Name}]");
        }

        void IStateAction.EnterComplete(IState previous)
        {
            Debug.Log($"Complete Enter State [{this.State.Name}]");
            Debug.Log("---");
        }

        void IStateAction.Resume(IState next)
        {
            Debug.Log($"Resume State [{this.State.Name}]");
        }

        void IStateAction.Terminate()
        {
            Debug.Log($"Terminate State [{this.State.Name}]");
        }
    }

    public class NextStateAction : DefaultStateAction, ITickable
    {
        private readonly Signal nextStateSignal;
        private readonly KeyCode nextStateKey;

        public NextStateAction(Signal nextStateSignal, KeyCode? nextStateKey = null)
        {
            this.nextStateSignal = nextStateSignal;
            this.nextStateKey = nextStateKey ?? KeyCode.Space;
        }

        public void Tick()
        {
            if (Input.GetKeyUp(this.nextStateKey))
                this.nextStateSignal?.Emit();
        }
    }

    public class BranchedStateAction : DefaultStateAction, ITickable
    {
        private readonly Dictionary<KeyCode, Signal> signalMap = new Dictionary<KeyCode, Signal>();

        public BranchedStateAction(params (KeyCode key, Signal signal)[] args)
        {
            foreach (var (key, signal) in args)
            {
                this.signalMap.Add(key, signal);
            }
        }

        public void Tick()
        {
            foreach (var kv in this.signalMap)
            {
                if (Input.GetKeyUp(kv.Key))
                    kv.Value?.Emit();
            }
        }
    }

    public class DelayedTransitionCondition : ITransitionCondition
    {
        private readonly float delay;
        private readonly Action<ITransition> onInvalidate;

        private float elapsed;

        public DelayedTransitionCondition(float delay, Action<ITransition> onInvalidate)
        {
            this.delay = delay;
            this.onInvalidate = onInvalidate;
        }

        bool ITransitionCondition.Validate(ITransition transition)
        {
            this.elapsed += Time.deltaTime;
            return this.elapsed >= this.delay;
        }

        void ITransitionCondition.Invalidate(ITransition transition)
        {
            this.elapsed = 0f;
            this.onInvalidate(transition);
        }
    }
}