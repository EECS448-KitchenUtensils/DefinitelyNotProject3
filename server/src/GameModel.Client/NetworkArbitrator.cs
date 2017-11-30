using GameModel.Data;
using GameModel.Messages;
using UnityEngine;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Runtime.Serialization;
using System.IO;

namespace GameModel.Client
{
    public class NetworkArbitrator : IArbitrator
    {
        public NetworkArbitrator(Uri wsAddress)
        {
            _wsAddress = wsAddress;
            _ws = new ClientWebSocket();
            _th = new Thread(CheckForMessages);
            Connect();
            CreateSerializer();
            _tc = new TurnController(PlayerEnum.PLAYER_1);
            _game = new ChessGame(_tc);
            _queue = new ConcurrentQueue<ModelMessage>();
        }

        public async void Connect()
        {
            await _ws.ConnectAsync(_wsAddress, CancellationToken.None);
            _th.Start();
            Debug.Log("Connected");
        }

        public void CreateSerializer()
        {
            var knownTypes = new[]
            {
                typeof(CreatePieceMessage),
                typeof(TranslatePieceMessage),
                typeof(GameBeginMessage),
                typeof(DestroyPieceMessage),
                typeof(SetTurnMessage),
                typeof(PieceEnum),
                typeof(PlayerEnum),
                typeof(XCoord)
            };

            _serializer = new DataContractSerializer(typeof(ModelMessage), knownTypes);
        }

        public void Forfeit()
        {
            //Nothing rn
        }

        public async void MakeMove(BoardPosition src, BoardPosition dest)
        {

            TranslatePieceMessage moveToTry = new TranslatePieceMessage(src, dest);
            var stream = new MemoryStream();
            _serializer.WriteObject(stream, moveToTry);
            await _ws.SendAsync(new ArraySegment<byte>(stream.ToArray()), WebSocketMessageType.Text, true, CancellationToken.None);
           
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

        private async void CheckForMessages()
        {
            UnityEngine.Debug.Log("Check Message Service Started");
            while (true)
            {
                Debug.Log("Loop");
                var buffer = new ArraySegment<byte>();
                await _ws.ReceiveAsync(buffer, CancellationToken.None);
                UnityEngine.Debug.Log("Message from server recieved");
                var recievedStream = new MemoryStream(buffer.ToArray());
                var recievedObject = (ModelMessage)_serializer.ReadObject(recievedStream);
                if(recievedObject is TranslatePieceMessage)
                {
                    var message = (TranslatePieceMessage)recievedObject;
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
                        var result = _game.MakeMove(message.src, message.dest);
                        var playersInGamePost = InGame();
                        var playersInCheckPost = InCheck();
                }
                _queue.Enqueue(recievedObject);
            }
        }

        private Thread _th;
        private DataContractSerializer _serializer;
        private Uri _wsAddress;
        private ClientWebSocket _ws;
        private ITurnController _tc;
        private IGameModel _game;
        private ConcurrentQueue<ModelMessage> _queue;
    }
}
