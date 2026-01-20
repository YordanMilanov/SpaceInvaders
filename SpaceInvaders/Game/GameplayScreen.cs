using SpaceInvaders.Common;
using SpaceInvaders.contracts;
using SpaceInvaders.Menu;
using SpaceInvaders.Menu.Common;
using SpaceInvaders.System;

namespace SpaceInvaders.Game
{
    class GameplayScreen : IScreen
    {
        private GameState _state;

        // Event raised when a screen change is requested
        public event Action<ScreenState>? ScreenChanged;

        public GameplayScreen(GameState state)
        {
            _state = state;
        }

        public void HandleInput(InputCommand input)
        {
            _state = GameplayInputProcessor.ProcessGameplayInput(input, _state);
        }

        public void Update()
        {
            if (!_state.IsPaused)
                _state = GameplayStateUpdater.UpdateBullets(_state);
        }

        public string Render(ScreenState updatedScreen) =>
            _state.IsPaused
                ? MenuFrameGenerator.GenerateFrame(new MenuState(), updatedScreen) // or overlay
                : GameplayFrameGenerator.GenerateFrame(_state);
    }
}
