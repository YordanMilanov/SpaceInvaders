namespace SpaceInvaders.Game
{
    public static class GameStateUpdater
    {
        public static GameState UpdateBullets(GameState gamestate)
        {
            for (int i = gamestate.Bullets.Count - 1; i >= 0; i--)
            {
                var b = gamestate.Bullets[i];
                b = b with { Y = b.Y - 1 };

                if (b.Y < 0)
                    gamestate.Bullets.RemoveAt(i);
                else
                    gamestate.Bullets[i] = b;
            }
            return gamestate;
        }
    }
}
