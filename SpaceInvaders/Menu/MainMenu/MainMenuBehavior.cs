using SpaceInvaders.Common;
using SpaceInvaders.contracts;
using SpaceInvaders.Menu.Common;
using SpaceInvaders.System;

namespace SpaceInvaders.Menu.MainMenu
{

    /// <summary>
    /// Contains rules for this screen. defines what happens on input.
    /// </summary>
    public class MainMenuBehavior : IScreenBehavior<MenuState>
    {
        public IScreenBehaviorResult<MenuState> HandleInput(InputCommand input, MenuState state)
        {
            return input.Type switch
            {
                SystemInputCommandType.UP => new MenuBehaviorResult(state with {CurrentOption = Math.Max(1, state.CurrentOption - 1)}),
                SystemInputCommandType.DOWN => new MenuBehaviorResult(state with {CurrentOption = Math.Min(MenuOptionsProvider.MainMenuOptionsCount - 1, state.CurrentOption + 1)}),
                SystemInputCommandType.ENTER => HandleEnter(state),
                SystemInputCommandType.ESCAPE => throw new OperationCanceledException(),
                _ => new MenuBehaviorResult(state)
            };
        }

        public IScreenBehaviorResult<MenuState> Update(MenuState state) => new MenuBehaviorResult(state); //TO DO: Implement if needed

        private static MenuBehaviorResult HandleEnter(MenuState state) =>
            (MainMenuOption)state.CurrentOption switch
            {
                MainMenuOption.StartGame => new MenuBehaviorResult(state, ScreenType.Gameplay),
                MainMenuOption.Credits => new MenuBehaviorResult(state, ScreenType.Credits),
                MainMenuOption.Exit => throw new OperationCanceledException(),
                _ => new MenuBehaviorResult(state, null)
            };
    }
}
