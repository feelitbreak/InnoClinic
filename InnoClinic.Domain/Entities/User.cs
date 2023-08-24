using InnoClinic.Domain.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InnoClinic.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Column("E-mail")]
        public string Email { get; set; }

        public string Password { get; set; }

        public Role Role { get; set; }
    }
}