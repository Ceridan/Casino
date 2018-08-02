using System.Collections.Generic;
using System.Linq;
using Moq;

namespace Casino.Tests.DSL
{
    public class GameBuilder
    {
        private readonly List<Player> _players = new List<Player>();
        private IDice _dice;

        public GameBuilder WithDiceWhichAlwaysDropsOne()
        {
            var dice = new Mock<IDice>();
            dice.Setup(x => x.GetDiceDropNumber()).Returns(1);
            _dice = dice.Object;
            return this;
        }

        public GameBuilder WithSixJoinedPlayers()
        {
            _players.AddRange(Enumerable.Range(0, 6).Select(s => new Player()));
            return this;
        }

        public Game Build()
        {
            var game = _dice != null ? new Game(_dice) : new Game();

            foreach (var player in _players)
            {
                player.Join(game);
            }

            return game;
        }
    }
}