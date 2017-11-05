namespace GameModel.Data
{
    /// <summary>
    /// Represents whether a position is empty, occupied by an enemy, or occupied by 
    /// a friendly piece
    /// </summary>
    public enum SpaceStatus
    {
        /// <summary>
        /// Space was empty
        /// </summary>
        Empty,
        /// <summary>
        /// Space is occupied by a friendly piece
        /// </summary>
        Friendly,
        /// <summary>
        /// Space is occupied by an enemy piece
        /// </summary>
        Enemy,
        /// <summary>
        /// Space is invalid
        /// </summary>
        Void
    }
}
