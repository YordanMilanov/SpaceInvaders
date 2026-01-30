using SpaceInvaders.Common;
using SpaceInvaders.contracts;
using SpaceInvaders.Game;
using SpaceInvaders.Menu;
using SpaceInvaders.Menu.Common;
using SpaceInvaders.Menu.GameOver;
using SpaceInvaders.Menu.MainMenu;
using SpaceInvaders.Menu.PauseMenu;

namespace SpaceInvaders.System
{
    public class ScreenFactory : IScreenFactory
    {
        public IScreen Create(ScreenType state)
            => state switch
            {
                ScreenType.MainMenu => InitMainMenuScreen(),
                ScreenType.Gameplay => InitGameplayScreen(),
                ScreenType.GameOver => InitGameOverScreen(),
                ScreenType.PauseMenu => InitPauseMenuScreen(),
                _ => InitMainMenuScreen()
            };


        private MainMenuScreen InitMainMenuScreen() => new MainMenuScreen(
                    new MenuState() { CurrentOption = 1, ScreenState = ScreenType.MainMenu },
                    new MainMenuBehavior(),
                    new MenuFrameGenerator());

        private GameplayScreen InitGameplayScreen() => new GameplayScreen(
                    new GameState(),
                    new GameplayBehavior(),
                    new GameplayFrameGenerator());

        private GameOverScreen InitGameOverScreen() => new GameOverScreen(
                    new MenuState() { CurrentOption = 1, ScreenState = ScreenType.GameOver },
                    new GameOverBehavior(),
                    new MenuFrameGenerator());

        private PauseMenuScreen InitPauseMenuScreen() => new PauseMenuScreen(
            new MenuState() { CurrentOption = 1, ScreenState = ScreenType.PauseMenu },
            new PauseMenuBehavior(),
            new MenuFrameGenerator());
    }
}
