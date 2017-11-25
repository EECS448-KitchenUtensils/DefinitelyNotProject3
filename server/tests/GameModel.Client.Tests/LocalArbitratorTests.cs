using GameModel.Messages;
using NUnit.Framework;

namespace GameModel.Client.Tests
{
    [TestFixture]
    public class LocalArbitratorTests
    {
        /// <summary>
        /// Tests to see if <see cref="LocalArbitrator"/> emits the initial 64 <see cref="CreatePieceMessage"/>s
        /// </summary>
        [Test]
        public void CreatesInitialPieces()
        {
            ModelMessage msg;
            int piecesCreated = 0;
            while (_uut.TryGetLatestMessage(out msg))
            {
                if (msg is CreatePieceMessage)
                    piecesCreated++;
            }
            Assert.That(piecesCreated, Is.EqualTo(64));
        }

        [Test]
        public void EmitsInitialSetTurnMessage()
        {
            ModelMessage msg;
            bool emitted = false;
            while (_uut.TryGetLatestMessage(out msg))
            {
                if (msg is SetTurnMessage)
                {
                    emitted = true;
                }
            }
            Assert.That(emitted, Is.True);
        }

        /// <summary>
        /// Creates a new <see cref="LocalArbitrator"/> for each test
        /// </summary>
        [SetUp]
        public void CreateUUT()
        {
            _uut = new LocalArbitrator();
        }

        private LocalArbitrator _uut;
    }
}
