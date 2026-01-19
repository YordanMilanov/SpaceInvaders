using SpaceInvaders.Common;
using SpaceInvaders.Menu.MenuStatic;
using System.Text;

namespace SpaceInvaders.Menu
{
    public static class MenuFrameGenerator
    {
        /// <summary>
        /// Renders the current game state as a string representing the game frame.
        /// </summary>
        /// <returns>A string containing the visual representation of the current game frame, with each line corresponding to a row in the game area.(coordination system)</returns>
        public static string GenerateFrame(MenuState menuState)
        {
            string[] rows = menuState.state switch
            {
                SystemState.MainMenu => MenuOptions.MainMenu(),
                SystemState.PauseMenu => MenuOptions.PauseMenu(),
                SystemState.GameOverMenu => MenuOptions.GameOverMenu(),
                _ => Array.Empty<string>(),
            };

            rows = SelectMenuOption(rows, menuState.CurrentOption.position);

            return string.Join(Environment.NewLine, rows);
        }

        /// <summary>
        /// Adds arrows to the selected menu option to indicate selection.
        /// </summary>
        private static string[] SelectMenuOption(string[] lines, int selectedOptionIndex)
        {
            if(selectedOptionIndex > 0 && selectedOptionIndex < lines.Length) lines[selectedOptionIndex] = ">" + lines[selectedOptionIndex].Substring(1, lines[selectedOptionIndex].Length - 2) + "<";
            return lines;
        }
    }
}
