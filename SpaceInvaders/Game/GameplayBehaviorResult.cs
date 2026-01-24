using SpaceInvaders.Common;
using SpaceInvaders.contracts;

namespace SpaceInvaders.Game
{
    public record GameBehaviorResult(GameState State, ScreenType? NavigateTo) : IScreenBehaviorResult<GameState>;
}
