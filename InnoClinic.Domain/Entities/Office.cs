using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InnoClinic.Domain.Entities
{
    public class Office : BaseEntity
    {
        public byte[]? Photo { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        [Column("House number")]
        public string HouseNumber { get; set; }

        [Column("Office number")]
        public string OfficeNumber { get; set; }

        [Column("Registry phone number")]
        public long RegistryPhoneNumber { get; set; }

        [JsonIgnore]
        public List<User> UserList { get; } = new();
    }
}
