using SpaceInvaders.Common;
using SpaceInvaders.contracts;
using SpaceInvaders.Menu.Common;
using SpaceInvaders.System;

namespace SpaceInvaders.Menu.PauseMenu
{
    public class PauseMenuBehavior : IScreenBehavior<MenuState>
    {
        public IScreenBehaviorResult<MenuState> HandleInput(InputCommand input, MenuState state)
        {
            return input.Type switch
            {
                SystemInputCommandType.UP => new PauseMenuBehaviorResult(state with { CurrentOption = Math.Max(1, state.CurrentOption - 1) }),
                SystemInputCommandType.DOWN => new PauseMenuBehaviorResult(state with { CurrentOption = Math.Min(MenuOptionsProvider.PauseMenuOptionsCount - 1, state.CurrentOption + 1) }),
                SystemInputCommandType.ENTER => HandleEnter(state),
                _ => new PauseMenuBehaviorResult(state)
            };
        }

        public IScreenBehaviorResult<MenuState> Update(MenuState state) => new PauseMenuBehaviorResult(state); //TO DO: Implement if needed

        private static PauseMenuBehaviorResult HandleEnter(MenuState state) =>
            (PauseMenuOption)state.CurrentOption switch
            {
                PauseMenuOption.Resume => new PauseMenuBehaviorResult(state, ScreenType.Gameplay),
                PauseMenuOption.Restart => new PauseMenuBehaviorResult(state, ScreenType.Gameplay, true),
                PauseMenuOption.Exit => new PauseMenuBehaviorResult(state, ScreenType.MainMenu),
                _ => new PauseMenuBehaviorResult(state)
            };
    }
}
