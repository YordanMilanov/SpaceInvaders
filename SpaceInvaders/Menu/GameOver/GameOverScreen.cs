using SpaceInvaders.contracts;
using SpaceInvaders.Menu.Common;

namespace SpaceInvaders.Menu.GameOver
{
    public class GameOverScreen : ScreenBase<MenuState>
    {
        public GameOverScreen(MenuState menuState, GameOverBehavior behavior, MenuFrameGenerator frameGenerator) : base(menuState, behavior, frameGenerator)
        {
        }
    }
}
