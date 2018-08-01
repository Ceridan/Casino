namespace Casino
{
    public class Player
    {
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

        public bool IsInGame { get; set; }
    }
}