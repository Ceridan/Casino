using NUnit.Framework;

namespace Casino.Tests
{
    [TestFixture]
    public class PlayerTests
    {
        [Test]
        public void ShouldBeInGame_WhenJoinedGame()
        {
            var player = new Player();
            var game = new Game();

            player.Join(game);

            Assert.IsTrue(player.IsInGame);
        }

        [Test]
        public void ShouldBeNotInGame_WhenLeaveGame()
        {
            var player = new Player();

            player.Leave();

            Assert.IsFalse(player.IsInGame);
        }

        [Test]
        public void ShouldNotBeAbleToLeaveGame_WhenNotInGame()
        {
            var player = new Player();

            var isSuccessfullLeave = player.Leave();

            Assert.IsFalse(isSuccessfullLeave);
        }

        [Test]
        public void ShouldNotBeAbleToJoinGame_WhenInGame()
        {
            var player = new Player();
            var game1 = new Game();
            var game2 = new Game();

            player.Join(game1);
            var isSuccessfulSecondJoin = player.Join(game2);

            Assert.IsFalse(isSuccessfulSecondJoin);
        }

        [Test]
        public void ShouldBeAbleToBuyChips()
        {
            var player = new Player();

            player.BuyChips(10);

            Assert.AreEqual(10, player.CurrentChips);
        }

        [Test]
        public void ShouldBeAbleToMakeBet()
        {
            var player = new Player();

            player.Bet(10);

            Assert.AreEqual(10, player.CurrentBet);
        }

        [Test]
        public void ShouldNotBeAbleToBet_WhenBetMoreChipsThenHave()
        {
            var player = new Player();

            player.BuyChips(10);
            var isSuccessfulBet = player.Bet(11);

            Assert.IsFalse(isSuccessfulBet);
        }
    }
}