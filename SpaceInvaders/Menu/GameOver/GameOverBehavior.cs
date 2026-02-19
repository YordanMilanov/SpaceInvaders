using SpaceInvaders.Common;
using SpaceInvaders.contracts;
using SpaceInvaders.Menu.Common;
using SpaceInvaders.System;

namespace SpaceInvaders.Menu.GameOver
{
    public class GameOverBehavior : IScreenBehavior<MenuState>
    {
        public IScreenBehaviorResult<MenuState> HandleInput(InputCommand input, MenuState state)
        {
            return input.Type switch
            {
                SystemInputCommandType.UP => new MenuBehaviorResult(state with { CurrentOption = Math.Max(1, state.CurrentOption - 1) }),
                SystemInputCommandType.DOWN => new MenuBehaviorResult(state with { CurrentOption = Math.Min(MenuOptionsProvider.GameOverMenuOptionsCount - 1, state.CurrentOption + 1) }),
                SystemInputCommandType.ENTER => HandleEnter(state),
                _ => new MenuBehaviorResult(state)
            };
        }

        public IScreenBehaviorResult<MenuState> Update(MenuState state) => new MenuBehaviorResult(state);

        private static MenuBehaviorResult HandleEnter(MenuState state) =>
            (GameOverMenuOption)state.CurrentOption switch
            {
                GameOverMenuOption.Restart => new MenuBehaviorResult(state, ScreenType.Gameplay),
                GameOverMenuOption.Exit => new MenuBehaviorResult(state, ScreenType.MainMenu),
                _ => new MenuBehaviorResult(state, null)
            };
    }
}
