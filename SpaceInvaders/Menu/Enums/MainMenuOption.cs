using System.ComponentModel.DataAnnotations;

namespace SpaceInvaders.Menu.Enums
{
    public enum MainMenuOption
    {
        [Display(Name = "Start Game")]
        StartGame = 1,

        [Display(Name = "Credits")]
        Credits = 4,

        [Display(Name = "Exit")]
        Exit = 5
    }
}
