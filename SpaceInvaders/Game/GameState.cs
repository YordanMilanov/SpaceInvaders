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

        public int Score { get; init; } = 0;

        public bool IsPaused { get; init; } = false;

        public bool IsGameOver { get; init; } = false;

        public int InvaderSideMoveFrameIntervalCounter { get; init; } = Configuration.InvaderSideMoveFrameInterval;
        public int InvaderDownMoveFrameIntervalCounter { get; init; } = Configuration.InvaderDownMoveFrameInterval;
        public int CurrentSideMoveFrameInterval { get; init; } = Configuration.InvaderSideMoveFrameInterval;
        public int CurrentDownMoveFrameInterval { get; init; } = Configuration.InvaderDownMoveFrameInterval;
    }

    public record Player(int X);

    public record Bullet(int X, int Y);

    public record Invader(int X, int Y);

}
