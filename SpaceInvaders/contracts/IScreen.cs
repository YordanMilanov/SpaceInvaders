using SpaceInvaders.Common;
using SpaceInvaders.System;

namespace SpaceInvaders.contracts
{
    public interface IScreen
    {
        void HandleInput(InputCommand input);
        void Update();
        string Render();

        public event Action<ScreenType>? OnScreenStateChanged;

    }
}
