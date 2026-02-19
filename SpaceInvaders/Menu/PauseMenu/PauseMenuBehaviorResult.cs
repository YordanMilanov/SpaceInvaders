using SpaceInvaders.Common;
using SpaceInvaders.Menu.Common;

namespace SpaceInvaders.Menu.PauseMenu
{
    public record PauseMenuBehaviorResult : MenuBehaviorResult
    {
        public PauseMenuBehaviorResult(
            MenuState state,
            ScreenType? navigateTo = null,
            bool shouldRestart = false)
            : base(state, navigateTo)
        {
            ShouldRestart = shouldRestart;
        }

        public bool ShouldRestart { get; init; }
    }
}
