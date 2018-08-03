namespace Casino.Tests.DSL
{
    public static class Create
    {
        public static GameBuilder Game => new GameBuilder();
        public static PlayerBuilder Player => new PlayerBuilder();
    }
}