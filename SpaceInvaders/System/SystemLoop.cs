using SpaceInvaders.Game;
using SpaceInvaders.Menu;
using SpaceInvaders.Menu.Common;
using SpaceInvaders.System;
using System.Threading.Channels;

namespace SpaceInvaders.Common;

class SystemLoop
{

    private readonly ChannelReader<InputCommand> _inputChannel;
    private readonly ChannelWriter<FrameSnapshot> _renderChannel;
    private readonly ScreenManager _screenManager;
    private readonly CancellationToken _token;

    private GameState _gameState = new();
    private MenuState _menuState = new();

    private ScreenState _screenState = new();

    public SystemLoop(
        ChannelReader<InputCommand> input,
        ChannelWriter<FrameSnapshot> render,
        ScreenManager screenManager,
        CancellationToken token)
    {
        _inputChannel = input;
        _renderChannel = render;
        _screenManager = screenManager;
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
            // Handle input
            while (_inputChannel.TryRead(out var input))
            {
                _screenManager.HandleInput(input);
            }

            // Update current screen: For Dynamic animations
            _screenManager.Update();

            // Render current screen
            var frame = _screenManager.Render();
            await _renderChannel.WriteAsync(new FrameSnapshot(frame), _token);

            // Wait for next frame
            await Task.Delay(frameTime, _token);
        }

        //while (!_token.IsCancellationRequested)
        //{
        //    // 1️. Process All pending input (if any)
        //    while (_inputChannel.TryRead(out var input))
        //    {
        //        switch (_screenState)
        //        {
        //            case ScreenState.Gameplay:
        //                _gameState = GameplayInputProcessor.ProcessGameplayInput(input, _gameState);
        //                break;

        //            case ScreenState.MainMenu:
        //                _menuState = MenuInputProcessor.ProcessMainMenuInput(input, _menuState);
        //                _screenState = _menuState.ScreenState;
        //                break;

        //            case ScreenState.PauseMenu:
        //                _menuState = MenuInputProcessor.ProcessPauseMenuInput(input, _menuState);
        //                _screenState = _menuState.ScreenState;
        //                break;

        //            case ScreenState.GameOverMenu:
        //                _menuState = MenuInputProcessor.ProcessGameOverMenuInput(input, _menuState);
        //                _screenState = _menuState.ScreenState;
        //                break;
        //        }
        //    }

        //    // 2️. Update frame (even with NO input)
        //    string frame = _screenState switch
        //    {
        //        ScreenState.Gameplay => GenerateGameplayFrame(),
        //        ScreenState.MainMenu => MenuFrameGenerator.GenerateFrame(_menuState),
        //        ScreenState.PauseMenu => MenuFrameGenerator.GenerateFrame(_menuState),
        //        ScreenState.GameOverMenu => MenuFrameGenerator.GenerateFrame(_menuState),
        //        ScreenState.SettingsMenu => MenuFrameGenerator.GenerateFrame(_menuState),
        //        _ => string.Empty
        //    };

        //    // 3️. Render every frame
        //    await _renderChannel.WriteAsync(new FrameSnapshot(frame), _token);

        //    // 4️. Fixed timestep
        //    await Task.Delay(frameTime, _token);
        //}
    }

    //private string GenerateGameplayFrame()
    //{
    //    if(!_gameState.IsPaused)
    //    {
    //        _gameState = GameplayStateUpdater.UpdateBullets(_gameState);
    //        return GameplayFrameGenerator.GenerateFrame(_gameState);
    //    } 
    //    else
    //    {
    //        _menuState = new MenuState() { 
    //            ScreenState = ScreenState.PauseMenu
    //        };
    //        _screenState = ScreenState.PauseMenu;
    //        return MenuFrameGenerator.GenerateFrame(_menuState);
    //    }
    //}
}
