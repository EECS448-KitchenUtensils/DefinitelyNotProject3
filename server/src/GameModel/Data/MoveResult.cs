namespace GameModel.Data
{
    /// <summary>
    /// Represents the result of a piece movement
    /// </summary>
    public struct MoveResult
    {
        /// <summary>
        /// Creates new MoveResult
        /// </summary>
        /// <param name="pos">The destination position of this move</param>
        /// <param name="outcome">The outcome of this move</param>
        public MoveResult(BoardPosition pos, MoveType outcome)
        {
            Position = pos;
            Outcome = outcome;
        }
        
        /// <summary>
        /// The destination position of this move
        /// </summary>
        public BoardPosition Position { get; }

        /// <summary>
        /// The outcome of this move
        /// </summary>
        public MoveType Outcome { get; }
    }
}