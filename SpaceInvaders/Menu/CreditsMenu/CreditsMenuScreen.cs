using SpaceInvaders.contracts;
using SpaceInvaders.Menu.Common;

namespace SpaceInvaders.Menu.CreditsMenu
{
    public class CreditsMenuScreen : ScreenBase<MenuState>
    {
        public CreditsMenuScreen(MenuState menuState, CreditsMenuBehavior behavior, MenuFrameGenerator frameGenerator) : base(menuState, behavior, frameGenerator)
        {
        }
    }
}
