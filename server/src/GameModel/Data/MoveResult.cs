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
        /// <param name="dest">The destination position of this move</param>
        /// <param name="outcome">The outcome of this move</param>
        public MoveResult(BoardPosition src, BoardPosition dest, MoveType outcome)
        {
            Source = src;
            Destination = dest;
            Outcome = outcome;
        }

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

        public override string ToString() =>
            string.Format("src={0}, dest={1}, outcome={2}", Source, Destination, Outcome);
    }
}