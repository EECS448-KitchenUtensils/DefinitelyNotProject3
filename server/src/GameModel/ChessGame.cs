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
        public MoveType MakeMove(BoardPosition src, BoardPosition dest)
        {
            //Source must be valid
            if (!ChessBoard.CheckPositionExists(src))
                return MoveType.Failure;
            //Destination must be valid
            if (!ChessBoard.CheckPositionExists(dest))
                return MoveType.Failure;
            var currentPlayer = _players[_current_player];
            var piece = _board.GetPieceByPosition(src);
            var pieceAtDest = _board.GetPieceByPosition(dest);
            //Check piece ownership
            if (piece.Owner != currentPlayer)
                return MoveType.Failure;
            // Make sure move is possible
            var moves = _board.PossibleMoves(src)
                              .Where(move => move.Position == dest)
                              .ToList();
            
            if (moves.Count == 1)
            {
                piece.Position = dest;

                // ensure that the player is not in check as a result of this turn
                foreach (ChessPiece p in Pieces)
                {
                    if (p.Owner != currentPlayer)
                    {
                        // get the possible captures for the piece
                        var c = _board.PossibleMoves(p.Position)
                                      .Where(move => move.Outcome == MoveType.Capture)
                                      .ToList();

                        // only check if the piece can capture something
                        if (c.Count > 0)
                        {
                            // enemy checks, if applicable
                            foreach (MoveResult move in c)
                            {
                                var target = GetPieceByPosition(move.Position);

                                // if even one check is possible, the move cannot be made
                                if (target.PieceType == PieceEnum.KING)
                                {
                                    if (target.Owner == currentPlayer)
                                    {
                                        piece.Position = src; // return piece to original location
                                        return MoveType.Failure;
                                    }
                                }
                            }
                        }
                    }
                }

                // at this point, the current player's king is not in check
                currentPlayer.Checked = false;

                // check for checks
                // NOTE this only accounts for active checks. What about discovered checks?
                var checks = _board.PossibleMoves(dest)
                                   .Where(move => move.Outcome == MoveType.Capture)
                                   .ToList();

                // check each player, if applicable
                checks.ForEach(move => {
                    var p = GetPieceByPosition(move.Position);
                    if(p.PieceType == PieceEnum.KING) {
                        Player enemy = p.Owner;
                        enemy.Checked = true;
                    }
                });

                _current_player = (_current_player + 1) % 4; //Advance next player, mod 4
                if (moves[0].Outcome == MoveType.Capture)
                    _board.RemovePiece(pieceAtDest);
                return moves[0].Outcome;
            }
            return MoveType.Failure;
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