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
            var screenFrame = InitScreenFrame(Configuration.ScreenWidth, Configuration.ScreenHeight);

            screenFrame[Configuration.PlayerStartPosition, gameState.Player.X] = Configuration.PlayerSymbol;

            screenFrame = AddBulletsToScreen(screenFrame, gameState.Bullets, Configuration.BulletSymbol);
            screenFrame = AddInvadersToScreen(screenFrame, gameState.Invaders, Configuration.InvaderSymbol);
            screenFrame = AddScoreToScreen(screenFrame, gameState.Score);


            return ConvertScreenFrameToString(screenFrame);
        }

        private char[,] InitScreenFrame(int width, int height)
        {
            var screenFrame = new char[height, width];

            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    screenFrame[y, x] = ' ';
            return screenFrame;
        }

        private char[,] AddScoreToScreen(char[,] screenFrame, int score)
        {
            var height = screenFrame.GetLength(0);
            var width = screenFrame.GetLength(1);

            string scoreText = $"Score {score}";

            for (int i = 0; i < scoreText.Length && i < width; i++)
            {
                screenFrame[0, i] = scoreText[i];
            }

            return screenFrame;
        }

        private char[,] AddBulletsToScreen(char[,] screenFrame, IEnumerable<Bullet> bullets, char bulletSymbol)
        {
            var height = screenFrame.GetLength(0);
            var width = screenFrame.GetLength(1);

            foreach (var b in bullets)
                if (b.Y >= 0 && b.Y < width)
                    screenFrame[b.Y, b.X] = bulletSymbol;

            return screenFrame;
        }

        private char[,] AddInvadersToScreen(char[,] screenFrame, IEnumerable<Invader> invaders, char invaderSymbol)
        {
            foreach (var i in invaders)
                screenFrame[i.Y, i.X] = invaderSymbol;

            return screenFrame;
        }

        private string ConvertScreenFrameToString(char[,] screenFrame)
        {
            var height = screenFrame.GetLength(0);
            var width = screenFrame.GetLength(1);

            var rows = new string[height];

            //Convert the 2D char array to a single string using multithreading
            Parallel.For(0, height, y =>
            {
                var sbRow = new StringBuilder(width);
                for (int x = 0; x < width; x++)
                {
                    sbRow.Append(screenFrame[y, x]);
                }
                rows[y] = sbRow.ToString();
            });
            return string.Join(Environment.NewLine, rows);
        }
    }
}
