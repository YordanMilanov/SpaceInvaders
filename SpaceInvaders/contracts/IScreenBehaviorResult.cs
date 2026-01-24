using SpaceInvaders.Common;

namespace SpaceInvaders.contracts
{
    public interface IScreenBehaviorResult<TScreenState>
        where TScreenState : IScreenState
    {
        /// <summary>
        /// The state of the screen
        /// </summary>
        TScreenState State { get; }

        /// <summary>
        /// Optional screen transition
        /// </summary>
        ScreenType? NavigateTo { get; }
    }
}