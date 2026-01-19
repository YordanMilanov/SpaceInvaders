using SpaceInvaders.System;
using System.Threading.Channels;

namespace SpaceInvaders.Common
{
    static class SystemInput
    {
        /// <summary>
        /// Starts the input system thread that listens for key presses and sends commands to the provided channel writer.
        /// </summary>
        public static Thread ThreadStart(ChannelWriter<InputCommand> writer, CancellationToken token)
        {
            var thread = new Thread(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    var key = Console.ReadKey(true).Key; // Read key without displaying(if false - displays it) it

                    InputCommand? command = key switch
                    {
                        ConsoleKey.UpArrow => new InputCommand(SystemInputCommandType.UP),
                        ConsoleKey.DownArrow => new InputCommand(SystemInputCommandType.DOWN),
                        ConsoleKey.LeftArrow => new InputCommand(SystemInputCommandType.LEFT),
                        ConsoleKey.RightArrow => new InputCommand(SystemInputCommandType.RIGHT),
                        ConsoleKey.Spacebar => new InputCommand(SystemInputCommandType.SPACE),
                        ConsoleKey.Escape => new InputCommand(SystemInputCommandType.ESCAPE),
                        _ => null
                    };

                    if (command != null) writer.TryWrite(command);
                }
            });

            thread.IsBackground = true;
            thread.Start();
            return thread;
        }
    }
}
