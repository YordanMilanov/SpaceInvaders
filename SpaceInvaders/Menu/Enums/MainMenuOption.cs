using System.ComponentModel.DataAnnotations;

namespace SpaceInvaders.Menu.Enums
{
    public enum MainMenuOption
    {
        [Display(Name = "Start Game")]
        StartGame = 1,

        [Display(Name = "Game Records")]
        GameRecords = 2,

        [Display(Name = "Settings")]
        Settings = 3,

        [Display(Name = "Credits")]
        Credits = 4,

        [Display(Name = "Exit")]
        Exit = 5
    }
}
