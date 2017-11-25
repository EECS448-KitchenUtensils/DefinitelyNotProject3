using GameModel.Data;
using System.Runtime.Serialization;

namespace GameModel.Messages
{
    /// <summary>
    /// Represents the movement of a piece on the board
    /// </summary>
    [DataContract]
    public class TranslatePieceMessage: ModelMessage
    {
        /// <summary>
        /// Creates a <see cref="TranslatePieceMessage"/>
        /// </summary>
        /// <param name="src">The source position for this translation</param>
        /// <param name="dest">The destination position for this translation</param>
        public TranslatePieceMessage(BoardPosition src, BoardPosition dest)
        {
            this.src = src;
            this.dest = dest;
        }

        /// <summary>
        /// The source position for this translation
        /// </summary>
        [DataMember]
        public readonly BoardPosition src;

        /// <summary>
        /// The destination position for this translation
        /// </summary>
        [DataMember]
        public readonly BoardPosition dest;
    }
}
