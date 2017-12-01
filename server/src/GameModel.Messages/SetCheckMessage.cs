using GameModel.Data;
using System.Runtime.Serialization;

namespace GameModel.Messages
{
    /// <summary>
    /// Indicates that the <see cref="Player.Checked"/> property has been changed
    /// </summary>
    [DataContract]
    public class SetCheckMessage: ModelMessage
    {
        /// <summary>
        /// Creates a <see cref="SetCheckMessage"/> from a <see cref="Player"/>
        /// </summary>
        /// <param name="player">The <see cref="Player"/> to create a message for</param>
        public SetCheckMessage(Player player)
        {
            this.player = player.Precedence;
            isInCheck = player.Checked;
        }

        /// <summary>
        /// Which player has had their check status updated
        /// </summary>
        [DataMember]
        public readonly PlayerEnum player;

        /// <summary>
        /// Whether the <see cref="Player"/> is in check as of this message
        /// </summary>
        [DataMember]
        public readonly bool isInCheck;
    }
}
