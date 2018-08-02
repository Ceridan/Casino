using System.Collections.Generic;

namespace Casino
{
    public class Game
    {
        private readonly List<Player> _players = new List<Player>();

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
    }
}