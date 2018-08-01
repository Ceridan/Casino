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