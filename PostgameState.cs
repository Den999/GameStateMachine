namespace D2D.Core
{
    /// <summary>
    /// Player have clicked next level button or menu button and scene transitions started
    /// (if they exists)
    /// </summary>
    public class PostgameState : GameState
    {
        public override bool IsGameActiveDuringState => false;
    }
}