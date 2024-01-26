using Memory.ConsoleApp;
using Moq;
using NUnit.Framework;

namespace Memory.Tests
{
    [TestFixture]
    public class CardTests
    {
        private MockRepository mockRepository;



        [SetUp]
        public void SetUp()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
        }

        private Card CreateCard(bool check)
        {
            if (check)
            {
                return new Card(1,'@');
            }
            else
            {
                return new Card(1, '@', "C:/program files/LegitUrl.png");
            }
            
        }

        [Test]
        public void Flip_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var card = this.CreateCard(true);

            // Act
            bool flipped = false;
            card.IsFlipped = false;
            card.Flip();

            // Assert
            Assert.IsTrue(flipped != card.IsFlipped);
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void Match_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var card = this.CreateCard(true);

            // Act
            card.Match();

            // Assert
            Assert.IsTrue(card.isMatched == true);
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void GetSymbol_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var card = this.CreateCard(true);

            // Act
            var resultFaceDown = card.GetSymbol();
            card.Flip();
            var resultFaceUp = card.GetSymbol();

            // Assert
            Assert.IsTrue(resultFaceDown == "#" && resultFaceUp == "@");
            this.mockRepository.VerifyAll();
        }

        [Test]
        public void GetURL_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var card = this.CreateCard(false);

            // Act
            var resultFaceDown = card.GetCardFace();
            card.Flip();
            var resultFaceUp = card.GetCardFace();

            // Assert
            Assert.IsTrue(resultFaceDown == "C:/programFiles/TheBackOfACard" && resultFaceUp == "C:/program files/LegitUrl.png");
            this.mockRepository.VerifyAll();
        }
    }
}
