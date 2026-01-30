using System.ComponentModel.DataAnnotations;

namespace SpaceInvaders.Menu.MainMenu
{
    public enum MainMenuOption
    {
        [Display(Name = "Start Game")]
        StartGame = 1,

        [Display(Name = "Credits")]
        Credits = 2,

        [Display(Name = "Exit")]
        Exit = 3
    }
}
