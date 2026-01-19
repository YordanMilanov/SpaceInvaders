using SpaceInvaders.Game;
using SpaceInvaders.Menu;
using SpaceInvaders.System;
using System.Text;
using System.Threading.Channels;

namespace SpaceInvaders.Common;

class SystemLoop
{

    private readonly ChannelReader<InputCommand> _input;
    private readonly ChannelWriter<FrameSnapshot> _render;
    private readonly CancellationToken _token;

    private GameState _gameState = new();
    private MenuState _menuState = new();

    private readonly SystemState _systemState = new();

    public SystemLoop(
        ChannelReader<InputCommand> input,
        ChannelWriter<FrameSnapshot> render,
        CancellationToken token)
    {
        _input = input;
        _render = render;
        _token = token;

        _gameState.Invaders.AddRange(new[]
        {
            new Invader(3, 2),
            new Invader(7, 2),
            new Invader(11, 2)
        });
    }

    public async Task RunAsync()
    {
        var frameTime = TimeSpan.FromMilliseconds(20); // 50 FPS

        while (!_token.IsCancellationRequested)
        {
            // 1️. Process All pending input (if any)
            while (_input.TryRead(out var input))
            {
                switch (_systemState)
                {
                    case SystemState.Gameplay:
                        _gameState = GameInputProcessor.ProcessGameplayInput(input, _gameState);
                        break;

                    case SystemState.MainMenu:
                        _menuState = MenuInputProcessor.ProcessMainMenuInput(input, _menuState);
                        break;

                    case SystemState.PauseMenu:
                        _menuState = MenuInputProcessor.ProcessPauseMenuInput(input, _menuState);
                        break;

                    case SystemState.GameOverMenu:
                        _menuState = MenuInputProcessor.ProcessGameOverMenuInput(input, _menuState);
                        break;
                }
            }

            // 2️. Update simulation (even with NO input)
            string frame = _systemState switch
            {
                SystemState.Gameplay => GenerateGameplayFrame(),
                SystemState.MainMenu => MenuFrameGenerator.GenerateFrame(_menuState),
                SystemState.PauseMenu => MenuFrameGenerator.GenerateFrame(_menuState),
                SystemState.GameOverMenu => MenuFrameGenerator.GenerateFrame(_menuState),
                _ => string.Empty
            };

            // 3️. Render every frame
            await _render.WriteAsync(new FrameSnapshot(frame), _token);

            // 4️. Fixed timestep
            await Task.Delay(frameTime, _token);
        }
    }

    private string GenerateGameplayFrame()
    {
        _gameState = GameStateUpdater.UpdateBullets(_gameState);
        return GameFrameGenerator.GenerateFrame(_gameState);
    }
}
