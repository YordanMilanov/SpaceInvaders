using SpaceInvaders.Common;

namespace SpaceInvaders.Menu.Common
{
    public record MenuState
    {
        public int CurrentOption { get; init; } = 1;
        //public bool HighlightOn { get; init; } = true; // for blinking cursor
    }
}
