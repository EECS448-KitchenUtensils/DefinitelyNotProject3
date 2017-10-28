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
        public XCoord x;
        public int y;
    }
}