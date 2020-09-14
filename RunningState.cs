using System.Collections.Generic;

namespace D2D.Core
{
    /// <summary>
    /// Game is running, player is trying to complete current level
    /// </summary>
    public class RunningState : GameState
    {
        public override List<GameState> PossibleNextStates => new List<GameState>() 
        {
            new PauseState(),
            new WinState(),
            new LoseState(),
        };
    }
}