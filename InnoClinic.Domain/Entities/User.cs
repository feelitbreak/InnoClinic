using InnoClinic.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InnoClinic.Domain.Entities
{
    public class User: BaseEntity
    {
        [Column("E-mail")]
        public string Email { get; set; }

        public string Password { get; set; }

        public Role Role { get; set; }

        [JsonIgnore]
        public List<Office> OfficeList { get; } = new();
    }
}