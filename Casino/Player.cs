namespace Casino
{
    public class Player
    {
        public void Join(Game game)
        {
            IsInGame = true;
        }

        public void Leave()
        {
            IsInGame = false;
        }

        public bool? IsInGame { get; set; }
    }
}