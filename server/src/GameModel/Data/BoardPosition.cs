namespace GameModel.Data {
    /// <summary>
    /// Represents a position on the board
    /// </summary>
    public struct BoardPosition {
        /// <summary>
        /// Initializes a BoardPosition
        /// </summary>
        /// <param name="x">The horizontal index</param>
        /// <param name="y">The vertical index</param>
        public BoardPosition(XCoord x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Checks structual equality of two BoardPosition instances
        /// </summary>
        /// <param name="first">A BoardPosition instance</param>
        /// <param name="second">A BoardPosition instance</param>
        /// <returns>true if the parameters are equal</returns>
        public static bool operator == (BoardPosition first, BoardPosition second)
        {
            return first.X == second.X && first.Y == second.Y;
        }

        /// <summary>
        /// Checks if two BoardPosition instances are not structurally equal
        /// </summary>
        /// <param name="first">A BoardPosition instance</param>
        /// <param name="second">A BoardPosition instance</param>
        /// <returns>true if the parameters are not equal</returns>
        public static bool operator != (BoardPosition first, BoardPosition second)
        {
            return !(first == second);
        }

        /// <summary>
        /// Adds a PositionDelta offset to a BoardPosition
        /// </summary>
        /// <param name="orig">The BoardPosition to start with</param>
        /// <param name="offset">The offset</param>
        /// <returns>A new BoardPosition that is offset</returns>
        public static BoardPosition operator + (BoardPosition orig, PositionDelta offset)
        {
            return new BoardPosition(orig.X + offset.X, orig.Y + offset.Y);
        }

        /// <summary>
        /// Implements structural equality
        /// </summary>
        /// <param name="obj">The other object to compare to</param>
        /// <returns>true if the other object has the same value as this one</returns>
        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case BoardPosition other:
                    return other.X == X && other.Y == Y;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Gets the determinsitic hash code
        /// </summary>
        /// <returns>A integer that corresponds with these values</returns>
        public override int GetHashCode() => (int)X ^ Y;

        /// <summary>
        /// The horizontal coordinate
        /// </summary>
        public XCoord X { get; }

        /// <summary>
        /// The vertical coordinate
        /// </summary>
        public int Y { get; }
    }
}