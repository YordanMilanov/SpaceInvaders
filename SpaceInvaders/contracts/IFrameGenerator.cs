namespace SpaceInvaders.contracts
{
    public interface IFrameGenerator<TState> 
        where TState : IScreenState
    {
        string GenerateFrame(TState state);
    }
}
