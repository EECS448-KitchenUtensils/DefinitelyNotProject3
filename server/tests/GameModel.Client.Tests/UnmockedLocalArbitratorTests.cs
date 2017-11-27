using GameModel.Messages;
using NUnit.Framework;

namespace GameModel.Client.Tests
{
    [TestFixture]
    public class UnmockedLocalArbitratorTests
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

        /// <summary>
        /// Tests to see if <see cref="LocalArbitrator"/> emits a <see cref="SetTurnMessage"/> on startup
        /// </summary>
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
        /// Tests to see if <see cref="LocalArbitrator"/> emits a <see cref="LostMessage"/> on forfeit
        /// </summary>
        [Test]
        public void EmitsForfeitMessage()
        {
            ModelMessage msg;
            while (_uut.TryGetLatestMessage(out msg))
            {
                if (msg is SetTurnMessage)
                {
                    break;
                }
            }
            var initialPlayer = ((SetTurnMessage)msg).player;
            _uut.Forfeit();
            var emittedMsg = _uut.TryGetLatestMessage(out msg);
            Assert.That(emittedMsg, Is.True);
            Assert.That(msg, Is.TypeOf(typeof(LostMessage)));
            var lostMsg = (LostMessage)msg;
            Assert.That(lostMsg.player.Precedence, Is.EqualTo(initialPlayer));
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
