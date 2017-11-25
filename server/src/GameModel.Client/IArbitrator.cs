using GameModel.Data;
using GameModel.Messages;
using System.Collections.Generic;

namespace GameModel
{
    public interface IArbitrator
    {
        /// <summary>
        /// Generates all the possible moves of a piece
        /// </summary>
        /// <param name="pos">Position of the piece to query</param>
        /// <returns>A sequence of moves and their type</returns>
        IEnumerable<MoveResult> PossibleMoves(BoardPosition pos);

        /// <summary>
        /// Attempts to move a piece from position to another
        /// Handles capturing, bad moves, and advancing the turn counter on success
        /// </summary>
        /// <param name="src">The current position of a piece</param>
        /// <param name="dest">The intended destination of the piece</param>
        /// <returns>Failure on invalid moves, Move on valid moves, Capture on valid Captures</returns>
        MoveType MakeMove(BoardPosition src, BoardPosition dest);

        /// <summary>
        /// Signals that a given player is forfeiting the game
        /// </summary>
        /// <param name="player">Which player to forfeit for</param>
        void Forfeit(Player player);

        /// <summary>
        /// Stops the game (completely for local, connection for remote)
        /// </summary>
        void Shutdown();

        /// <summary>
        /// Attempts to get the latest message from the queue with no waiting
        /// </summary>
        /// <param name="message">A retrieved message</param>
        /// <returns>true if a message was retrieved, else false</returns>
        bool TryGetLatestMessage(out ModelMessage message);
    }
}
