using System.ComponentModel.DataAnnotations;

namespace SpaceInvaders.Menu.Enums
{
    public enum MenuHeader
    {

        [Display(Name = "=== Space Invaders ===")]
        MainMenu,

        [Display(Name = "=== Game Paused ===")]
        PauseMenu,

        [Display(Name = "=== Game Over ===")]
        GameOver,

        [Display(Name = "=== Settings ===")]
        Settings,
    }
}
