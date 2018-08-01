using System.Linq;
using NUnit.Framework;

namespace Casino.Tests
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void ShouldDeclineSeventhPlayer_WhenSixPlayersAlreadyJoined()
        {
            var game = new Game();
            var joinedPlayers = Enumerable.Range(0, 6).Select(s => new Player());
            var declinedPlayer = new Player();

            foreach (var player in joinedPlayers)
            {
                player.Join(game);
            }

            declinedPlayer.Join(game);
            var playersCount = game.GetJoinedPlayers().Count;

            Assert.AreEqual(6, playersCount);
        }
    }
}