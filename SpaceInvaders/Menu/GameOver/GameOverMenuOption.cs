using System.ComponentModel.DataAnnotations;

namespace SpaceInvaders.Menu.GameOver
{
    public enum GameOverMenuOption
    {
        [Display(Name = "Restart")]
        Restart = 1,

        [Display(Name = "Exit")]
        Exit = 2,
    }
}
