using SpaceInvaders.Common;
using SpaceInvaders.System;

namespace SpaceInvaders.contracts
{
    public abstract class ScreenBase<TScreenState> : IScreen
        where TScreenState : IScreenState
    {
        protected TScreenState _screenState;
        protected readonly IScreenBehavior<TScreenState> _screenBehavior;
        protected readonly IFrameGenerator<TScreenState> _frameGenerator;

        public virtual event Action<ScreenType>? OnScreenStateChanged;

        public ScreenBase(TScreenState screenState, IScreenBehavior<TScreenState> behavior, IFrameGenerator<TScreenState> frameGenerator)
        {
            _screenState = screenState;
            _screenBehavior = behavior;
            _frameGenerator = frameGenerator;
        }

        public virtual void HandleInput(InputCommand input)
        {
            var result = _screenBehavior.HandleInput(input, _screenState);
            _screenState = result.State;

            if (result.NavigateTo.HasValue)
            {
                Console.Clear();
                RaiseScreenStateChanged(result.NavigateTo.Value);
            }
        }

        public virtual void Update() {
            var result = _screenBehavior.Update( _screenState);
            _screenState = result.State;

            if (result.NavigateTo.HasValue)
            {
                Console.Clear();
                RaiseScreenStateChanged(result.NavigateTo.Value);
            }
        } 

        public virtual string Render() => _frameGenerator.GenerateFrame(_screenState);

        protected virtual void RaiseScreenStateChanged(ScreenType type)
        {
            OnScreenStateChanged?.Invoke(type);
        }
    }

}
