﻿using NUnit.Framework;

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
    }
}