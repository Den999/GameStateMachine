# GameStateMachine
Flexible and configurable game state machine for Unity. (finite state machine).
- Supports custom game states. You can create them!
- Easy to use (you don`t need to usubsrcibe, it will be happen automatically)
- No singletons and static classes.

Use sample:
```csharp
private void Start()
{
    // Find a game state machine object
    GameStateMachine = FindObjectOfType<GameStateMachine>();

    // On win state show window. Note, there is no need to unsubscribe!
    GameStateMachine.AdjustActionToState<WinState>(gameObject, ShowWindow);

    // Push running state
    GameStateMachine.PushState(new RunningState());

    // Check for the last state and get info about it
    if (GameStateMachine.Last.IsGameActiveDuringState)
    {
        Debug.Log("The game is active! It is not a pause or postgame");
    }

    // Push your custom state
    GameStateMachine.PushState(new MyCustomState());
}

// Create your own states!
public class MyCustomState : GameState
{
    // Which states can be after our custom state?
    public override GameState[] PossibleNextStates => GameState[]
    {
        new RunningState(),
        new WinState(),
        new LoseState(),
    };

    // The state is not active (like pause, etc.)
    public override bool IsGameActiveDuringState => false;
}
```
