using System;
using System.Collections.Generic;

namespace D2D.Core
{
    /// <summary>
    /// Game paused (all active objects were frozen)
    /// </summary>
    public class PauseState : GameState
    {
        protected override GameState[] PossibleNextStates => new GameState[] 
        {
            new RunningState(), 
        };
        public override bool IsGameActiveDuringState => false;
    }
}