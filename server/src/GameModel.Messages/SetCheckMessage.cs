using GameModel.Data;
using System.Runtime.Serialization;

namespace GameModel.Messages
{
    [DataContract]
    public class SetCheckMessage
    {
        public SetCheckMessage(Player player)
        {
            this.player = player.Precedence;
            isInCheck = player.Checked;
        }

        [DataMember]
        public readonly PlayerEnum player;

        [DataMember]
        public readonly bool isInCheck;
    }
}
