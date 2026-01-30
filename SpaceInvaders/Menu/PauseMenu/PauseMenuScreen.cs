using SpaceInvaders.contracts;
using SpaceInvaders.Menu.Common;

namespace SpaceInvaders.Menu.PauseMenu
{
    public class PauseMenuScreen : ScreenBase<MenuState>
    {
        public PauseMenuScreen(MenuState menuState, PauseMenuBehavior behavior, MenuFrameGenerator frameGenerator) : base(menuState, behavior, frameGenerator)
        {
        }
    }
}
