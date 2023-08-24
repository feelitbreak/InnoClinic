using InnoClinic.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace InnoClinic.Domain.Entities
{
    public class User: BaseEntity
    {
        [Column("E-mail")]
        public string Email { get; set; }

        public string Password { get; set; }

        public Role Role { get; set; }
    }
}