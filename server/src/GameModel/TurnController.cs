using GameModel.Data;
using System.Collections.Generic;
using System.Linq;

namespace GameModel
{
    /// <summary>
    /// Encapsulates logic for telling which player owns the current turn
    /// </summary>
    public class TurnController: ITurnController
    {
        /// <summary>
        /// Constructor for general use
        /// </summary>
        /// <param name="firstPlayer">Player to first</param>
        public TurnController(PlayerEnum firstPlayer)
        {
            _players = new[]
            {
                new Player(PlayerEnum.PLAYER_1),
                new Player(PlayerEnum.PLAYER_2),
                new Player(PlayerEnum.PLAYER_3),
                new Player(PlayerEnum.PLAYER_4)
            };
            _currentIndex = firstPlayer;
        }

        /// <summary>
        /// Constructor for testing
        /// </summary>
        /// <param name="players">A sequence of <see cref="Player"/> to control</param>
        /// <param name="firstPlayer"><see cref="Player"/> to go first</param>
        public TurnController(IEnumerable<Player> players, PlayerEnum firstPlayer)
        {
            _players = players.ToArray();
            _currentIndex = firstPlayer;
        }

        /// <summary>
        /// Returns the current <see cref="Player"/>
        /// </summary>
        public Player Current => LoopPlayers(0).FirstOrDefault(player => player.InGame);

        /// <summary>
        /// Advances the turn
        /// </summary>
        /// <returns>The new current <see cref="Player"/></returns>
        public Player Next()
        {
            var nextPlayer = LoopPlayers(1).FirstOrDefault(player => player.InGame);
            if (nextPlayer != null)
            {
                _currentIndex = nextPlayer.Precedence;
            }
            return nextPlayer;
        }

        /// <summary>
        /// Accesses the <see cref="Player"/> instance for the first player
        /// </summary>
        public Player Player1 => _players[(int)PlayerEnum.PLAYER_1];

        /// <summary>
        /// Accesses the <see cref="Player"/> instance for the second player
        /// </summary>
        public Player Player2 => _players[(int)PlayerEnum.PLAYER_2];

        /// <summary>
        /// Accesses the <see cref="Player"/> instance for the third player
        /// </summary>
        public Player Player3 => _players[(int)PlayerEnum.PLAYER_3];

        /// <summary>
        /// Accesses the <see cref="Player"/> instance for the fourth player
        /// </summary>
        public Player Player4 => _players[(int)PlayerEnum.PLAYER_4];

        private IEnumerable<Player> LoopPlayers(int incr)
        {
            var nextIndex = ((int)_currentIndex + incr) % 4;
            for (var i = 0; i < 4; i++)
            {
                yield return _players[nextIndex];
                nextIndex = (nextIndex + 1) % 4;
            }
            yield break;
        }
        private PlayerEnum _currentIndex;
        private Player[] _players;
    }
}
