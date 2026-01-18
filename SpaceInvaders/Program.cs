using System.Threading.Channels;
using SpaceInvaders;

var inputChannel = Channel.CreateUnbounded<InputCommand>();
var renderChannel = Channel.CreateBounded<FrameSnapshot>(2);

using var cts = new CancellationTokenSource();

InputSystem.ThreadStart(inputChannel.Writer, cts.Token);

var gameLoop = new GameLoop(
    inputChannel.Reader,
    renderChannel.Writer,
    cts.Token);

var gameLoopTask = Task.Run(() => gameLoop.RunAsync());
var rendererTask = Task.Run(() => Renderer.RunAsync(renderChannel.Reader));

try
{
    await gameLoopTask;
}
catch (OperationCanceledException)
{
    // graceful exit
}

renderChannel.Writer.Complete();
await rendererTask;

Console.Clear();
Console.WriteLine("Game exited cleanly.");
