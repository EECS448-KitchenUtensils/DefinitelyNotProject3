using NUnit.Framework;
using GameModel.Messages;

namespace GameModel.Client.Tests
{
    [TestFixture]
    public class MockedLocalArbitratorTests
    {
        /// <summary>
        /// An invalid move should not emit any messages
        /// </summary>
        [Test]
        public void InvalidMoveEmitsNothing()
        {

        }

        /// <summary>
        /// A valid move in empty space should emit:
        /// <see cref="TranslatePieceMessage"/>
        /// <see cref="SetTurnMessage"/>
        /// </summary>
        [Test]
        public void ValidMoveEmitsTranslatePieceSetTurn()
        {

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

        }
    }
}
