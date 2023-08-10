using System.ComponentModel.DataAnnotations;

namespace InnoClinic.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [MinLength(6)]
        public string Password { get; set; } = string.Empty;

        [MinLength(6)]
        public string ReenteredPassword { get; set; } = string.Empty;
    }
}