using SpaceInvaders.Common;
using SpaceInvaders.contracts;
using SpaceInvaders.System;

namespace SpaceInvaders.Menu.Common
{
    class MenuScreen : IMenuScreen
    {
        private MenuState _menuState;
        private readonly IMenuBehavior _menuBehavior;

        // Event raised when a screen change is requested
        public event Action<ScreenState>? ScreenChanged;

        public MenuScreen(MenuState initialState, IMenuBehavior behavior)
        {
            _menuState = initialState;
            _menuBehavior = behavior;
        }

        public void HandleInput(InputCommand input)
        {
            //Update 
            var menuBehaviorResult = _menuBehavior.Handle(input, _menuState);
           
            // Update only the menu state
            _menuState = menuBehaviorResult.State;

            // Raise event if a screen change is requested
            if (menuBehaviorResult.ScreenState.HasValue)
            {
                ScreenChanged?.Invoke(menuBehaviorResult.ScreenState.Value);
            }
        }

        public void Update() {
            //Make a blinking selection arrows ><
        }

        public string Render(ScreenState currentScreen)
            => MenuFrameGenerator.GenerateFrame(_menuState, currentScreen);

        public void ResetState() => _menuState = new MenuState(); // restore initial state on leave
    }

}
