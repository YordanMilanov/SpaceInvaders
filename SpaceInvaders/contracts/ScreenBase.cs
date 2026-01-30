using SpaceInvaders.Common;
using SpaceInvaders.System;

namespace SpaceInvaders.contracts
{
    public abstract class ScreenBase<TScreenState> : IScreen
        where TScreenState : IScreenState
    {
        private TScreenState _screenState;
        private readonly IScreenBehavior<TScreenState> _screenBehavior;
        private readonly IFrameGenerator<TScreenState> _frameGenerator;

        public event Action<ScreenType>? OnScreenStateChanged;

        public ScreenBase(TScreenState screenState, IScreenBehavior<TScreenState> behavior, IFrameGenerator<TScreenState> frameGenerator)
        {
            _screenState = screenState;
            _screenBehavior = behavior;
            _frameGenerator = frameGenerator;
        }

        public void HandleInput(InputCommand input)
        {
            var result = _screenBehavior.HandleInput(input, _screenState);
            _screenState = result.State;

            if (result.NavigateTo.HasValue)
            {
                Console.Clear();
                OnScreenStateChanged?.Invoke(result.NavigateTo.Value);
            }
        }

        public virtual void Update() {
            var result = _screenBehavior.Update( _screenState);
            _screenState = result.State;

            if (result.NavigateTo.HasValue)
            {
                Console.Clear();
                OnScreenStateChanged?.Invoke(result.NavigateTo.Value);
            }
        } 

        public virtual string Render() => _frameGenerator.GenerateFrame(_screenState);
    }

}
