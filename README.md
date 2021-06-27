# GameStateMachine
Flexible and configurable game state machine for Unity. (finite state machine).
- Lightweight and have NO dependecies.
- Supports custom game states. You can create them!
- Easy to use and safe (you don`t need to usubsrcibe, it will be happen automatically)
- No singletons and static classes.

Use sample:
```csharp
public class Sample : MonoBehaviour
{
    private void Start()
    {
        // Just like a FindObjectOfType, but if it is not exists => create it
        // (better singleton version)
        var gsm = this.FindLazy<GameStateMachine>();

        gsm.On<RunningState>(() => Debug.Log("Running"));

        gsm.Push(new RunningState());
    }
}
    
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
