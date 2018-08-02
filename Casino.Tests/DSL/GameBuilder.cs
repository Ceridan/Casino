using Moq;

namespace Casino.Tests.DSL
{
    public class GameBuilder
    {
        private IDice _dice;

        public GameBuilder WithDiceWhichAlwaysDropsOne()
        {
            var dice = new Mock<IDice>();
            dice.Setup(x => x.GetDiceDropNumber()).Returns(1);
            _dice = dice.Object;
            return this;
        }

        public Game Build()
        {
            if (_dice != null)
                return new Game(_dice);

            return new Game();
        }
    }
}