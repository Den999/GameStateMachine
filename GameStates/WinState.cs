using System.Collections.Generic;

namespace D2D.Core
{
    /// <summary>
    /// Player complete the level
    /// </summary>
    public class WinState : GameState
    {
        protected override GameState[] PossibleNextStates => new GameState[] 
        {
            new PostgameState()
        };
        public override bool IsGameActiveDuringState => false;
    }
}