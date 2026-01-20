using SpaceInvaders.Common;
using SpaceInvaders.contracts;
using SpaceInvaders.Menu.Common;

namespace SpaceInvaders.System
{
    class ScreenManager
    {
        private readonly Dictionary<ScreenState, IScreen> _screens;
        private IScreen _currentScreen;
        private ScreenState _currentScreenState;

        public ScreenManager(Dictionary<ScreenState, IScreen> screens, ScreenState start)
        {
            _screens = screens;
            _currentScreen = screens[start];

            // Subscribe to MenuScreen events
            if (_currentScreen is MenuScreen menuScreen)
                menuScreen.ScreenChanged += OnScreenChanged;
        }

        private void OnScreenChanged(ScreenState newScreen)
        {
            // Reset the old screen if it’s a MenuScreen
            if (_currentScreen is IMenuScreen oldScreen)
                oldScreen.ResetState();

            // Switch to the new screen
            if (_screens.TryGetValue(newScreen, out var screen))
            {
                _currentScreen = screen;
                if (_currentScreen is IScreen menuScreen)
                    menuScreen.ScreenChanged += OnScreenChanged;
            }

            _currentScreenState = newScreen;
        }

        public void HandleInput(InputCommand input)
        {
            _currentScreen.HandleInput(input);
        }

        public void Update() => _currentScreen.Update();

        public string Render() => _currentScreen.Render(_currentScreenState);
    }
}
