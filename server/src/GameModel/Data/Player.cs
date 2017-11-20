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

        public static bool operator ==(Player first, Player second) => first.Precedence == second.Precedence;

        public static bool operator !=(Player first, Player second) => !(first == second);

        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case Player other:
                    return other.Precedence == Precedence;
                default:
                    return false;
            }
        }

        public override int GetHashCode() => (int)Precedence;

        public PlayerEnum Precedence { get; }
        public bool Checked { get; internal set; }
    }
}
