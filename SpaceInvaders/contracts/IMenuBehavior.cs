using SpaceInvaders.Menu.Common;
using SpaceInvaders.System;

namespace SpaceInvaders.contracts
{
    interface IMenuBehavior
    {
        MenuBehaviorResult Handle(InputCommand input, MenuState state);
    }
}
