using SpaceInvaders.Common;
using SpaceInvaders.contracts;

namespace SpaceInvaders.Menu.Common
{
    public record MenuBehaviorResult(
    MenuState State,          // Updated menu data
    ScreenState? ScreenState   // Optional screen transition
);
}
