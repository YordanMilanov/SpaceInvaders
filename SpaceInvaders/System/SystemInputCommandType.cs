namespace SpaceInvaders.System
{
    public enum SystemInputCommandType
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        SPACE,
        ESCAPE,
        ENTER
    }

    public record InputCommand(SystemInputCommandType Type);

    public record FrameSnapshot(string Frame);
}
