# Unity.QuaStateMachine

A Unity package for [QuaStateMachine](https://github.com/qua11q7/QuaStateMachine) which also contains many improvements.

> QuaStateMachine framework allows you to convert state machine diagram to code. It supports orthogonal and inner states.

## Example

The [Example](https://github.com/laicasaane/Unity.QuaStateMachine/tree/master/Example) shows how to create and tick a state machine in Unity.

Its diagram is shown below:

![ExampleMachine.png](https://raw.githubusercontent.com/laicasaane/Unity.QuaStateMachine/master/Docs/ExampleMachine.png)

In this sample diagram, there are 9 *States* and 7 *Signals*:

Main states

- A
- B
- C
- D
- E

Inner states of **B**
- B1
- B2

Inner states of **B2**
- B21
- B22

Signals

- A_B
- B_C
- B_D
- C_E
- D_E
- B1_B2
- B21_B22

All of the states are connected to each other with *Transitions*. Transitions can be triggered via emitting the corresponding *Signal*. In the diagram above, 10 transitions can be seen. Two of the transitions indicate that **Active** and **Stopped** states should be the initial state of their corresponding level. There are two transitions between **Stopped** and **Running** states, one leading from **Stopped** to **Running** and the other leading from **Running** to **Stopped**. These two different transitions are connected to the same signal, **StartStop**. If the current state is **Stopped** and the **StartStop** signal is emitted, state machine will change its state to **Running** state. If the current state is **Started** and the **StartStop** signal is emitted, state machine then will change its state to **Stopped** state.  There cannot be a transition without a connected signal.
