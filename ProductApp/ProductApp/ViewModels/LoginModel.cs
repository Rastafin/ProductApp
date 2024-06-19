using System.ComponentModel.DataAnnotations;

namespace ProductApp.ViewModels
{
    public class LoginModel
    {
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Username { get; set; } = null!;
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Password { get; set; } = null!;
    }
}
