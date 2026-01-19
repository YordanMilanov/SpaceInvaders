using System.Threading.Channels;

namespace SpaceInvaders.System;

static class SystemRenderer
{
    public static async Task RunAsync(ChannelReader<FrameSnapshot> reader)
    {
        await foreach (var frame in reader.ReadAllAsync())
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0); //Used to prevent flickering. Frame is overwritten in the same console space.
            Console.WriteLine(frame.Frame);
        }
    }
}