namespace GameModel {
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