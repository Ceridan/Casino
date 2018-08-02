namespace Casino.Tests.DSL
{
    public class PlayerBuilder
    {
        private readonly Player _player = new Player();

        public PlayerBuilder WithChips(int chipsAmount)
        {
            _player.BuyChips(chipsAmount);
            return this;
        }

        public PlayerBuilder WithGame(Game game)
        {
            _player.Join(game);
            return this;
        }

        public PlayerBuilder WithBetOnNumber(int number)
        {
            _player.Bet(number: number, chipsAmount: 10);
            return this;
        }

        public Player Build()
        {
            return _player;
        }
    }
}