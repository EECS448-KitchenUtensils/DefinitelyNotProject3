using GameModel.Data;
using System.Collections.Generic;
using System.Linq;

namespace GameModel
{
    public class ChessGame
    {
        public ChessGame()
        {
            _board = new ChessBoard();
            _current_player = 0;
        }
        /// <summary>
        /// Generates all the possible moves of a piece
        /// </summary>
        /// <param name="pos">Position of the piece to query</param>
        /// <returns>A sequence of moves and their type</returns>
        public IEnumerable<(BoardPosition, MoveType)> PossibleMoves(BoardPosition pos) => _board.PossibleMoves(pos);

        public ChessPiece GetPieceByPosition(BoardPosition pos) => _board.GetPieceByPosition(pos);

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
            //Make sure move is possible
            var moves = _board.PossibleMoves(src)
                              .Where(move => move.pos == dest)
                              .ToList();
            if (moves.Count == 1)
                return moves[0].type;
            return MoveType.Failure;
        }

        /// <summary>
        /// Gets which player owns the current turn
        /// </summary>
        /// <returns>The active player</returns>
        public PlayerEnum GetActivePlayer() => _players[_current_player];

        private int _current_player;
        private ChessBoard _board;
        private PlayerEnum[] _players = {PlayerEnum.PLAYER_1, PlayerEnum.PLAYER_2, PlayerEnum.PLAYER_3, PlayerEnum.PLAYER_4};
    }
}