using SpaceInvaders.Common;

namespace SpaceInvaders.contracts
{
    public interface IScreenFactory
    {
        IScreen Create(ScreenType state);
    }
}
