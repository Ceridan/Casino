using System.Linq;
using Casino.Tests.DSL;
using NUnit.Framework;

namespace Casino.Tests
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void ShouldDeclineSeventhPlayer_WhenSixPlayersAlreadyJoined()
        {
            var game = Create.Game
                .WithSixJoinedPlayers()
                .Build();
            var declinedPlayer = new Player();

            declinedPlayer.Join(game);
            var playersCount = game.GetJoinedPlayers().Count;

            Assert.AreEqual(6, playersCount);
        }

        [Test]
        public void ShouldAcceptBetsOnlyDividedByFive()
        {
            var game = new Game();

            var acceptedBet = game.AcceptBet(15);

            Assert.IsTrue(acceptedBet);
        }

        [Test]
        public void ShouldNotAcceptBetsThatNotDividedByFive()
        {
            var game = new Game();

            var rejectedBet = game.AcceptBet(17);

            Assert.IsFalse(rejectedBet);
        }

        [Test]
        public void ShouldClaimChipsFromPlayer_WhenPlayerMakeBetOnWrongNumber()
        {
            var game = Create.Game
                .WithDiceWhichAlwaysDropsOne()
                .Build();
            var player = Create.Player
                .WithChips(100)
                .WithGame(game)
                .Build();

            player.Bet(number: 2, chipsAmount: 10);
            game.Play();

            Assert.AreEqual(10, game.CasinoChips);
        }

        [Test]
        public void ShouldBeAbleToCreateGameWithTwoDices()
        {
            var game = new Game(numberOfDices: 2);

            Assert.AreEqual(2, game.DiceCount);
        }

        [Test]
        [TestCase(2, 36)]
        [TestCase(7, 6)]
        [TestCase(9, 9)]
        public void ShouldDecideWinningModifierBasedOnLuckyNumber_WhenGameWithTwoDices(int number, int modifier)
        {
            var game = new Game(numberOfDices: 2);

            var modifierForNumber = game.GetWinningModifier(winningNumber: number);

            Assert.AreEqual(modifier, modifierForNumber);
        }

        [Test]
        public void WhenPlayerJoinTheGame_ShouldIncreasePlayerCount()
        {
            var player = new Player();
            var game = new Game();

            player.Join(game);

            Assert.AreEqual(1, game.GetJoinedPlayers().Count);
        }

        [Test]
        public void WhenPlayerLeaveTheGame_ShouldDecreasePlayerCount()
        {
            var player = new Player();
            var game = new Game();
            player.Join(game);

            player.Leave(game);

            Assert.AreEqual(0, game.GetJoinedPlayers().Count);
        }
    }
}