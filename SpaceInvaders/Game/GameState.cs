namespace SpaceInvaders.Game
{
    public class GameState
    {
        public Player Player { get; set; } = new(10);
        public List<Bullet> Bullets { get; } = new();
        public List<Invader> Invaders { get; } = new();
    }

    public record Player(int X);

    public record Bullet(int X, int Y);

    public record Invader(int X, int Y);

}
