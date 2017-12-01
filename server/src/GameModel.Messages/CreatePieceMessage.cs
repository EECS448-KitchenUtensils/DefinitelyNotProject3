using GameModel.Data;
using System.Runtime.Serialization;

namespace GameModel.Messages
{
    /// <summary>
    /// Communicates that a <see cref="ChessPiece"/> has been created
    /// </summary>
    [DataContract]
    public class CreatePieceMessage: ModelMessage
    {
        /// <summary>
        /// Creates a <see cref="CreatePieceMessage"/>
        /// </summary>
        /// <param name="piece">The <see cref="ChessPiece"/> being created</param>
        public CreatePieceMessage(ChessPiece piece)
        {
            pieceType = piece.PieceType; //Always returns the deepest downcast possible (King, Bishop, etc.)
            owner = piece.Owner.Precedence;
            position = piece.Position;
        }
        
        /// <summary>
        /// The <see cref="PieceEnum"/> of the created piece
        /// </summary>
        [DataMember]
        public readonly PieceEnum pieceType;

        /// <summary>
        /// The owner of the created piece
        /// </summary>
        [DataMember]
        public readonly PlayerEnum owner;

        /// <summary>
        /// The position of the created piece
        /// </summary>
        [DataMember]
        public readonly BoardPosition position;
    }
}
