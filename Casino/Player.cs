namespace Casino
{
    public class Player
    {
        public bool IsInGame { get; set; }
        public int CurrentChips { get; set; }
        public int CurrentBet { get; set; }

        public bool Join(Game game)
        {
            if (IsInGame)
            {
                return false;
            }

            game.AddPlayer(this);
            IsInGame = true;
            return true;
        }

        public bool Leave()
        {
            if (IsInGame)
            {
                IsInGame = false;
                return true;
            }

            return false;
        }

        public void BuyChips(int chipsAmount)
        {
            CurrentChips = chipsAmount;
        }

        public void Bet(int chipsAmount)
        {
            CurrentBet = chipsAmount;
        }
    }
}