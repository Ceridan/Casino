using System.Collections.Generic;

namespace Casino
{
    public class Player
    {
        public bool IsInGame { get; set; }
        public int CurrentChips { get; set; }
        public List<Bet> CurrentBets { get; } = new List<Bet>();

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

        public bool Bet(int number, int chipsAmount)
        {
            if (chipsAmount > CurrentChips)
                return false;

            CurrentBets.Add(new Bet { Number = number, ChipsAmount = chipsAmount });
            CurrentChips -= chipsAmount;
            return true;
        }
    }
}