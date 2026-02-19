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
            state = UpdateInvaderMoveIntervalCounters(state);

            state = MoveBullets(state);
            state = MoveInvaders(state);
            state = UpdateInvaders(state);

            if (state.IsGameOver)
            {
                return new GameplayBehaviorResult(state, Common.ScreenType.GameOver);
            } 

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
        private static GameState UpdateInvaderMoveIntervalCounters(GameState state) => 
            state with
            {
                InvaderSideMoveFrameIntervalCounter = Math.Max(0, state.InvaderSideMoveFrameIntervalCounter - 1),
                InvaderDownMoveFrameIntervalCounter = Math.Max(0, state.InvaderDownMoveFrameIntervalCounter - 1),
            };

        private static GameState MoveInvaders(GameState state)
        {
            if (state.InvaderSideMoveFrameIntervalCounter == 0)
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

                    InvaderSideMoveFrameIntervalCounter = state.CurrentSideMoveFrameInterval,
                };
            }

            if (state.InvaderDownMoveFrameIntervalCounter == 0)
            {
                var invaders = state.Invaders.ToBuilder();

                for (int i = 0; i < invaders.Count; i++)
                {
                    var invader = invaders[i];

                    var newY = invader.Y + 1;

                    if (newY > Configuration.PlayerStartPosition - 1)
                    {
                        return state with { IsGameOver = true };
                    }

                    invaders[i] = invader with { Y = newY };
                }

                state = state with
                {
                    Invaders = invaders.ToImmutable(),
                    InvaderDownMoveFrameIntervalCounter = state.CurrentDownMoveFrameInterval
                };
            }
            return state;
        }

        private static GameState MoveBullets(GameState state)
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

        private static GameState UpdateInvaders(GameState state)
        {
            var remainingBullets = state.Bullets.ToBuilder();
            var remainingInvaders = ImmutableList.CreateBuilder<Invader>();
            var score = state.Score;

            foreach (var invader in state.Invaders)
            {
                var hitBulletIndex = remainingBullets
                    .FindIndex(b => b.X == invader.X && b.Y == invader.Y);

                if (hitBulletIndex != -1)
                {
                    score++;
                    remainingBullets.RemoveAt(hitBulletIndex);
                }
                else
                {
                    remainingInvaders.Add(invader);
                }
            }

            if(remainingInvaders.Count is 0)
            {
                return state with
                {
                    Score = score,
                    Invaders = GameplayHelper.InitInvaders(),
                    CurrentDownMoveFrameInterval = state.CurrentDownMoveFrameInterval / 2,
                    InvaderDownMoveFrameIntervalCounter = state.CurrentDownMoveFrameInterval / 2,
                };
            }

            return state with
            {
                Score = score,
                Bullets = remainingBullets.ToImmutable(),
                Invaders = remainingInvaders.ToImmutable()
            };
        }
        #endregion
    }
}
