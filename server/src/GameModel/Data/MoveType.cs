namespace GameModel.Data
{
    /// <summary>
    /// The type of the result of a move
    /// </summary>
    public enum MoveType
    {
        /// <summary>
        /// A valid move that resulted in a simple piece translation
        /// </summary>
        Move,
        /// <summary>
        /// A valid move that resulted in a piece capture
        /// </summary>
        Capture,
        /// <summary>
        /// An invalid move
        /// </summary>
        Failure
    }
}
