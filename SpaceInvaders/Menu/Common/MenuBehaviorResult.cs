using SpaceInvaders.Common;
using SpaceInvaders.contracts;

namespace SpaceInvaders.Menu.Common
{
    public record MenuBehaviorResult(MenuState State, ScreenType? NavigateTo) : IScreenBehaviorResult<MenuState>;
}
