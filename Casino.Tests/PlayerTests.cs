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
            player.BuyChips(10);

            player.Bet(number: 5, chipsAmount: 10);

            Assert.AreEqual(10, player.CurrentBets[0].ChipsAmount);
        }

        [Test]
        public void ShouldNotBeAbleToBet_WhenBetMoreChipsThenHave()
        {
            var player = new Player();
            player.BuyChips(10);

            var isSuccessfulBet = player.Bet(number: 5, chipsAmount: 11);

            Assert.IsFalse(isSuccessfulBet);
        }

        [Test]
        public void ShouldBeAbleToBetSeveralTimesOnDifferentNumbers()
        {
            var player = new Player();
            player.BuyChips(20);

            player.Bet(number: 5, chipsAmount: 10);
            player.Bet(number: 6, chipsAmount: 10);

            Assert.AreEqual(5, player.CurrentBets[0].Number);
            Assert.AreEqual(6, player.CurrentBets[1].Number);
        }

        [Test]
        public void ShouldBeAbleToBetOnNumbersFromOneToSix()
        {
            var player = new Player();
            player.BuyChips(100);

            var isSuccessfulBet1 = player.Bet(number: 1, chipsAmount: 10);
            var isSuccessfulBet2 = player.Bet(number: 2, chipsAmount: 10);
            var isSuccessfulBet3 = player.Bet(number: 3, chipsAmount: 10);
            var isSuccessfulBet4 = player.Bet(number: 4, chipsAmount: 10);
            var isSuccessfulBet5 = player.Bet(number: 5, chipsAmount: 10);
            var isSuccessfulBet6 = player.Bet(number: 6, chipsAmount: 10);

            Assert.IsTrue(isSuccessfulBet1);
            Assert.IsTrue(isSuccessfulBet2);
            Assert.IsTrue(isSuccessfulBet3);
            Assert.IsTrue(isSuccessfulBet4);
            Assert.IsTrue(isSuccessfulBet5);
            Assert.IsTrue(isSuccessfulBet6);
        }

        [Test]
        public void ShouldNotBeAbleToBetOnNumbersOtherThanOneToSix()
        {
            var player = new Player();
            player.BuyChips(100);

            var isRejectedBet1 = player.Bet(number: -1, chipsAmount: 10);
            var isRejectedBet2 = player.Bet(number: 0, chipsAmount: 10);
            var isRejectedBet3 = player.Bet(number: 7, chipsAmount: 10);

            Assert.IsFalse(isRejectedBet1);
            Assert.IsFalse(isRejectedBet2);
            Assert.IsFalse(isRejectedBet3);
        }
    }
}