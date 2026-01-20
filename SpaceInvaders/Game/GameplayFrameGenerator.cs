using System.Text;

namespace SpaceInvaders.Game
{
    public static class GameplayFrameGenerator
    {
        /// <summary>
        /// Renders the current game state as a string representing the game frame.
        /// </summary>
        /// <returns>A string containing the visual representation of the current game frame, with each line corresponding to a row in the game area.(coordination system)</returns>
        public static string GenerateFrame(GameState gameState)
        {
            var screenFrame = new char[12, 22];

            //Initialize screen frame with empty spaces
            for (int y = 0; y < 12; y++)
                for (int x = 0; x < 22; x++)
                    screenFrame[y, x] = ' ';

            screenFrame[10, gameState.Player.X] = 'A'; // Add player plane

            // Add each bullet to the screen frame
            foreach (var b in gameState.Bullets)
                if (b.Y >= 0 && b.Y < 12)
                    screenFrame[b.Y, b.X] = '|';

            // Add each invader to the screen frame
            foreach (var i in gameState.Invaders)
                screenFrame[i.Y, i.X] = 'M';


            var rows = new string[12]; // one slot per row

            //Convert the 2D char array to a single string using multiple threads
            Parallel.For(0, 12, y =>
            {
                var sbRow = new StringBuilder(22); // each row has its own StringBuilder
                for (int x = 0; x < 22; x++)
                {
                    sbRow.Append(screenFrame[y, x]); // append each char in this row
                }
                rows[y] = sbRow.ToString(); // save completed row
            });

            // Combine all rows into final string
            var result = string.Join(Environment.NewLine, rows);

            return result;
        }
    }
}
