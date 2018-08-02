namespace Casino.Tests.DSL
{
    public static class Create
    {
        public static GameBuilder GameBuilder => new GameBuilder();
        public static PlayerBuilder PlayerBuilder => new PlayerBuilder();
    }
}