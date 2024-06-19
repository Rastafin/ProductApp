using System.ComponentModel.DataAnnotations;

namespace ProductApp.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Username must between 5 and 50 characters long")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Password must between 5 and 50 characters long")]
        public string Password { get; set; } = null!;
    }
}
