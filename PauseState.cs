using System.Collections.Generic;

namespace D2D.Core
{
    /// <summary>
    /// Game paused (all active objects were frozen)
    /// </summary>
    public class PauseState : GameState
    {
        public override List<GameState> PossibleNextStates => new List<GameState>() 
        {
            new RunningState()
        };
        public override bool IsGameActiveDuringState => false;
    }
}