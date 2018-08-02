using System.Collections.Generic;

namespace Casino
{
    public class Game
    {
        private readonly List<Player> _players = new List<Player>();
        private readonly IDice _dice;

        public Game()
        {
            _dice = new Dice();
        }

        public Game(IDice dice)
        {
            _dice = dice;
        }

        public int CasinoChips { get; set; }

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