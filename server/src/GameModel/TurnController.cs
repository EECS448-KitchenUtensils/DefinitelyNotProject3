using GameModel.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameModel
{
    public class TurnController
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
        public Player Current => _players[(int)_currentIndex];

        /// <summary>
        /// Advances the turn
        /// </summary>
        /// <returns>The new current <see cref="Player"/></returns>
        public Player Next() => LoopPlayers().FirstOrDefault(player => player.InGame);

        private IEnumerable<Player> LoopPlayers()
        {
            var nextIndex = ((int)_currentIndex + 1) % 4;
            while (nextIndex != (int)_currentIndex)
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
