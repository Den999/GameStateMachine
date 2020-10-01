using System;
using System.Collections.Generic;

namespace D2D.Core
{
    /// <summary>
    /// Determines some type of game state
    /// </summary>
    public abstract class GameState
    {
        /// <summary>
        /// Contains all states which can go after this state
        /// </summary>
        protected virtual GameState[] PossibleNextStates => new GameState[]{};

        public virtual bool IsNextStatePossible(GameState nextState)
        {
            foreach (var possibleNextState in PossibleNextStates)
            {
                if (nextState.GetType() == possibleNextState.GetType())
                    return true;
            }

            return false;
        }

        /// <summary>
        /// PauseState sets it to false
        /// </summary>
        public abstract bool IsGameActiveDuringState { get; }
    }
}