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
            var game = Create.GameBuilder
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
            var game = Create.GameBuilder
                .WithDiceWhichAlwaysDropsOne()
                .Build();
            Create.PlayerBuilder
                .WithChips(100)
                .WithGame(game)
                .WithBetOnNumberWithAmount(number: 2, chipsAmount: 10)
                .Build();

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
        public void ShouldDecideWinningModifierBasedOnLuckyNumber_WhenGameWithTwoDices()
        {
            var game = new Game(numberOfDices: 2);

            var modifierForNumber2 = game.GetWinningModifier(winningNumber: 2);
            var modifierForNumber7 = game.GetWinningModifier(winningNumber: 7);
            var modifierForNumber9 = game.GetWinningModifier(winningNumber: 9);

            Assert.AreEqual(36, modifierForNumber2);
            Assert.AreEqual(6, modifierForNumber7);
            Assert.AreEqual(9, modifierForNumber9);
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