using GameModel.Data;
using GameModel.Messages;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

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
            throw new NotImplementedException();
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

        private ITurnController _tc;
        private IGameModel _game;
        private ConcurrentQueue<ModelMessage> _queue;
    }
}
