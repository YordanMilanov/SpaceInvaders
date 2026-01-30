using SpaceInvaders.Config;
using SpaceInvaders.contracts;
using System.Collections.Immutable;

namespace SpaceInvaders.Game
{
    public record GameState : IScreenState
    {
        public Player Player { get; init; } = new Player(Configuration.PlayerStartPosition);
       
        public ImmutableList<Bullet> Bullets { get; init; } = ImmutableList<Bullet>.Empty;

        public ImmutableList<Invader> Invaders { get; init; } = GameplayHelper.InitInvaders();

        public bool IsPaused { get; init; } = false;

        public int InvaderSideMoveFrameInterval { get; init; } = Configuration.InvaderSideMoveFrameInterval;
        public int InvaderDownMoveFrameInterval { get; init; } = Configuration.InvaderDownMoveFrameInterval;
    }

    public record Player(int X);

    public record Bullet(int X, int Y);

    public record Invader(int X, int Y);

}
