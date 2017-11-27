using GameModel.Data;
using System.Runtime.Serialization;

namespace GameModel.Messages
{
    /// <summary>
    /// Encodes the destruction of a piece
    /// </summary>
    [DataContract]
    public class DestroyPieceMessage: ModelMessage
    {
        public DestroyPieceMessage(ChessPiece piece)
        {
            pieceType = piece.PieceType;
            position = piece.Position;
            owner = piece.Owner.Precedence;
        }

        /// <summary>
        /// The type of the piece destroyed
        /// </summary>
        [DataMember]
        public readonly PieceEnum pieceType;

        /// <summary>
        /// The position of the piece destroyed
        /// </summary>
        [DataMember]
        public readonly BoardPosition position;

        /// <summary>
        /// The owner of the piece destroyed
        /// </summary>
        [DataMember]
        public readonly PlayerEnum owner;
    }
}
