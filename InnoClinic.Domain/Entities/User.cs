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

        public bool IsEmailVerified { get; set; }

        public byte[]? Photo { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public long PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public bool? IsLinkedToAccount { get; set; }

        public int? OfficeId { get; set; }

        [JsonIgnore]
        public Office? Office { get; set; }
    }
}
