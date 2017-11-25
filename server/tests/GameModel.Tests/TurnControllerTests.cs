using GameModel.Data;
using NUnit.Framework;
using System.Collections;
using System.Linq;

namespace GameModel.Tests
{
    [TestFixture]
    class TurnControllerTests
    {
        [Test, TestCaseSource("NextCases")]
        public void NextTests(TurnController tc, Player expected)
        {
            Assert.That(tc.Next(), Is.EqualTo(expected));
        }

        [Test]
        public void ForfeitAdvancesTurn()
        {
            var tc = new TurnController(Players, PlayerEnum.PLAYER_1);
            tc.Player1.Forfeit();
            Assert.That(tc.Current, Is.EqualTo(tc.Player2));
        }

        [Test]
        public void LossAdvancesTurn()
        {
            var tc = new TurnController(Players, PlayerEnum.PLAYER_1);
            tc.Player1.Loss();
            Assert.That(tc.Current, Is.EqualTo(tc.Player2));
        }

        public static IEnumerable NextCases
        {
            get
            {
                {
                    var players = Players;
                    var tc = new TurnController(players, PlayerEnum.PLAYER_1);
                    yield return new TestCaseData(tc, players[1])
                        .SetCategory(nameof(TurnController))
                        .SetName("Simple Advance");
                }
                {
                    var players = Players;
                    var tc = new TurnController(players, PlayerEnum.PLAYER_4);
                    yield return new TestCaseData(tc, players[0])
                        .SetCategory(nameof(TurnController))
                        .SetName("Advance wrap-around");
                }
                {
                    var players = Players;
                    players[1].InGame = false;
                    var tc = new TurnController(players, PlayerEnum.PLAYER_1);
                    yield return new TestCaseData(tc, players[2])
                        .SetCategory(nameof(TurnController))
                        .SetName("Skip dead players");
                }
            }
        }
        private static Player[] Players =>
            Enumerable.Range((int)PlayerEnum.PLAYER_1, (int)PlayerEnum.PLAYER_4 - (int)PlayerEnum.PLAYER_1)
                      .Select(i => new Player((PlayerEnum)i))
                      .ToArray();
    }
}
