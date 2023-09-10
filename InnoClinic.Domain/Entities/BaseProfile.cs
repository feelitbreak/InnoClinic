
using System.Text.Json.Serialization;

namespace InnoClinic.Domain.Entities
{
    public  abstract class BaseProfile: BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public long PhoneNumber { get; set; }

        public int? UserId { get; set; }

        [JsonIgnore]
        public User? User { get; set; }
    }
}
