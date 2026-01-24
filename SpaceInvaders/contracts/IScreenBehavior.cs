using SpaceInvaders.Menu.Common;
using SpaceInvaders.System;

namespace SpaceInvaders.contracts
{
    public interface IScreenBehavior<TScreenState>
        where TScreenState : IScreenState
    {
        IScreenBehaviorResult<TScreenState> Handle(InputCommand input, TScreenState state);
    }
}
