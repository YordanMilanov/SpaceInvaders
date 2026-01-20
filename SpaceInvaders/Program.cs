using System.Threading.Channels;
using SpaceInvaders.Common;
using SpaceInvaders.System;

var inputChannel = Channel.CreateUnbounded<InputCommand>();

var renderChannel = Channel.CreateBounded<FrameSnapshot>(
    new BoundedChannelOptions(2)
    {
        FullMode = BoundedChannelFullMode.DropOldest // If the channel is full, drop the oldest frame (framerate drop)
    });

using var cts = new CancellationTokenSource();

SystemInput.ThreadStart(inputChannel.Writer, cts.Token);

var screenManager = ScreenManagerFactory.Create();

var systemLoop = new SystemLoop(
    inputChannel.Reader,
    renderChannel.Writer,
    screenManager,
    cts.Token);

var systemLoopTask = Task.Run(() => systemLoop.RunAsync());
var rendererTask = Task.Run(() => SystemRenderer.RunAsync(renderChannel.Reader));

try
{
    await systemLoopTask;
}
catch (OperationCanceledException)
{
    // graceful exit
}

renderChannel.Writer.Complete();
await rendererTask;

Console.Clear();
Console.WriteLine("Game exited cleanly.");
