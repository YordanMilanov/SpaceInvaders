using SpaceInvaders.Common;
using SpaceInvaders.contracts;

namespace SpaceInvaders.Game
{
    public record GameplayBehaviorResult(GameState State, ScreenType? NavigateTo = null) : IScreenBehaviorResult<GameState>;
}
