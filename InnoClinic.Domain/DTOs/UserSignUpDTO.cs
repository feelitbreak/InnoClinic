using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Domain.DTOs
{
    public class UserSignUpDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6), MaxLength(15)]
        public string Password { get; set; }

        [Required]
        [MinLength(6), MaxLength(15)]
        public string ReenteredPassword { get; set; }
    }
}
