using SpaceInvaders.Common;
using SpaceInvaders.contracts;

namespace SpaceInvaders.System
{
    class ScreenManager
    {
        private readonly IScreenFactory _factory;
        private readonly Stack<IScreen> _screens = new();

        public ScreenManager(IScreenFactory factory, ScreenType initialState)
        {
            _factory = factory;
            AddScreen(initialState);
        }

        /// <summary>
        /// Stack-Push based. Adds a new screen on top of the stack.
        /// </summary>
        private IScreen AddScreen(ScreenType state) {
            var screen = _factory.Create(state);
            screen.OnScreenStateChanged += OnScreenStateChanged;
            _screens.Push(screen);
            return screen;
        }

        /// <summary>
        /// Stack-Pop based. Removes the last used screen.
        /// </summary>
        private void RemoveScreen()
        {
            if (_screens.Count <= 1) return; // Prevent popping the last screen

            var old = _screens.Pop();
            old.OnScreenStateChanged -= OnScreenStateChanged;
        }

        private void OnScreenStateChanged(ScreenType newScreenType) => AddScreen(newScreenType);

        public void HandleInput(InputCommand input) => _screens.Peek().HandleInput(input);

        public void Update() => _screens.Peek().Update();

        public string Render() => _screens.Peek().Render();
    }
}
