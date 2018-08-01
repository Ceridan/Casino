namespace Casino
{
    public class Player
    {
        public void Join(Game game)
        {
            IsInGame = true;
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