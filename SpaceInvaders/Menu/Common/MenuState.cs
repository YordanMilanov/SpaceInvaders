using SpaceInvaders.Common;
using SpaceInvaders.contracts;

namespace SpaceInvaders.Menu.Common
{
    public record MenuState : IScreenState
    {
        public required int CurrentOption { get; init; } = 1;
        public required ScreenType ScreenState { get; init; }
        //public bool HighlightOn { get; init; } = true; // for blinking cursor
    }
}
