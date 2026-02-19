using SpaceInvaders.Config;
using SpaceInvaders.Extensions;
using SpaceInvaders.Menu.CreditsMenu;
using SpaceInvaders.Menu.Enums;
using SpaceInvaders.Menu.GameOver;
using SpaceInvaders.Menu.MainMenu;
using SpaceInvaders.Menu.PauseMenu;

namespace SpaceInvaders.Menu.Common
{
    public class MenuOptionsProvider
    {

        private static readonly string[] MainMenuOptions = new[]
        {
            MenuHeader.MainMenu.GetDisplayName(),
            MainMenuOption.StartGame.GetDisplayName(),
            MainMenuOption.Credits.GetDisplayName(),
            MainMenuOption.Exit.GetDisplayName(),
        };

        private static readonly string[] PauseMenuOptions = new[]
        {
            MenuHeader.PauseMenu.GetDisplayName(),
            PauseMenuOption.Resume.GetDisplayName(),
            PauseMenuOption.Restart.GetDisplayName(),
            PauseMenuOption.Exit.GetDisplayName(),
        };

        private static readonly string[] GameOverOptions = new[]
        {
            MenuHeader.GameOver.GetDisplayName(),
            GameOverMenuOption.Restart.GetDisplayName(),
            GameOverMenuOption.Exit.GetDisplayName(),
        };

        private static readonly string[] CreditsOptions = new[]
        {
            MenuHeader.Credits.GetDisplayName(),
            $"Dev: {Configuration.Developer}",
            CreditsMenuOption.Exit.GetDisplayName()
        };

        public static string[] MainMenu() => AlignCenterMenu(MainMenuOptions);
        public static string[] PauseMenu() => AlignCenterMenu(PauseMenuOptions);
        public static string[] GameOverMenu() => AlignCenterMenu(GameOverOptions);
        public static string[] CreditsMenu() => AlignCenterMenu(CreditsOptions);

        public static int MainMenuOptionsCount => MainMenuOptions.Length;
        public static int PauseMenuOptionsCount => PauseMenuOptions.Length;
        public static int GameOverMenuOptionsCount => GameOverOptions.Length;

        /// <summary>
        /// Aligns the menu options to the center based on the longest option length.
        /// </summary>
        private static string[] AlignCenterMenu(string[] menu)
        {
            int menuWidth = LongestOptionLength(menu);
            string[] centeredMenu = new string[menu.Length];
            for (int i = 0; i < menu.Length; i++)
            {
                int spaces = (menuWidth - menu[i].Length) / 2;
                var spaceBuffer = new string(' ', Math.Max(0, spaces));

                centeredMenu[i] = spaceBuffer + menu[i] + spaceBuffer;

                if (menu[i].Length % 2 != 0) centeredMenu[i] += " "; // Add extra space if the option length is odd 
            }
            return centeredMenu;
        }

        /// <summary>
        /// Gets the length of the longest option in the menu.
        /// </summary>
        private static int LongestOptionLength(string[] options)
        {
            int maxLength = 0;
            foreach (var option in options)
            {
                if (option.Length > maxLength)
                {
                    maxLength = option.Length;
                }
            }
            return maxLength;
        }
    }

}
