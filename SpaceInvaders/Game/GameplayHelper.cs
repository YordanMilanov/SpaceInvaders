using SpaceInvaders.Config;
using System.Collections.Immutable;

namespace SpaceInvaders.Game
{
    public class GameplayHelper
    {
        public static ImmutableList<Invader> InitInvaders()
        {
            var invaders = ImmutableList.CreateBuilder<Invader>();

            for (int i = 0; i < Configuration.ScreenWidth; i++)
            {
                if(i % 2 == 0)
                {
                    invaders.Add(new Invader(i, 2));
                }
            }

            return invaders.ToImmutable();
        }
    }
}
