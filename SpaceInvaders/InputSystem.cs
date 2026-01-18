using System.Threading.Channels;

namespace SpaceInvaders
{
    static class InputSystem
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
                        ConsoleKey.LeftArrow => new InputCommand(InputCommandType.LEFT),
                        ConsoleKey.RightArrow => new InputCommand(InputCommandType.RIGHT),
                        ConsoleKey.Spacebar => new InputCommand(InputCommandType.SHOOT),
                        ConsoleKey.Escape => new InputCommand(InputCommandType.PAUSE),
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
