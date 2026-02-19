using SpaceInvaders.Common;
using SpaceInvaders.contracts;
using SpaceInvaders.Menu.Common;
using SpaceInvaders.System;

namespace SpaceInvaders.Menu.CreditsMenu
{
    public class CreditsMenuBehavior : IScreenBehavior<MenuState>
    {
        public IScreenBehaviorResult<MenuState> HandleInput(InputCommand input, MenuState state)
        {
            return input.Type switch
            {
                SystemInputCommandType.ESCAPE => new MenuBehaviorResult(state, ScreenType.MainMenu),
                SystemInputCommandType.ENTER => new MenuBehaviorResult(state, ScreenType.MainMenu),
                _ => new MenuBehaviorResult(state)
            };
        }

        public IScreenBehaviorResult<MenuState> Update(MenuState state) => new MenuBehaviorResult(state);
    }
}
