using SpaceInvaders.Config;
using SpaceInvaders.System;
using System.Threading.Channels;

namespace SpaceInvaders.Common;

class SystemLoop
{

    private readonly ChannelReader<InputCommand> _inputChannel;
    private readonly ChannelWriter<FrameSnapshot> _renderChannel;
    private readonly ScreenManager _screenManager;
    private readonly CancellationToken _token;

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
    }

    public async Task RunAsync()
    {
        var frameTime = TimeSpan.FromMilliseconds(Configuration.RefreshRate); //FPS
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
    }
}
