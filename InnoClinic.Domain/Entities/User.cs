using InnoClinic.Domain.Enums;
using System.Text.Json.Serialization;

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

        public Patient? Patient { get; set; }

        public Receptionist? Receptionist { get; set; }

        public Doctor? Doctor { get; set; }
    }
}
