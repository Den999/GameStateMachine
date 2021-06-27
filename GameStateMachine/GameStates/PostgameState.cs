namespace D2D.Core
{
    /// <summary>
    /// Player have clicked next level button or menu button and scene transitions started
    /// (if they exists)
    /// </summary>
    public class PostgameState : GameState
    {
        // Postgame state is a last state => no next states possible
        public override bool IsNextStatePossible(GameState nextState)
        {
            return false;
        }
        
        public override bool IsGameActiveDuringState => false;
        
        public override bool CanBeFirstState => true;
    }
}