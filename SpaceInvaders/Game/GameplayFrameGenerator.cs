using SpaceInvaders.Config;
using SpaceInvaders.contracts;
using System.Text;

namespace SpaceInvaders.Game
{
    public class GameplayFrameGenerator : IFrameGenerator<GameState>
    {
        /// <summary>
        /// Renders the current game state as a string representing the game frame.
        /// </summary>
        /// <returns>A string containing the visual representation of the current game frame, with each line corresponding to a row in the game area.(coordination system)</returns>
        public string GenerateFrame(GameState gameState)
        {
            var screenFrame = new char[Configuration.ScreenHeight, Configuration.ScreenWidth];

            //Initialize screen frame with empty spaces
            for (int y = 0; y < Configuration.ScreenHeight; y++)
                for (int x = 0; x < Configuration.ScreenWidth; x++)
                    screenFrame[y, x] = ' ';

            screenFrame[Configuration.PlayerStartPosition, gameState.Player.X] = Configuration.PlayerSymbol; // Add player plane

            // Add each bullet to the screen frame
            foreach (var b in gameState.Bullets)
                if (b.Y >= 0 && b.Y < Configuration.ScreenWidth)
                    screenFrame[b.Y, b.X] = Configuration.BulletSymbol;

            // Add each invader to the screen frame
            foreach (var i in gameState.Invaders)
                screenFrame[i.Y, i.X] = Configuration.InvaderSymbol;

            var rows = new string[Configuration.ScreenHeight];

            //Convert the 2D char array to a single string using multithreading
            Parallel.For(0, Configuration.ScreenHeight, y =>
            {
                var sbRow = new StringBuilder(Configuration.ScreenWidth);
                for (int x = 0; x < Configuration.ScreenWidth; x++)
                {
                    sbRow.Append(screenFrame[y, x]);
                }
                rows[y] = sbRow.ToString();
            });

            var result = string.Join(Environment.NewLine, rows);

            return result;
        }
    }
}
