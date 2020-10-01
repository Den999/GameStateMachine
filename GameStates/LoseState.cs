using System;
using System.Collections.Generic;

namespace D2D.Core
{
    /// <summary>
    /// Player died or time expired
    /// </summary>
    public class LoseState : GameState
    {
        protected override GameState[] PossibleNextStates => new GameState[] 
        {
            new PostgameState(),
        };
        public override bool IsGameActiveDuringState => false;
    }
}