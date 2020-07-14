using UnityEngine;
using UnityEngine.UI;

namespace QuaStateMachine.Examples
{
    public class Example : MonoBehaviour
    {
        [SerializeField]
        private Text stateNameText = null;

        [SerializeField]
        private Text stateTimeText = null;

        [SerializeField]
        private Text transitionNameText = null;

        [SerializeField]
        private Text transitionTimeText = null;

        [SerializeField]
        private Text machineTimeText = null;

        private StateMachine machine;
        private float stateEnterTime;
        private float transitionTime;

        private void Start()
        {
            this.machine = new StateMachine();

            ExampleMachine.CreateStates(this.machine);
            ExampleMachine.CreateTransitions(this.machine);
            ExampleMachine.CreateSignals(this.machine);

            ExampleMachine.CreateBSubStates(this.machine);
            ExampleMachine.CreateBTransitions(this.machine);
            ExampleMachine.CreateBSignals(this.machine);

            ExampleMachine.CreateB2SubStates(this.machine);
            ExampleMachine.CreateB2Transitions(this.machine);
            ExampleMachine.CreateB2Signals(this.machine);

            ExampleMachine.TestMachineStructure(this.machine);

            ExampleMachine.ConfigureStateMachine(this.machine, this);
            ExampleMachine.ConfigureStates(this.machine, this);
            ExampleMachine.ConfigureTransitions(this.machine, this);
            ExampleMachine.ConfigureSignals(this.machine);

            Debug.Log("---");

            this.machine.Build();
        }

        private void Update()
        {
            this.machine.Tick();
        }

        public void UpdateTimeText(IStateMachineAction _)
        {
            this.machineTimeText.text = $"{Time.timeSinceLevelLoad}";
        }

        public void UpdateStateName(IStateAction action)
        {
            this.stateNameText.text = $"{action.State.Name}";
            this.stateEnterTime = Time.time;
        }

        public void UpdateStateTime(IStateAction _)
        {
            this.stateTimeText.text = $"{Time.time - this.stateEnterTime}";
        }

        public void UpdateTransitionName(ITransitionAction action, TransitionArgs _)
        {
            this.transitionNameText.text = $"{action.Transition.Name}";
            this.transitionTime = Time.time;
        }

        public void RemoveTransitionName(ITransitionAction _)
        {
            this.transitionNameText.text = string.Empty;
        }

        public void UpdateTransitionTime(ITransitionAction _)
        {
            this.transitionTimeText.text = $"{Time.time - this.transitionTime}";
        }
    }
}
