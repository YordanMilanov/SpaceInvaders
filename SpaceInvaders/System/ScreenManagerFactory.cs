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
            var screens = new Dictionary<ScreenState, IScreen>
            {
                [ScreenState.MainMenu] = new MenuScreen(
                    new MenuState(),
                    new MainMenuBehavior()),

                [ScreenState.Gameplay] =
                    new GameplayScreen(new GameState())
            };

            return new ScreenManager(screens, ScreenState.MainMenu);
        }
    }
}
