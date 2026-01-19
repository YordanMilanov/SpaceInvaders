using SpaceInvaders.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders.Game
{
    public static class GameInputProcessor
    {
        /// <summary>
        /// Processes all pending input commands and updates the game state accordingly.
        /// </summary>
        /// <remarks>This method handles player movement, shooting, and pause commands by reading from the input </remarks>
        /// <exception cref="OperationCanceledException">Thrown if a pause command is received during input processing.</exception>
        public static GameState ProcessGameplayInput(InputCommand input, GameState state)
        {
            switch (input.Type)
            {
                case SystemInputCommandType.LEFT:
                    state.Player = state.Player with { X = Math.Max(0, state.Player.X - 1) }; // Ensure player doesn't move out of bounds то the left, left limit 0
                    break;
                case SystemInputCommandType.RIGHT:
                    state.Player = state.Player with { X = Math.Min(20, state.Player.X + 1) }; // Ensure player doesn't move out of bounds to the right, not more than 20
                    break;
                case SystemInputCommandType.SPACE:
                    state.Bullets.Add(new Bullet(state.Player.X, 10)); // Bullet starts just above the player at Y=10
                    break;
                case SystemInputCommandType.ESCAPE: //TODO: Implement  Menu/Pause functionality
                    throw new OperationCanceledException();
            }
            return state;
        }

    }
}
