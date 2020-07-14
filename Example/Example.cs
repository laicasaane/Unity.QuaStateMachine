using System;
using System.Collections.Generic;
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

        [Space]
        [SerializeField]
        private List<KeyValue> mainStates = null;

        [SerializeField]
        private List<KeyValue> bSubStates = null;

        [SerializeField]
        private List<KeyValue> b2SubStates = null;

        private readonly Dictionary<string, GameObject> mainStateMap = new Dictionary<string, GameObject>();
        private readonly Dictionary<string, GameObject> bSubStateMap = new Dictionary<string, GameObject>();
        private readonly Dictionary<string, GameObject> b2SubStateMap = new Dictionary<string, GameObject>();

        private StateMachine machine;
        private float stateEnterTime;
        private float transitionTime;

        private void Awake()
        {
            PrepareMap(this.mainStates, this.mainStateMap);
            PrepareMap(this.bSubStates, this.bSubStateMap);
            PrepareMap(this.b2SubStates, this.b2SubStateMap);

            HideAll(this.mainStates);
            HideAll(this.bSubStates);
            HideAll(this.b2SubStates);
        }

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

        public void UpdateMainStateName(IStateAction action)
        {
            var stateName = $"{action.State.Name}";
            this.stateNameText.text = stateName;
            this.stateEnterTime = Time.time;

            HideAll(this.mainStates);

            if (this.mainStateMap.TryGetValue(stateName, out var go))
                go.SetActive(true);
        }

        public void UpdateMainStateTime(IStateAction _)
        {
            this.stateTimeText.text = $"{Time.time - this.stateEnterTime}";
        }

        public void UpdateMainTransitionName(ITransitionAction action, TransitionArgs _)
        {
            this.transitionNameText.text = $"{action.Transition.Name}";
            this.transitionTime = Time.time;
        }

        public void RemoveMainTransitionName(ITransitionAction _)
        {
            this.transitionNameText.text = string.Empty;
        }

        public void UpdateMainTransitionTime(ITransitionAction _)
        {
            this.transitionTimeText.text = $"{Time.time - this.transitionTime}";
        }

        public void OnInvalidateTransition(ITransition transition)
        {

        }

        [Serializable]
        private sealed class KeyValue
        {
            public string Key;
            public GameObject Value;
        }

        private void PrepareMap(List<KeyValue> list, Dictionary<string, GameObject> map)
        {
            foreach (var kv in list)
            {
                map[kv.Key] = kv.Value;
            }
        }

        private void HideAll(List<KeyValue> list)
        {
            foreach (var kv in list)
            {
                kv.Value.SetActive(false);
            }
        }
    }
}
