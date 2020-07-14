using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QuaStateMachine.Examples
{
    public class Example : MonoBehaviour
    {
        [SerializeField]
        private float totalTransitionTime = 1f;

        [Space]
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
        private List<KeyedGameObject> mainStates = null;

        [SerializeField]
        private List<KeyedGameObject> bSubStates = null;

        [SerializeField]
        private List<KeyedGameObject> b2SubStates = null;

        [Space]
        [SerializeField]
        private List<KeyedSlider> mainTransitions = null;

        public float TotalTransitionTime
            => this.totalTransitionTime;

        private readonly Dictionary<string, GameObject> mainStateMap = new Dictionary<string, GameObject>();
        private readonly Dictionary<string, GameObject> bSubStateMap = new Dictionary<string, GameObject>();
        private readonly Dictionary<string, GameObject> b2SubStateMap = new Dictionary<string, GameObject>();

        private readonly Dictionary<string, Slider> mainTransitionMap = new Dictionary<string, Slider>();

        private StateMachine machine;
        private float stateEnterTime;
        private float transitionTime;

        private Slider mainSlider;

        private void Awake()
        {
            PrepareMap(this.mainStates, this.mainStateMap);
            PrepareMap(this.bSubStates, this.bSubStateMap);
            PrepareMap(this.b2SubStates, this.b2SubStateMap);

            PrepareMap(this.mainTransitions, this.mainTransitionMap);

            HideAll(this.mainStates);
            HideAll(this.bSubStates);
            HideAll(this.b2SubStates);

            ResetAll(this.mainTransitions);
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

            HideAll(this.mainStates, stateName);

            if (this.mainStateMap.TryGetValue(stateName, out var go))
                go.SetActive(true);
        }

        public void UpdateMainStateTime(IStateAction _)
        {
            this.stateTimeText.text = $"{Time.time - this.stateEnterTime}";
        }

        public void UpdateMainTransitionName(ITransitionAction action, TransitionArgs _)
        {
            var transitionName = $"{action.Transition.Name}";
            this.transitionNameText.text = transitionName;
            this.transitionTime = Time.time;
            this.mainSlider = null;

            ResetAll(this.mainTransitions, transitionName);

            if (this.mainTransitionMap.TryGetValue(transitionName, out var slider))
                this.mainSlider = slider;
        }

        public void RemoveMainTransitionName(ITransitionAction _)
        {
            this.transitionNameText.text = string.Empty;
        }

        public void UpdateMainTransitionTime(ITransitionAction _)
        {
            var elapsed = Time.time - this.transitionTime;
            this.transitionTimeText.text = $"{elapsed}";

            if (this.mainSlider)
            {
                this.mainSlider.value = elapsed / this.totalTransitionTime;
            }
        }

        public void OnInvalidateTransition(ITransition _)
        {
        }

        [Serializable]
        private sealed class KeyedGameObject
        {
            public string Key = string.Empty;
            public GameObject Value = null;
        }

        [Serializable]
        private sealed class KeyedSlider
        {
            public string Key = string.Empty;
            public Slider Value = null;
        }

        private void PrepareMap(List<KeyedGameObject> list, Dictionary<string, GameObject> map)
        {
            foreach (var kv in list)
            {
                map[kv.Key] = kv.Value;
            }
        }

        private void HideAll(List<KeyedGameObject> list, string except = null)
        {
            foreach (var kv in list)
            {
                if (!string.IsNullOrEmpty(except) && string.Equals(kv.Key, except))
                    continue;

                kv.Value.SetActive(false);
            }
        }

        private void PrepareMap(List<KeyedSlider> list, Dictionary<string, Slider> map)
        {
            foreach (var kv in list)
            {
                map[kv.Key] = kv.Value;
            }
        }

        private void ResetAll(List<KeyedSlider> list, string except = null)
        {
            foreach (var kv in list)
            {
                if (!string.IsNullOrEmpty(except) && string.Equals(kv.Key, except))
                    continue;

                kv.Value.value = 0f;
            }
        }
    }
}
