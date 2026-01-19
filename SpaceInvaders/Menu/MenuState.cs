using SpaceInvaders.Common;

namespace SpaceInvaders.Menu
{
    public class MenuState
    {
        public SelectedOption CurrentOption { get; set; } = new(1);
        public SystemState state = SystemState.MainMenu;
    }

    public record SelectedOption(int position);
}
