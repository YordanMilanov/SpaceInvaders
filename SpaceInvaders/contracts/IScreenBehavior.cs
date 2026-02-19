using SpaceInvaders.System;

namespace SpaceInvaders.contracts
{
    public interface IScreenBehavior<TScreenState>
        where TScreenState : IScreenState
    {
        IScreenBehaviorResult<TScreenState> HandleInput(InputCommand input, TScreenState state);
        IScreenBehaviorResult<TScreenState> Update(TScreenState state);
    }
}
