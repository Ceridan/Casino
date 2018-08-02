using System.Collections.Generic;

namespace Casino
{
    public class Game
    {
        private readonly List<Player> _players = new List<Player>();
        private readonly IDice _dice;

        public Game(int numberOfDices = 1)
        {
            _dice = new Dice();
            DiceCount = numberOfDices;
        }

        public Game(IDice dice, int numberOfDices = 1)
        {
            _dice = dice;
            DiceCount = numberOfDices;
        }

        public int CasinoChips { get; set; }
        public int DiceCount { get; }

        public List<Player> GetJoinedPlayers()
        {
            return _players;
        }

        public void AddPlayer(Player player)
        {
            if (_players.Count < 6)
                _players.Add(player);
        }

        public bool AcceptBet(int chipsAmount)
        {
            return chipsAmount % 5 == 0;
        }

        public void Play()
        {
            var luckyNumber = _dice.GetDiceDropNumber();

            foreach (var player in _players)
            {
                foreach (var bet in player.CurrentBets)
                {
                    if (bet.Number == luckyNumber)
                    {
                        player.CurrentChips += bet.ChipsAmount * 6;
                    }
                    else
                    {
                        CasinoChips += bet.ChipsAmount;
                    }
                }

                player.CurrentBets.Clear();
            }
        }
    }
}