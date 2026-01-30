using SpaceInvaders.contracts;

namespace SpaceInvaders.Game
{
    class GameplayScreen : ScreenBase<GameState>
    {
        public GameplayScreen(
            GameState gameState,
            GameplayBehavior behavior,
            GameplayFrameGenerator frameGenerator)
            : base(gameState, behavior, frameGenerator)
        {
        }
    }
}
