using GameModel.Data;
using GameModel.Messages;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace GameModel.Client
{
    public class LocalArbitrator : IArbitrator
    {
        public LocalArbitrator()
        {
            _tc = new TurnController(PlayerEnum.PLAYER_1);
            _game = new ChessGame(_tc);
            _queue = new ConcurrentQueue<ModelMessage>();
            EmitCreatePieces();
            EmitSetTurn();
        }
        
        public LocalArbitrator(ITurnController tc, IGameModel gameModel)
        {
            _tc = tc;
            _game = gameModel;
            _queue = new ConcurrentQueue<ModelMessage>();
            EmitCreatePieces();
            EmitSetTurn();
        }

        public void Forfeit()
        {
            _queue.Enqueue(new LostMessage(LostMessage.Reason.Forfeit, _tc.Current));
            _tc.Current.Forfeit();
        }

        public void MakeMove(BoardPosition src, BoardPosition dest)
        {
            var players = new[] {
                _tc.Player1,
                _tc.Player2,
                _tc.Player3,
                _tc.Player4
            };
            PlayerEnum[] InGame() => players.Where(p => p.InGame)
                                            .Select(p => p.Precedence)
                                            .ToArray();
            PlayerEnum[] InCheck() => players.Where(p => p.InGame && p.Checked)
                                             .Select(p => p.Precedence)
                                             .ToArray();
            var playersInGamePre = InGame();
            var playersInCheckPre = InCheck();
            var result = _game.MakeMove(src, dest);
            var playersInGamePost = InGame();
            var playersInCheckPost = InCheck();
            switch (result.Outcome)
            {
                case MoveType.Move:
                    EmitPieceMove(src, dest);
                    EmitLoss(playersInGamePre, playersInGamePost);
                    EmitInCheck(playersInCheckPre, playersInCheckPost);
                    EmitSetTurn();
                    break;
                case MoveType.Capture:
                    EmitPieceMove(src, dest);
                    EmitPieceDestroy(result.Destroyed);
                    EmitLoss(playersInGamePre, playersInGamePost);
                    EmitInCheck(playersInCheckPre, playersInCheckPost);
                    EmitSetTurn();
                    break;
            }
        }

        /// <summary>
        /// Proxies <see cref="ChessGame.PossibleMoves(BoardPosition)"/>
        /// </summary>
        /// <param name="pos">Position to check from</param>
        /// <returns>A sequence of possible moves</returns>
        public IEnumerable<MoveResult> PossibleMoves(BoardPosition pos) =>
            _game.PossibleMoves(pos);

        /// <summary>
        /// Fakes a shutdown since that doesn't really make sense for local games
        /// </summary>
        public void Shutdown()
        {
        }

        public bool TryGetLatestMessage(out ModelMessage message) =>
            _queue.TryDequeue(out message);

        private void EmitCreatePieces()
        {
            foreach (var piece in _game.Pieces)
            {
                var msg = new CreatePieceMessage(piece);
                _queue.Enqueue(msg);
            }
        }


        private void EmitSetTurn()
        {
            var msg = new SetTurnMessage(_tc.Current);
            _queue.Enqueue(msg);
        }


        private void EmitPieceMove(BoardPosition src, BoardPosition dest)
        {
            throw new NotImplementedException();
        }

        private void EmitPieceDestroy(ChessPiece destroyed)
        {
            throw new NotImplementedException();
        }

        private void EmitInCheck(PlayerEnum[] playersInCheckPre, PlayerEnum[] playersInCheckPost)
        {
            throw new NotImplementedException();
        }

        private void EmitLoss(PlayerEnum[] playersInGamePre, PlayerEnum[] playersInGamePost)
        {
            throw new NotImplementedException();
        }

        private ITurnController _tc;
        private IGameModel _game;
        private ConcurrentQueue<ModelMessage> _queue;
    }
}
