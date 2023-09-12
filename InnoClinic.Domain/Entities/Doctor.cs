using System.Text.Json.Serialization;

namespace InnoClinic.Domain.Entities
{
    public class Doctor : BaseProfile
    {
        public DateTime DateOfBirth { get; set; }

        public DateTime CareerStartYear { get; set; }

        public int? OfficeId { get; set; }

        [JsonIgnore]
        public Office? Office { get; set; }
    }
}
