using System.Collections.Generic;

namespace Casino
{
    public class Player
    {
        private int diceCount = 1;

        public bool IsInGame { get; set; }
        public int CurrentChips { get; set; }
        public List<Bet> CurrentBets { get; } = new List<Bet>();

        public bool Join(Game game)
        {
            if (IsInGame)
            {
                return false;
            }

            if (game.AddPlayer(this))
            {
                diceCount = game.DiceCount;
                IsInGame = true;
                return true;
            }

            return false;
        }

        public bool Leave(Game game)
        {
            if (IsInGame && game.RemovePlayer(this))
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
            if (!IsInGame)
                return false;

            if (number < 1 * diceCount || number > 6 * diceCount)
                return false;

            if (chipsAmount > CurrentChips)
                return false;

            CurrentBets.Add(new Bet { Number = number, ChipsAmount = chipsAmount });
            CurrentChips -= chipsAmount;
            return true;
        }
    }
}