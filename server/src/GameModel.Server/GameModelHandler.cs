using Microsoft.Web.WebSockets;
using System;
using System.Reactive.Linq;

namespace GameModel.Server
{
    /// <summary>
    /// Handles a connection with a client GameModel over a websocket
    /// </summary>
    public class GameModelHandler: WebSocketHandler
    {
        public override void OnOpen()
        {
            _running = true;
            var msgStream = Observable.FromEventPattern<MessageEventArgs>(
                handler => _messages += handler,
                handler => _messages -= handler)
                .TakeWhile(evArg => _running)
                .Select(evArgs => evArgs.EventArgs.Message);
            _client = new ClientConnection(Guid.NewGuid(), msgStream, Send);
            _plumbing = new Plumbing(_client);
        }

        public override void OnMessage(byte[] message)
        {
            _messages?.Invoke(this, new MessageEventArgs(message));
        }

        public override void OnClose()
        {
            _running = false;
            base.OnClose();
        }

        private class MessageEventArgs: EventArgs
        {
            public MessageEventArgs(byte[] message)
            {
                Message = message;
            }
            public byte[] Message;
        }

        private event EventHandler<MessageEventArgs> _messages;
        private ClientConnection _client;
        private Plumbing _plumbing;
        private bool _running;
    }
}