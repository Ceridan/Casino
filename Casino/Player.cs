using System.Collections.Generic;

namespace Casino
{
    public class Player
    {
        private int diceCount = 1;
        private readonly List<Bet> _currentBets = new List<Bet>();

        public bool IsInGame { get; set; }
        public int CurrentChips { get; set; }

        public IReadOnlyList<Bet> CurrentBets => _currentBets;

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

        public void ClearBets()
        {
            _currentBets.Clear();
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

            _currentBets.Add(new Bet { Number = number, ChipsAmount = chipsAmount });
            CurrentChips -= chipsAmount;
            return true;
        }
    }
}