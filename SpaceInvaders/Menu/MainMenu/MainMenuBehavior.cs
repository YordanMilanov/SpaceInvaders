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
    class MainMenuBehavior : IMenuBehavior
    {
        public MenuBehaviorResult Handle(InputCommand input, MenuState state)
        {
            return input.Type switch
            {
                SystemInputCommandType.UP => new (state with { CurrentOption = Math.Max(1, state.CurrentOption - 1) }, null),
                SystemInputCommandType.DOWN => new (state with { CurrentOption = Math.Min(MenuOptionsProvider.MainMenuOptionsCount - 1, state.CurrentOption + 1)}, null),
                SystemInputCommandType.ENTER => HandleEnter(state),
                SystemInputCommandType.ESCAPE => throw new OperationCanceledException(),
                _ => new (state , null)
            };
        }

        private static MenuBehaviorResult HandleEnter(MenuState state) =>
            (MainMenuOption)state.CurrentOption switch
            {
                MainMenuOption.StartGame => new MenuBehaviorResult(state, ScreenState.Gameplay),
                MainMenuOption.GameRecords => new MenuBehaviorResult(state, ScreenState.GameRecords),
                MainMenuOption.Settings => new MenuBehaviorResult(state, ScreenState.SettingsMenu),
                MainMenuOption.Credits => new MenuBehaviorResult(state, ScreenState.Credits),
                _ => new MenuBehaviorResult(state, null)
            };
    }
}
