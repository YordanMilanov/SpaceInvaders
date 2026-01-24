using SpaceInvaders.Common;
using SpaceInvaders.System;

namespace SpaceInvaders.contracts
{
    public abstract class ScreenBase : IScreen
    {
        private IScreenState _screenState;
        private readonly IScreenBehavior<IScreenState> _screenBehavior;
        private readonly IFrameGenerator<IScreenState> _frameGenerator;

        public event Action<ScreenType>? OnScreenStateChanged;

        public ScreenBase(IScreenState screenState, IScreenBehavior<IScreenState> behavior, IFrameGenerator<IScreenState> frameGenerator)
        {
            _screenState = screenState;
            _screenBehavior = behavior;
            _frameGenerator = frameGenerator;
        }

        public void HandleInput(InputCommand input)
        {
            var result = _screenBehavior.Handle(input, _screenState);
            _screenState = result.State;

            if (result.NavigateTo.HasValue)
                OnScreenStateChanged?.Invoke(result.NavigateTo.Value);
        }

        public virtual void Update()
        {
            // blinking arrows, animations
        }

        public virtual string Render() => _frameGenerator.GenerateFrame(_screenState);
    }

}
