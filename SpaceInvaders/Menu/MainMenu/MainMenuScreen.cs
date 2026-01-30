using SpaceInvaders.contracts;
using SpaceInvaders.Menu.Common;

namespace SpaceInvaders.Menu.MainMenu
{
    public class MainMenuScreen : ScreenBase<MenuState>
    {
        public MainMenuScreen(MenuState menuState, MainMenuBehavior behavior, MenuFrameGenerator frameGenerator) : base(menuState, behavior, frameGenerator)
        {
        }
    }

}
