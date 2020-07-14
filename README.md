# Unity.QuaStateMachine

A Unity package for [QuaStateMachine](https://github.com/qua11q7/QuaStateMachine) which also contains many improvements.

> QuaStateMachine framework allows you to convert state machine diagram to code. It supports orthogonal and inner states.

## Example

The [Example](https://github.com/laicasaane/Unity.QuaStateMachine/tree/master/Example) shows how to create and tick a state machine in Unity.

Its diagram is shown below:

![ExampleMachine.png](https://raw.githubusercontent.com/laicasaane/Unity.QuaStateMachine/master/Docs/ExampleMachine.png)

First we define the state names. In this example, state names are strings for simplicity. However you can use any type for state names.

```csharp
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
```

Then we create the machine and its actual states.

```csharp
this.machine = new StateMachine();

// [...]

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
```

Every two states should be connected by a *Transition*.

```csharp
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
```

To start a transition, their must be at least one *Signal* connected to it.

```csharp
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
```

The same for the sub states.

**Note:** One signal can be used for many transitions. So instead of creating new signal, we can connect an existing signal to the transitions.

```csharp
public static class BSubSignal
{
    public const string ToggleB = nameof(ToggleB);
}

// [...]

public static void CreateBSignals(StateMachine machine)
{
    machine
        .Create(BSubSignal.ToggleB, BSubTransitions.B1_B2, out BSubSignals.ToggleB)
        .Connect(BSubSignals.ToggleB, BSubTransitions.B2_B1)
    ;
}
```