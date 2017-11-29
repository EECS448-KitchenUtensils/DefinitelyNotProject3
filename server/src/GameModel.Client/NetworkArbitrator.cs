﻿using GameModel.Data;
using GameModel.Messages;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace GameModel.Client
{
    class NetworkArbitrator : IArbitrator
    {
        //As of now just the same as local, need to make networked turn controller
        public NetworkArbitrator()
        {
            _tc = new TurnController(PlayerEnum.PLAYER_1);
            _game = new ChessGame(_tc);
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
            Player[] InGame() => players.Where(p => p.InGame)
                                            .ToArray();
            Player[] InCheck() => players.Where(p => p.InGame && p.Checked)
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
            var msg = new TranslatePieceMessage(src, dest);
            _queue.Enqueue(msg);
        }

        private void EmitPieceDestroy(ChessPiece destroyed)
        {
            var msg = new DestroyPieceMessage(destroyed);
            _queue.Enqueue(msg);
        }

        private void EmitInCheck(Player[] playersInCheckPre, Player[] playersInCheckPost)
        {
            foreach (var newInCheck in playersInCheckPost.Except(playersInCheckPre))
            {
                var msg = new SetCheckMessage(newInCheck);
                _queue.Enqueue(msg);
            }
            foreach (var leavingCheck in playersInCheckPre.Except(playersInCheckPost))
            {
                var msg = new SetCheckMessage(leavingCheck);
                _queue.Enqueue(msg);
            }
        }

        private void EmitLoss(Player[] playersInGamePre, Player[] playersInGamePost)
        {
            foreach (var losingPlayer in playersInGamePost.Except(playersInGamePre))
            {
                var reason = losingPlayer.Checked ? LostMessage.Reason.Checkmate : LostMessage.Reason.KingCapture;
                var msg = new LostMessage(reason, losingPlayer);
                _queue.Enqueue(msg);
            }
        }

        private ITurnController _tc;
        private IGameModel _game;
        private ConcurrentQueue<ModelMessage> _queue;

    }
}
