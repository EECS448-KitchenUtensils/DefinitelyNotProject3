namespace GameModel.Data {
    /// <summary>
    /// Represents a position on the board
    /// </summary>
    public struct BoardPosition {

        public BoardPosition(XCoord x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public static bool operator == (BoardPosition first, BoardPosition second)
        {
            return first.x == second.x && first.y == second.y;
        }
        public static bool operator != (BoardPosition first, BoardPosition second)
        {
            return !(first == second);
        }
        public static BoardPosition operator + (BoardPosition orig, PositionDelta offset)
        {
            return new BoardPosition(orig.x + offset.X, orig.y + offset.Y);
        }
        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case BoardPosition other:
                    return other.x == x && other.y == y;
                default:
                    return false;
            }
        }
        public override int GetHashCode() => (int)x ^ y;
        public XCoord x;
        public int y;
    }
}