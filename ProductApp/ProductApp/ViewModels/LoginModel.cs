using System.ComponentModel.DataAnnotations;

namespace ProductApp.ViewModels
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
