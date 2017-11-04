namespace GameModel.Data
{
    /// <summary>
    /// Holds a position difference for translating a piece on the board
    /// </summary>
    public struct PositionDelta 
    {
        public PositionDelta(int xDiff, int yDiff)
        {
            X = xDiff;
            Y = yDiff;
        }
        public override int GetHashCode() => X ^ Y;
        public int X;
        public int Y;
    }
}