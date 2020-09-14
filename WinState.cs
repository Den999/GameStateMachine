using System.Collections.Generic;

namespace D2D.Core
{
    /// <summary>
    /// Player complete the level
    /// </summary>
    public class WinState : GameState
    {
        public override List<GameState> PossibleNextStates => new List<GameState>() 
        {
            new PostgameState()
        };
        public override bool IsGameActiveDuringState => false;
    }
}