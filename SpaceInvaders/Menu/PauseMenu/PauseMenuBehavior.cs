using SpaceInvaders.Common;
using SpaceInvaders.contracts;
using SpaceInvaders.Menu.Common;
using SpaceInvaders.Menu.MainMenu;
using SpaceInvaders.System;

namespace SpaceInvaders.Menu.PauseMenu
{
    public class PauseMenuBehavior : IScreenBehavior<MenuState>
    {
        public IScreenBehaviorResult<MenuState> HandleInput(InputCommand input, MenuState state)
        {
            return input.Type switch
            {
                SystemInputCommandType.UP => new MenuBehaviorResult(state with { CurrentOption = Math.Max(1, state.CurrentOption - 1) }),
                SystemInputCommandType.DOWN => new MenuBehaviorResult(state with { CurrentOption = Math.Min(MenuOptionsProvider.PauseMenuOptionsCount - 1, state.CurrentOption + 1) }),
                SystemInputCommandType.ENTER => HandleEnter(state),
                _ => new MenuBehaviorResult(state)
            };
        }

        public IScreenBehaviorResult<MenuState> Update(MenuState state) => new MenuBehaviorResult(state); //TO DO: Implement if needed

        private static MenuBehaviorResult HandleEnter(MenuState state) =>
            (PauseMenuOption)state.CurrentOption switch
            {
                PauseMenuOption.Resume => new MenuBehaviorResult(state, ScreenType.Gameplay),
                PauseMenuOption.Restart => new MenuBehaviorResult(state, ScreenType.Gameplay),
                PauseMenuOption.Exit => new MenuBehaviorResult(state, ScreenType.MainMenu),
                _ => new MenuBehaviorResult(state, null)
            };
    }
}
