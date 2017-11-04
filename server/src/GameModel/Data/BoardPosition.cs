namespace GameModel.Data {
    /// <summary>
    /// Represents a position on the board
    /// </summary>
    public struct BoardPosition {

        public BoardPosition(XCoord x, int y)
        {
            X = x;
            Y = y;
        }
        public static bool operator == (BoardPosition first, BoardPosition second)
        {
            return first.X == second.X && first.Y == second.Y;
        }
        public static bool operator != (BoardPosition first, BoardPosition second)
        {
            return !(first == second);
        }
        public static BoardPosition operator + (BoardPosition orig, PositionDelta offset)
        {
            return new BoardPosition(orig.X + offset.X, orig.Y + offset.Y);
        }
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
        public override int GetHashCode() => (int)X ^ Y;
        public XCoord X { get; }
        public int Y { get; }
    }
}