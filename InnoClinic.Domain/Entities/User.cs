using System.ComponentModel.DataAnnotations;

namespace InnoClinic.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [MinLength(6)]
        public string Password { get; set; }
    }
}