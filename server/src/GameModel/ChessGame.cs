using GameModel.Data;
using System.Collections.Generic;
using System.Linq;

namespace GameModel
{
    /// <summary>
    /// The main model class. Manages the state of the entire game.
    /// </summary>
    public class ChessGame
    {
        /// <summary>
        /// Default constructor for ChessGame
        /// </summary>
        public ChessGame()
        {
            _players = new Player[4];
            _players[0] = new Player(PlayerEnum.PLAYER_1);
            _players[1] = new Player(PlayerEnum.PLAYER_2);
            _players[2] = new Player(PlayerEnum.PLAYER_3);
            _players[3] = new Player(PlayerEnum.PLAYER_4);
            _board = new ChessBoard(_players);
            _current_player = 0;
        }
        /// <summary>
        /// Generates all the possible moves of a piece
        /// </summary>
        /// <param name="pos">Position of the piece to query</param>
        /// <returns>A sequence of moves and their type</returns>
        public IEnumerable<MoveResult> PossibleMoves(BoardPosition pos) => _board.PossibleMoves(pos);

        /// <summary>
        /// Looks up a ChessPiece by position on the board
        /// </summary>
        /// <param name="pos">The position the query</param>
        /// <returns>A ChessPiece reference or null</returns>
        public ChessPiece GetPieceByPosition(BoardPosition pos) => _board.GetPieceByPosition(pos);

        /// <summary>
        /// Enumerates all of the pieces on the board
        /// </summary>
        public IEnumerable<ChessPiece> Pieces => _board.Pieces;

        /// <summary>
        /// Attempts to move a piece from position to another
        /// Handles capturing, bad moves, and advancing the turn counter on success
        /// </summary>
        /// <param name="src">The current position of a piece</param>
        /// <param name="dest">The intended destination of the piece</param>
        /// <returns>Failure on invalid moves, Move on valid moves, Capture on valid Captures</returns>
        public MoveResult MakeMove(BoardPosition src, BoardPosition dest)
        {
            bool checkPositions() => (ChessBoard.CheckPositionExists(src) && ChessBoard.CheckPositionExists(dest));
            //Source and Destination must be valid
            if (checkPositions() == false)
                return new MoveResult(src, dest, MoveType.Failure);
            var currentPlayer = _players[_current_player];
            var piece = _board.GetPieceByPosition(src);
            //Check piece ownership
            if (piece.Owner != currentPlayer)
                return new MoveResult(src, dest, MoveType.Failure);
            // ToList or equivalent must be called since this enumerable will be used multiple times
            var moves = _board.PossibleMoves(src)
                              .ToList();
            //Check there is a valid move from source to destination for this piece
            if (moves.Count(move => move.Destination == dest) == 1)
            {
                var pieceAtDest = _board.GetPieceByPosition(dest);
                // Update Piece position
                piece.Position = dest;

                _current_player = (_current_player + 1) % 4; //Advance next player, mod 4
                if (moves[0].Outcome == MoveType.Capture)
                    _board.RemovePiece(pieceAtDest);
                // check for checks
                var playersInCheck = _board.PossibleMoves(dest)
                                   .Where(move => move.Outcome == MoveType.Capture)
                                   .Select(move => GetPieceByPosition(move.Destination))
                                   .Where(p => p is King)
                                   .Select(p => p.Owner);

                // process each check that was found
                foreach (var player in playersInCheck)
                {
                    player.Checked = true;
                }
                return new MoveResult(src, dest, moves[0].Outcome);
            }
            return new MoveResult(src, dest, MoveType.Failure);
        }

        /// <summary>
        /// Gets which player owns the current turn
        /// </summary>
        /// <returns>The active player</returns>
        public PlayerEnum GetActivePlayer() => _players[_current_player].Precedence;

        private int _current_player;
        private ChessBoard _board;
        private Player[] _players;
    }
}