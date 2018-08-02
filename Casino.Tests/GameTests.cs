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
    }
}