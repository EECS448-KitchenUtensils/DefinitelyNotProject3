using System;
namespace GameModel.Data
{
    public class Player
    {
        public Player(PlayerEnum order)
        {
            Precedence = order;
            Checked = false;
        }

        public PlayerEnum Precedence { get; }
        public bool Checked { get; internal set; }
    }
}
