using System.Collections.Generic;

namespace D2D.Core
{
    /// <summary>
    /// Game is running, player is trying to complete current level
    /// </summary>
    public class RunningState : GameState
    {
        protected override GameState[] PossibleNextStates => new GameState[] 
        {
            new PauseState(),
            new WinState(),
            new LoseState(),
        };

        public override bool IsGameActiveDuringState => true;
    }
}