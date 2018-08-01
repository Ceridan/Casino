namespace Casino
{
    public class Player
    {
        public void Join(Game game)
        {
            IsInGame = true;
        }

        public bool? IsInGame { get; set; }
    }
}