using GameModel.Messages;
using Microsoft.Web.WebSockets;
using System;
using System.Reactive.Linq;

namespace GameModel.Server
{
    /// <summary>
    /// Handles a connection with a client GameModel over a websocket
    /// </summary>
    public class GameModelHandler : WebSocketHandler
    {
        /// <summary>
        /// Called when a connection is opened
        /// </summary>
        public override void OnOpen()
        {
            _running = true;
            var msgStream = Observable.FromEventPattern<MessageEventArgs>(
                handler => _messages += handler,
                handler => _messages -= handler)
                .TakeWhile(evArg => _running)
                .Select(evArgs => evArgs.EventArgs.Message);
            _client = new ClientConnection(Guid.NewGuid(), msgStream, Send);
            _connections?.Invoke(this, new HandlerEventArgs(this));
        }

        /// <summary>
        /// Called when a websocket message is received
        /// </summary>
        /// <param name="message"></param>
        public override void OnMessage(byte[] message)
        {
            _messages?.Invoke(this, new MessageEventArgs(message));
        }

        /// <summary>
        /// Called when a connection is closed
        /// </summary>
        public override void OnClose()
        {
            _running = false;
            _client.IsRunning = false;
            base.OnClose();
        }

        /// <summary>
        /// Creates an <see cref="IObservable{T}"/> containing all of the information about client connections and messages
        /// </summary>
        public static IObservable<ClientConnection> Connections
        {
            get
            {
                return Observable.FromEventPattern<HandlerEventArgs>(
                    handler => _connections += handler,
                    handler => _connections -= handler)
                    .Select(evArgs => evArgs.EventArgs.Handler)
                    .Select(handler => handler._client);
            }
        }

        private class MessageEventArgs: EventArgs
        {
            public MessageEventArgs(byte[] message)
            {
                Message = message;
            }
            public byte[] Message;
        }

        private class HandlerEventArgs : EventArgs
        {
            public HandlerEventArgs(GameModelHandler handler)
            {
                Handler = handler;
            }
            public GameModelHandler Handler;
        }

        private static event EventHandler<HandlerEventArgs> _connections;
        private event EventHandler<MessageEventArgs> _messages;
        private ClientConnection _client;
        private Plumbing _plumbing;
        private bool _running;
    }
}