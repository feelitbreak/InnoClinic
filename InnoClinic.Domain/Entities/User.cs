using InnoClinic.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InnoClinic.Domain.Entities
{
    public class User: BaseEntity
    {
        [Column("E-mail")]
        public string Email { get; set; }

        [Column("Hashed password")]
        public byte[] HashedPassword { get; set; }

        public byte[] Salt { get; set; }

        public Role Role { get; set; }

        public int? OfficeId { get; set; }

        [JsonIgnore]
        public Office? Office { get; set; }
    }
}
