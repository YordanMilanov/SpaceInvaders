using System.ComponentModel.DataAnnotations;

namespace SpaceInvaders.Menu.PauseMenu
{
    public enum  PauseMenuOption
    {
        [Display(Name = "Resume")]
        Resume = 1,

        [Display(Name = "Restart")]
        Restart = 2,

        [Display(Name = "Exit")]
        Exit = 3,
    }
}
