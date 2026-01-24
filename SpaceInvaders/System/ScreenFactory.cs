using SpaceInvaders.Common;
using SpaceInvaders.contracts;
using SpaceInvaders.Menu.Common;

namespace SpaceInvaders.System
{
    public class ScreenFactory : IScreenFactory
    {
        public IScreen Create(ScreenType state)
            => state switch
            {
                ScreenType.MainMenu => new MenuScreen(),
            };
           
    }
}
