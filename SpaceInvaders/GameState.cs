namespace SpaceInvaders
{
    class GameState
    {
        public Player Player { get; set; } = new(10);
        public List<Bullet> Bullets { get; } = new();
        public List<Invader> Invaders { get; } = new();
    }

    record Player(int X);

    record Bullet(int X, int Y);

    record Invader(int X, int Y);

}
