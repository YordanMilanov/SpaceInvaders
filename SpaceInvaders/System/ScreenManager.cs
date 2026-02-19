using SpaceInvaders.Common;
using SpaceInvaders.contracts;
using SpaceInvaders.Menu.PauseMenu;

namespace SpaceInvaders.System
{
    class ScreenManager
    {
        private readonly IScreenFactory _screenFactory;
        private readonly Stack<IScreen> _screens = new();

        public ScreenManager(ScreenType initialState)
        {
            _screenFactory = new ScreenFactory();
            AddScreen(initialState);
        }

        /// <summary>
        /// Stack-Push based. Adds a new screen on top of the stack.
        /// </summary>
        private IScreen AddScreen(ScreenType screenType) {
            var screen = _screenFactory.Create(screenType);
            screen.OnScreenStateChanged += OnScreenStateChanged;

            if (screen is PauseMenuScreen pauseMenuScreen) { 
                pauseMenuScreen.OnRestartRequested += OnRestartRequested;
                screen = pauseMenuScreen;
            }

            _screens.Push(screen);
            return screen;
        }

        /// <summary>
        /// Stack-Pop based. Removes the last used screen.
        /// </summary>
        private void RemoveScreen()
        {
            IScreen? old;
            _screens.TryPop(out old);
            if(old is not null)
            {
                old.OnScreenStateChanged -= OnScreenStateChanged;

                if (old is PauseMenuScreen pauseMenuScreen)
                {
                    pauseMenuScreen.OnRestartRequested -= OnRestartRequested;
                }
            }
        }

        private void ResetScreens() => _screens.Clear();

        private void OnScreenStateChanged(ScreenType newScreenType) {

            if (newScreenType == ScreenType.Gameplay)
            {
                if(_screens.Peek() is PauseMenuScreen)
                {
                    RemoveScreen();
                    return;
                }
            }

            if (newScreenType != ScreenType.PauseMenu) {
                ResetScreens();
                AddScreen(newScreenType);
            }
            else
            {
                AddScreen(newScreenType);
            }
        }

        private void OnRestartRequested()
        {
            ResetScreens();
            AddScreen(ScreenType.Gameplay);
        }

        public void HandleInput(InputCommand input) => _screens.Peek().HandleInput(input);

        public void Update() => _screens.Peek().Update();

        public string Render() => _screens.Peek().Render();
    }
}
