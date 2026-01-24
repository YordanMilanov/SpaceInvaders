using SpaceInvaders.Common;
using SpaceInvaders.contracts;
using SpaceInvaders.Game;
using SpaceInvaders.Menu.Common;
using SpaceInvaders.Menu.MainMenu;

namespace SpaceInvaders.System
{
    class ScreenManagerFactory
    {
        public static ScreenManager Create()
        {
            var screens = new Dictionary<ScreenType, IScreen>
            {
                [ScreenType.MainMenu] = new MenuScreen(
                    new MenuState(),
                    new MainMenuBehavior()),

                [ScreenType.Gameplay] =
                    new GameplayScreen(new GameState())
            };

            return new ScreenManager(screens, ScreenType.MainMenu);
        }
    }
}
