using GameModel.Data;

namespace GameModel
{
    /// <summary>
    /// Interface for mocking a <see cref="TurnController"/>
    /// </summary>
    public interface ITurnController
    {
        /// <summary>
        /// Returns the current <see cref="Player"/>
        /// </summary>
        Player Current { get; }

        /// <summary>
        /// Advances the turn
        /// </summary>
        /// <returns>The new current <see cref="Player"/></returns>
        Player Next();

        /// <summary>
        /// Accesses the <see cref="Player"/> instance for the first player
        /// </summary>
        Player Player1 { get; }

        /// <summary>
        /// Accesses the <see cref="Player"/> instance for the second player
        /// </summary>
        Player Player2 { get; }

        /// <summary>
        /// Accesses the <see cref="Player"/> instance for the third player
        /// </summary>
        Player Player3 { get; }

        /// <summary>
        /// Accesses the <see cref="Player"/> instance for the fourth player
        /// </summary>
        Player Player4 { get; }
    }
}
