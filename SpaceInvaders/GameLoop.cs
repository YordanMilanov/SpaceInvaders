using System.Text;
using System.Threading.Channels;

namespace SpaceInvaders;

class GameLoop
{
    private readonly ChannelReader<InputCommand> _input;
    private readonly ChannelWriter<FrameSnapshot> _render;
    private readonly CancellationToken _token;

    private readonly GameState _state = new();

    public GameLoop(
        ChannelReader<InputCommand> input,
        ChannelWriter<FrameSnapshot> render,
        CancellationToken token)
    {
        _input = input;
        _render = render;
        _token = token;

        _state.Invaders.AddRange(new[]
        {
            new Invader(3, 2),
            new Invader(7, 2),
            new Invader(11, 2)
        });
    }

    public async Task RunAsync()
    {
        var frameTime = TimeSpan.FromMilliseconds(20); //50 FPS

        while (!_token.IsCancellationRequested)
        {
            ProcessInput();
            UpdateBullets();

            var frame = RenderFrame(); //Generate Frame

            await _render.WriteAsync(new FrameSnapshot(frame), _token);

            await Task.Delay(frameTime, _token);
        }
    }

    /// <summary>
    /// Processes all pending input commands and updates the game state accordingly.
    /// </summary>
    /// <remarks>This method handles player movement, shooting, and pause commands by reading from the input </remarks>
    /// <exception cref="OperationCanceledException">Thrown if a pause command is received during input processing.</exception>
    private void ProcessInput()
    {
        while (_input.TryRead(out var input))
        {
            switch (input.Type)
            {
                case InputCommandType.LEFT:
                    _state.Player = _state.Player with { X = Math.Max(0, _state.Player.X - 1) }; // Ensure player doesn't move out of bounds то the left, left limit 0
                    break;
                case InputCommandType.RIGHT:
                    _state.Player = _state.Player with { X = Math.Min(20, _state.Player.X + 1) }; // Ensure player doesn't move out of bounds to the right, not more than 20
                    break;
                case InputCommandType.SHOOT:
                    _state.Bullets.Add(new Bullet(_state.Player.X, 10)); // Bullet starts just above the player at Y=10
                    break;
                case InputCommandType.PAUSE: //TODO: Implement  Menu/Pause functionality
                    throw new OperationCanceledException();
            }
        }
    }

    /// <summary>
    /// Update Each bullet's position and remove bullets that have moved off-screen.
    /// </summary>
    private void UpdateBullets()
    {
        for (int i = _state.Bullets.Count - 1; i >= 0; i--)
        {
            var b = _state.Bullets[i];
            b = b with { Y = b.Y - 1 };

            if (b.Y < 0)
                _state.Bullets.RemoveAt(i);
            else
                _state.Bullets[i] = b;
        }
    }

    /// <summary>
    /// Renders the current game state as a string representing the game frame.
    /// </summary>
    /// <returns>A string containing the visual representation of the current game frame, with each line corresponding to a row in the game area.(coordination system)</returns>
    private string RenderFrame()
    {
        var screenFrame = new char[12, 22];

        //Initialize screen frame with empty spaces
        for (int y = 0; y < 12; y++)
            for (int x = 0; x < 22; x++)
                screenFrame[y, x] = ' ';

        screenFrame[10, _state.Player.X] = 'A'; // Add player plane

        // Add each bullet to the screen frame
        foreach (var b in _state.Bullets)
            if (b.Y >= 0 && b.Y < 12)
                screenFrame[b.Y, b.X] = '|';

        // Add each invader to the screen frame
        foreach (var i in _state.Invaders)
            screenFrame[i.Y, i.X] = 'M';


        var rows = new string[12]; // one slot per row

        //Convert the 2D char array to a single string using multiple threads
        Parallel.For(0, 12, y =>
        {
            var sbRow = new StringBuilder(22); // each row has its own StringBuilder
            for (int x = 0; x < 22; x++)
            {
                sbRow.Append(screenFrame[y, x]); // append each char in this row
            }
            rows[y] = sbRow.ToString(); // save completed row
        });

        // Combine all rows into final string
        var result = string.Join(Environment.NewLine, rows);

        return result;
    }
}
