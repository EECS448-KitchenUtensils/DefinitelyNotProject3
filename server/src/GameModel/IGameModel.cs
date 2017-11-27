using GameModel.Data;
using System.Collections.Generic;

namespace GameModel
{
    /// <summary>
    /// Interface to mock a ChessGame instance
    /// </summary>
    public interface IGameModel
    {
        /// <summary>
        /// Generates all the possible moves of a piece
        /// </summary>
        /// <param name="pos">Position of the piece to query</param>
        /// <returns>A sequence of moves and their type</returns>
        IEnumerable<MoveResult> PossibleMoves(BoardPosition pos);

        /// <summary>
        /// Looks up a ChessPiece by position on the board
        /// </summary>
        /// <param name="pos">The position the query</param>
        /// <returns>A ChessPiece reference or null</returns>
        ChessPiece GetPieceByPosition(BoardPosition pos);

        /// <summary>
        /// Enumerates all of the pieces on the board
        /// </summary>
        IEnumerable<ChessPiece> Pieces { get; }

        /// <summary>
        /// Attempts to move a piece from position to another
        /// Handles capturing, bad moves, and advancing the turn counter on success
        /// </summary>
        /// <param name="src">The current position of a piece</param>
        /// <param name="dest">The intended destination of the piece</param>
        /// <returns>Failure on invalid moves, Move on valid moves, Capture on valid Captures</returns>
        MoveResult MakeMove(BoardPosition src, BoardPosition dest);
    }
}
