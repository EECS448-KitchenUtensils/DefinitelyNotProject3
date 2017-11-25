using GameModel.Data;
using System;

namespace GameModel.Messages
{
    [Serializable]
    public class CreatePieceMessage: ModelMessage
    {
        /// <summary>
        /// Creates a <see cref="CreatePieceMessage"/>
        /// </summary>
        /// <param name="piece">The <see cref="ChessPiece"/> being created</param>
        public CreatePieceMessage(ChessPiece piece)
        {
            pieceType = piece.GetType(); //Always returns the deepest downcast possible (King, Bishop, etc.)
            owner = piece.Owner.Precedence;
            position = piece.Position;
        }
        /// <summary>
        /// The <see cref="Type"/> of the created piece
        /// </summary>
        public readonly Type pieceType;

        /// <summary>
        /// The owner of the created piece
        /// </summary>
        public readonly PlayerEnum owner;

        /// <summary>
        /// The position of the created piece
        /// </summary>
        public readonly BoardPosition position;
    }
}
