using SpaceInvaders.Common;
using SpaceInvaders.contracts;
using SpaceInvaders.Menu.Common;
using SpaceInvaders.Menu.Enums;
using SpaceInvaders.System;

namespace SpaceInvaders.Menu.MainMenu
{

    /// <summary>
    /// Contains rules for this screen. defines what happens on input.
    /// </summary>
    class MainMenuBehavior : IScreenBehavior<MenuState>
    {
        public IScreenBehaviorResult<MenuState> Handle(InputCommand input, MenuState state)
        {
            return input.Type switch
            {
                SystemInputCommandType.UP => new (state with { CurrentOption = Math.Max(1, state.CurrentOption - 1) }, state.ScreenState),
                SystemInputCommandType.DOWN => new (state with { CurrentOption = Math.Min(MenuOptionsProvider.MainMenuOptionsCount - 1, state.CurrentOption + 1)}, state.ScreenState),
                SystemInputCommandType.ENTER => HandleEnter(state),
                SystemInputCommandType.ESCAPE => throw new OperationCanceledException(),
                _ => new (state , null)
            };
        }

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
