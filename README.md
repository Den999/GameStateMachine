# GameStateMachine
- Lightweight and have NO dependecies.
- Supports custom game states. You can create them!
- Easy to use and safe (no scene setup, all you need will be created automatically at runtime)
- No singletons and static classes.

## Basic usage:
```csharp
public class Sample : MonoBehaviour
{
    private void Start()
    {
        // Just like a FindObjectOfType, but if it is not exists => create it
        // (better singleton version)
        var stateMachine = this.FindLazy<GameStateMachine>();

        // Subscribing
        stateMachine.On<RunningState>(() => Debug.Log("Running"));
        stateMachine.On<LoseState, WinState>(() => Debug.Log("The game was finished (lose or win)"));

        // Pushing states
        stateMachine.Push(new RunningState());
        stateMachine.Push(new WinState());
    }
}
```

## Common use cases:
```csharp
// On player trigger the finish => push WinState
// GameStateMachineUser hashes StateMachine for us :)
public class Finish : GameStateMachineUser
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player p))
            StateMachine.Push(new WinState());
    }
}

// OnGameFinish will be called on WinState or LoseState (from GameStateMachineUser) 
public class PlayerMovement : GameStateMachineUser
{
    private float _speedFactor = 1;

    protected override void OnGameFinish()
    {
        _speedFactor = 0;
    }
}
```

## Custom states be like:
```csharp
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
