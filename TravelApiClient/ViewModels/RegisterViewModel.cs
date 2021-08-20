
using System.ComponentModel.DataAnnotations;
namespace TravelApiClient.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Reenter Password")]
        [Compare(nameof(Password), ErrorMessage = "Passwords don't match.")]
        public string PasswordMatch { get; set; }
    }
}