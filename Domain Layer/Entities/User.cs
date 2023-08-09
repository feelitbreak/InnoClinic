using System.ComponentModel.DataAnnotations;

namespace Domain_Layer.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Please, enter the email")]
        [EmailAddress(ErrorMessage = "You've entered an invalid email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please, enter the password")]
        [MaxLength(15), MinLength(6)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Re-entered Password")]
        [Required(ErrorMessage = "Please, reenter the password")]
        [MaxLength(15), MinLength(6)]
        public string ReenteredPassword { get; set; } = string.Empty;
    }
}