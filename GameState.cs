using System.Collections.Generic;

namespace D2D.Core
{
    /// <summary>
    /// Determines some type of game state
    /// </summary>
    public class GameState
    {
        /// <summary>
        /// Contains all states which can go after this state
        /// </summary>
        public virtual List<GameState> PossibleNextStates => new List<GameState>();
        
        /// <summary>
        /// PauseState sets it to false
        /// </summary>
        public virtual bool IsGameActiveDuringState => true;
    }
}