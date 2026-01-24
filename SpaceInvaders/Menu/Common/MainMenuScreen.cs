using SpaceInvaders.Common;
using SpaceInvaders.contracts;
using SpaceInvaders.Menu.MainMenu;

namespace SpaceInvaders.Menu.Common
{
    public class MainMenuScreen : ScreenBase
    {
        public MainMenuScreen(MenuState menuState, MainMenuBehavior behavior, MenuFrameGenerator frameGenerator) : base(menuState, behavior, frameGenerator)
        {
        }
    }

}
