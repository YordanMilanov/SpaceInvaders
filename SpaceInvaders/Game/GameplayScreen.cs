using SpaceInvaders.contracts;
using SpaceInvaders.Menu.MainMenu;

namespace SpaceInvaders.Game
{
    class GameplayScreen : ScreenBase
    {
        public GameplayScreen(
            GameState gameState,
            MainMenuBehavior behavior,
            GameplayFrameGenerator frameGenerator)
            : base(gameState, behavior, frameGenerator)
        {
        }
    }
}
