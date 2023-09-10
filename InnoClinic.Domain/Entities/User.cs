using System.Text.Json.Serialization;
using InnoClinic.Domain.Enums;

namespace InnoClinic.Domain.Entities
{
    public class User: BaseEntity
    {
        public string Email { get; set; }

        public byte[] HashedPassword { get; set; }

        public byte[] Salt { get; set; }

        public Role Role { get; set; }

        public byte[]? Photo { get; set; }

        public bool IsEmailVerified { get; set; }

        [JsonIgnore]
        public Patient? Patient { get; set; }

        [JsonIgnore]
        public Receptionist? Receptionist { get; set; }

        [JsonIgnore]
        public Doctor? Doctor { get; set; }
    }
}
