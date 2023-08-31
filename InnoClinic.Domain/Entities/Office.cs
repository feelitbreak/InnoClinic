using System.Text.Json.Serialization;

namespace InnoClinic.Domain.Entities
{
    public class Office : BaseEntity
    {
        public byte[]? Photo { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string HouseNumber { get; set; }

        public string OfficeNumber { get; set; }

        public long RegistryPhoneNumber { get; set; }

        [JsonIgnore]
        public List<User> Users { get; } = new();
    }
}
