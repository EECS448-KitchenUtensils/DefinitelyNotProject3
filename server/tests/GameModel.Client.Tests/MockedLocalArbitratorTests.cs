using NUnit.Framework;
using GameModel.Messages;

namespace GameModel.Client.Tests
{
    /// <summary>
    /// Contains tests for <see cref="LocalArbitrator"/> that require the use
    /// of a mocked <see cref="ITurnController"/> and <see cref="IGameModel"/>
    /// </summary>
    [TestFixture]
    public class MockedLocalArbitratorTests
    {
        /// <summary>
        /// An invalid move should not emit any messages
        /// </summary>
        [Test]
        public void InvalidMoveEmitsNothing()
        {
            Assert.Inconclusive("Implement me!");
        }

        /// <summary>
        /// A valid move in empty space should emit:
        /// <see cref="TranslatePieceMessage"/>
        /// <see cref="SetTurnMessage"/>
        /// </summary>
        [Test]
        public void ValidMoveEmitsTranslatePieceSetTurn()
        {
            Assert.Inconclusive("Implement me!");
        }

        /// <summary>
        /// A valid move resulting in checkmate should emit:
        /// <see cref="TranslatePieceMessage"/>
        /// <see cref="SetCheckMessage"/>
        /// <see cref="SetTurnMessage"/>
        /// </summary>
        [Test]
        public void MoveThatCausesCheckEmitsTranslatePieceSetCheckSetTurn()
        {
            Assert.Inconclusive("Implement me!");
        }

        /// <summary>
        /// A valid move resulting in a loss (failing to get out of check) should emit:
        /// <see cref="TranslatePieceMessage"/>
        /// <see cref="LostMessage"/>
        /// <see cref="SetTurnMessage"/>
        /// </summary>
        [Test]
        public void MoveThatCausesLossEmitsTranslatePieceLostSetTurn()
        {
            Assert.Inconclusive("Implement me!");
        }

        /// <summary>
        /// A valid capture resulting in a loss (king capture or failing to get out of check) should emit:
        /// <see cref="DestroyPieceMessage"/>
        /// <see cref="TranslatePieceMessage"/>
        /// <see cref="LostMessage"/>
        /// <see cref="SetTurnMessage"/>
        /// </summary>
        [Test]
        public void CaptureThatCausesLossEmitsDestroyPieceTranslatePieceLostSetTurn()
        {
            Assert.Inconclusive("Implement me!");
        }

        /// <summary>
        /// A valid move that removes a check should emit:
        /// <see cref="SetCheckMessage"/>
        /// <see cref="TranslatePieceMessage"/>
        /// <see cref="SetTurnMessage"/>
        /// </summary>
        [Test]
        public void MoveThatRemovesCheckEmitsSetCheckTranslatePieceSetTurn()
        {
            Assert.Inconclusive("Implement me!");
        }
    }
}
