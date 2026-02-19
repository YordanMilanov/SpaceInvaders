using SpaceInvaders.contracts;
using SpaceInvaders.Menu.Common;
using SpaceInvaders.System;

namespace SpaceInvaders.Menu.PauseMenu
{
    public class PauseMenuScreen : ScreenBase<MenuState>
    {
        public event Action? OnRestartRequested;

        public PauseMenuScreen(MenuState menuState, PauseMenuBehavior behavior, MenuFrameGenerator frameGenerator) : base(menuState, behavior, frameGenerator){}

        public override void HandleInput(InputCommand input)
        {
            var result = _screenBehavior.HandleInput(input, _screenState);
            _screenState = result.State;

            if (result.NavigateTo.HasValue)
            {
                Console.Clear();

                if (result is PauseMenuBehaviorResult pauseMenuResult)
                {
                    if (pauseMenuResult.ShouldRestart)
                    {
                        OnRestartRequested!.Invoke();
                    }
                }

                RaiseScreenStateChanged(result.NavigateTo.Value);
            }
        }
    }
}
