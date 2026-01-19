namespace SpaceInvaders.Menu.MenuStatic
{
    public class MenuOptions
    {

        private static readonly string[] MainMenuOptions = new[]
        {
            "=== Space Invaders ===",
            "Start Game",
            "Game Records",
            "Settings",
            "Credits",
            "Exit"
        };

        private static readonly string[] PauseMenuOptions = new[]
        {
            "=== Game Paused ===",
            "Resume",
            "Restart",
            "Exit"
        };

        private static readonly string[] GameOverOptions = new[]
        {
            "=== Game Paused ===",
            "Resume",
            "Restart",
            "Exit"
        };

        public static string[] MainMenu() => AlignCenterMenu(MainMenuOptions);
        public static string[] PauseMenu() => AlignCenterMenu(PauseMenuOptions);
        public static string[] GameOverMenu() => AlignCenterMenu(GameOverOptions);

        public static int MainMenuLength => (int)LongestOptionLength(MainMenuOptions);
        public static int PauseMenuLength => (int)LongestOptionLength(PauseMenuOptions);
        public static int GameOverMenuLength => (int)LongestOptionLength(GameOverOptions);


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
