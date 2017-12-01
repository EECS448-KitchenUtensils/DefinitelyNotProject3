namespace GameModel.Data
{
    /// <summary>
    /// Enum used to identify pieces that have been serialized. This is 
    /// so the entire <see cref="ChessPiece"/> hierarchy doesn't have to be
    /// serialized.
    /// </summary>
    public enum PieceEnum
    {
        /// <summary>
        /// Represents a <see cref="Pawn"/>
        /// </summary>
        PAWN,
        /// <summary>
        /// Represents a <see cref="Bishop"/>
        /// </summary>
        BISHOP,
        /// <summary>
        /// Represents a <see cref="Knight"/>
        /// </summary>
        KNIGHT,
        /// <summary>
        /// Represents a <see cref="Rook"/>
        /// </summary>
        ROOK,
        /// <summary>
        /// Represents a <see cref="Queen"/>
        /// </summary>
        QUEEN,
        /// <summary>
        /// Represents a <see cref="King"/>
        /// </summary>
        KING
    }
}
