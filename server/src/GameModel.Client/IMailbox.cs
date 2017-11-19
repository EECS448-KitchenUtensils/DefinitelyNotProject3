using GameModel.Messages;

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
        /// Attempts to retrieve a message from the queue
        /// </summary>
        /// <returns>A retrieved ModelMessage or null</returns>
        ModelMessage RetrieveMessage();

        /// <summary>
        /// Checks if the receive queue has messages in it
        /// </summary>
        bool HasMessages { get; }
    }
}
