namespace GameModel.Data
{
    /// <summary>
    /// Represents whether a position is empty, occupied by an enemy, or occupied by 
    /// a friendly piece
    /// </summary>
    public enum SpaceStatus
    {
        Empty,
        Friendly,
        Enemy,
        Void
    }
}
