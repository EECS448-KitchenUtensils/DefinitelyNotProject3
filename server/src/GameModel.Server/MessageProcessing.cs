using GameModel.Data;
using GameModel.Messages;
using System;
using System.IO;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using System.Xml;

namespace GameModel.Server
{
    /// <summary>
    /// Contains methods to de/hydrate streams of messages
    /// </summary>
    public static class MessageProcessing
    {
        /// <summary>
        /// Initializes objects used to de/hydrate messages
        /// </summary>
        static MessageProcessing()
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

        /// <summary>
        /// Hydrates serialized messages
        /// </summary>
        /// <param name="rawMessages">An <see cref="IObserver{T}"/> of raw messages</param>
        /// <returns>An <see cref="IObserver{T}"/> of hydrated messages</returns>
        public static IObservable<ModelMessage> ParseMessages(this IObservable<byte[]> rawMessages) =>
            rawMessages.Select(rawMessage =>
            {
                var stream = new MemoryStream(rawMessage);
                return (ModelMessage)_serializer.ReadObject(stream);
            });

        /// <summary>
        /// Dehydrates messages
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        public static IObservable<byte[]> SerializeMessages(this IObservable<ModelMessage> messages) =>
            messages.Select(msg =>
            {
                var stream = new MemoryStream();
                _serializer.WriteObject(stream, msg);
                return stream.ToArray();
            });

        private static DataContractSerializer _serializer;
    }
}