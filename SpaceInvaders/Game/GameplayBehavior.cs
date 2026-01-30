using SpaceInvaders.Config;
using SpaceInvaders.contracts;
using SpaceInvaders.System;
using System.Collections.Immutable;

namespace SpaceInvaders.Game
{
    public class GameplayBehavior : IScreenBehavior<GameState>
    {
        /// <summary>
        /// Processes all input commands and updates the game state accordingly.
        /// </summary>
        public IScreenBehaviorResult<GameState> HandleInput(InputCommand input, GameState state)
        {
            return input.Type switch
            {
                SystemInputCommandType.LEFT => ProcessGameplayLeftClick(state),
                SystemInputCommandType.RIGHT => ProcessGameplayRightClick(state),
                SystemInputCommandType.SPACE => ProcessGameplaySpaceClick(state),
                SystemInputCommandType.ESCAPE => ProcessGameplayEscapeClick(state),
                _ => new GameplayBehaviorResult(state)
            };
        }

        public IScreenBehaviorResult<GameState> Update(GameState state)
        {
            state = UpdateInvaderMoveIntervals(state);

            var moveInvadersResult = MoveInvaders(state);
           
            if(moveInvadersResult.NavigateTo is not null)
            {
                return new GameplayBehaviorResult(moveInvadersResult.State);
            } 
            else
            {
                state = moveInvadersResult.State;
            }

                state = UpdateBullets(state);
            return new GameplayBehaviorResult(state);
        }


        #region HandlerHelpers
        private static GameplayBehaviorResult ProcessGameplayEscapeClick(GameState state) => 
            new GameplayBehaviorResult( 
                state with { IsPaused = !state.IsPaused },
                Common.ScreenType.PauseMenu
            );

        private static GameplayBehaviorResult ProcessGameplaySpaceClick(GameState state) => new GameplayBehaviorResult(
                        state with
                        {
                            Bullets = state.Bullets.Add(
                                new Bullet(state.Player.X, 10))
                        });

        private static GameplayBehaviorResult ProcessGameplayLeftClick(GameState state) => new GameplayBehaviorResult(
                        state with {
                            Player = state.Player with
                            {
                                X = Math.Max(0, state.Player.X - 1)
                            }
                        });
       
        private static GameplayBehaviorResult ProcessGameplayRightClick(GameState state) => new GameplayBehaviorResult(
                        state with {
                            Player = state.Player with
                            {
                                X = Math.Min(20, state.Player.X + 1)
                            }
                        });
        #endregion

        #region UpdateHelpers
        private static GameState UpdateInvaderMoveIntervals(GameState state) => 
            state with
            {
                InvaderSideMoveFrameInterval = Math.Max(0, state.InvaderSideMoveFrameInterval - 1),
                InvaderDownMoveFrameInterval = Math.Max(0, state.InvaderDownMoveFrameInterval - 1),
            };

        private static GameplayBehaviorResult MoveInvaders(GameState state)
        {
            if (state.InvaderSideMoveFrameInterval == 0)
            {
                state = state with
                {
                    Invaders = state.Invaders
                        .Select(invader =>
                        {
                            var newX = invader.X + 1;
                            if (newX >= Configuration.ScreenWidth)
                                newX = 0;

                            return invader with { X = newX };
                        })
                        .ToImmutableList(),

                    InvaderSideMoveFrameInterval = Configuration.InvaderSideMoveFrameInterval,
                };
            }

            if (state.InvaderDownMoveFrameInterval == 0)
            {
                var invaders = state.Invaders.ToBuilder();

                for (int i = 0; i < invaders.Count; i++)
                {
                    var invader = invaders[i];

                    var newY = invader.Y + 1;

                    if (newY > Configuration.PlayerStartPosition)
                    {
                        return new GameplayBehaviorResult(state, Common.ScreenType.GameOver);
                    }

                    invaders[i] = invader with { Y = newY };
                }

                state = state with
                {
                    Invaders = invaders.ToImmutable(),
                    InvaderDownMoveFrameInterval = Configuration.InvaderDownMoveFrameInterval
                };
            }
            return new GameplayBehaviorResult(state);
        }

        private static GameState UpdateBullets(GameState state)
        {
            var bullets = state.Bullets.ToBuilder();

            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i].Y < 0) state.Bullets.RemoveAt(i);
                else bullets[i] = bullets[i] with { Y = bullets[i].Y - 1 };
            }

            state = state with { Bullets = bullets.ToImmutable() };

            return state;
        }
        #endregion
    }
}
