namespace GameModel {
    /// <summary>
    /// Represents the outcome of the movement of a piece
    /// </summary>
    public interface IMoveResult
    {
        BoardPosition Destination {get;}
    }
}