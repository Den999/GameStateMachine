using System.Collections.Generic;

namespace D2D.Core
{
    /// <summary>
    /// Player died or time expired
    /// </summary>
    public class LoseState : GameState
    {
        public override List<GameState> PossibleNextStates => new List<GameState>() 
        {
            new PostgameState()
        };
        public override bool IsGameActiveDuringState => false;
    }
}