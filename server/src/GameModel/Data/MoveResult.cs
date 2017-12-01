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
        /// <param name="src">The source position of this move</param>
        /// <param name="dest">The destination position of this move</param>
        /// <param name="outcome">The outcome of this move</param>
        public MoveResult(BoardPosition src, BoardPosition dest, MoveType outcome)
        {
            Source = src;
            Destination = dest;
            Outcome = outcome;
            Destroyed = null;
        }

        /// <summary>
        /// Alternate constructor for when a move results in a piece being removed from the board
        /// </summary>
        /// <param name="src">The source positon of this move</param>
        /// <param name="dest">The destination position of this move</param>
        /// <param name="outcome">The outcome of this move</param>
        /// <param name="destroyed">The piece that was removed from play by this move</param>
        public MoveResult(BoardPosition src, BoardPosition dest, MoveType outcome, ChessPiece destroyed): this(src, dest, outcome)
        {
            Destroyed = destroyed;
        }

        /// <summary>
        /// The source position of this move
        /// </summary>
        public BoardPosition Source { get; }

        /// <summary>
        /// The destination position of this move
        /// </summary>
        public BoardPosition Destination { get; }

        /// <summary>
        /// The outcome of this move
        /// </summary>
        public MoveType Outcome { get; }

        /// <summary>
        /// A piece destroyed (captured) by this move
        /// </summary>
        public ChessPiece Destroyed { get; }

        /// <summary>
        /// Creates a <see cref="System.String"/> representation for debugging purposes
        /// </summary>
        /// <returns>A debugging-quality representation of the properties in this <see cref="MoveResult"/></returns>
        public override string ToString() =>
            string.Format("src={0}, dest={1}, outcome={2}", Source, Destination, Outcome);
    }
}