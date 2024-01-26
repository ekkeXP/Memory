using Memory.ConsoleApp;
using Moq;
using NUnit.Framework;

namespace Memory.Tests
{
    [TestFixture]
    public class MemoryGameTests
    {
        private MockRepository mockRepository;

        [SetUp]
        public void SetUp()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
        }

        private MemoryGame CreateMemoryGame()
        {
            return new MemoryGame();
        }

        [Test]
        public void Start_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var memoryGame = this.CreateMemoryGame();

            // Act
            memoryGame.Start();

            // Assert
            Assert.IsTrue(memoryGame.aTimer != null && memoryGame.aTimer.AutoReset == true && memoryGame.aTimer.Enabled == true);
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void Stop_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var memoryGame = this.CreateMemoryGame();

            // Act
            memoryGame.Start();
            memoryGame.stopTimer();

            // Assert
            Assert.IsTrue(memoryGame.aTimer == null);
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void FillCardList_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var memoryGame = this.CreateMemoryGame();

            // Act
            memoryGame.FillCardList(10);

            // Assert
            Assert.IsTrue(memoryGame.cards.Length == 10);
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void ShuffleCards_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var memoryGame = this.CreateMemoryGame();

            // Act
            memoryGame.FillCardList(10);
            Card[] cards = (Card[])memoryGame.cards.Clone();
            memoryGame.ShuffleCards();

            // Assert
            Assert.IsFalse(cards.SequenceEqual(memoryGame.cards));
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void FlipCard_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var memoryGame = this.CreateMemoryGame();

            // Act
            memoryGame.FillCardList(10);
            memoryGame.FlipCard(3);

            // Assert
            Assert.IsTrue(memoryGame.cards[2].IsFlipped == true);
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void IsFinished_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var memoryGame = this.CreateMemoryGame();

            // Act
            memoryGame.FillCardList(10);
            foreach (Card card in memoryGame.cards)
            {
                card.isMatched = true;
            }

            // Assert
            Assert.IsTrue(memoryGame.IsFinished());
            this.mockRepository.VerifyAll();
        }
    }
}
