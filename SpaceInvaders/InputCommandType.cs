namespace SpaceInvaders
{
    enum InputCommandType
    {
        LEFT,
        RIGHT,
        SHOOT,
        PAUSE
    }

    record InputCommand(InputCommandType Type);

    record FrameSnapshot(string Frame);
}
