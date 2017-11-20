using GameModel.Messages;
using System;

namespace GameModel.Client
{
    public interface IMailbox
    {
        /// <summary>
        /// Puts a message into the send queue
        /// </summary>
        /// <param name="msg">Message to send</param>
        void SendMessage(ModelMessage msg);

        /// <summary>
        /// A stream of messages routed through this mailbox
        /// </summary>
        IObservable<ModelMessage> Messages { get; }
    }
}
