using Casino.Tests.DSL;
using Moq;
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
            var game = new Game();
            player.Join(game);

            player.Leave(game);

            Assert.IsFalse(player.IsInGame);
        }

        [Test]
        public void ShouldNotBeAbleToLeaveGame_WhenNotInGame()
        {
            var player = new Player();
            var game = new Game();

            var isSuccessfullLeave = player.Leave(game);

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
            player.Join(new Game());

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
            player.Join(new Game());

            player.Bet(number: 5, chipsAmount: 10);
            player.Bet(number: 6, chipsAmount: 10);

            Assert.AreEqual(5, player.CurrentBets[0].Number);
            Assert.AreEqual(6, player.CurrentBets[1].Number);
        }

        [Test]
        public void ShouldBeAbleToBetOnNumbersFromOneToSix_WhenGameHasOneDice()
        {
            var player = new Player();
            player.BuyChips(100);
            player.Join(new Game(numberOfDices: 1));

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
        public void ShouldNotBeAbleToBetOnNumbersOtherThanOneToSix_WhenGameHasOneDice()
        {
            var player = new Player();
            player.BuyChips(100);
            player.Join(new Game(numberOfDices: 1));

            var isRejectedBet1 = player.Bet(number: -1, chipsAmount: 10);
            var isRejectedBet2 = player.Bet(number: 0, chipsAmount: 10);
            var isRejectedBet3 = player.Bet(number: 7, chipsAmount: 10);

            Assert.IsFalse(isRejectedBet1);
            Assert.IsFalse(isRejectedBet2);
            Assert.IsFalse(isRejectedBet3);
        }

        [Test]
        public void ShouldBeAbleToBetOnNumbersFromTwoToTwelve_WhenGameHasTwoDices()
        {
            var player = new Player();
            player.BuyChips(100);
            player.Join(new Game(numberOfDices: 2));

            var isSuccessfulBet2 = player.Bet(number: 2, chipsAmount: 10);
            var isSuccessfulBet7 = player.Bet(number: 7, chipsAmount: 10);
            var isSuccessfulBet12 = player.Bet(number: 12, chipsAmount: 10);

            Assert.IsTrue(isSuccessfulBet2);
            Assert.IsTrue(isSuccessfulBet7);
            Assert.IsTrue(isSuccessfulBet12);

        }

        [Test]
        public void ShouldNotBeAbleToBetOnNumbersOtherThanTwoToTwelve_WhenGameHasTwoDices()
        {
            var player = new Player();
            player.BuyChips(100);
            player.Join(new Game(numberOfDices: 2));

            var isRejectedBet1 = player.Bet(number: -1, chipsAmount: 10);
            var isRejectedBet2 = player.Bet(number: 1, chipsAmount: 10);
            var isRejectedBet3 = player.Bet(number: 13, chipsAmount: 10);

            Assert.IsFalse(isRejectedBet1);
            Assert.IsFalse(isRejectedBet2);
            Assert.IsFalse(isRejectedBet3);
        }

        [Test]
        public void ShouldLoseTheGame_WhenWrongBet()
        {
            var game = Create.GameBuilder
                .WithDiceWhichAlwaysDropsOne()
                .Build();
            var player = Create.PlayerBuilder
                .WithChips(100)
                .WithGame(game)
                .WithBetOnNumber(2)
                .Build();

            game.Play();

            Assert.AreEqual(90, player.CurrentChips);
        }

        [Test]
        public void ShouldBeAbleToWinSixBets_WhenMakeBetOnLuckyNumber()
        {
            var game = Create.GameBuilder
                .WithDiceWhichAlwaysDropsOne()
                .Build();
            var player = Create.PlayerBuilder
                .WithChips(100)
                .WithGame(game)
                .WithBetOnNumberWithAmount(number: 1, chipsAmount: 10)
                .Build();

            game.Play();

            Assert.AreEqual(150, player.CurrentChips);
        }

        [Test]
        public void ShouldBeAbleToWinOnlyBetsOnLuckyNumber_WhenMakeDifferentBets()
        {
            var game = Create.GameBuilder
                .WithDiceWhichAlwaysDropsOne()
                .Build();
            var player = Create.PlayerBuilder
                .WithChips(100)
                .WithGame(game)
                .WithBetOnNumberWithAmount(number: 1, chipsAmount: 10)
                .WithBetOnNumberWithAmount(number: 2, chipsAmount: 10)
                .Build();

            game.Play();

            Assert.AreEqual(140, player.CurrentChips);
        }
    }
}