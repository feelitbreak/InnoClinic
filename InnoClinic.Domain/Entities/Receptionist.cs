using System.Text.Json.Serialization;

namespace InnoClinic.Domain.Entities
{
    public class Receptionist: BaseProfile
    {
        public int? OfficeId { get; set; }

        [JsonIgnore]
        public Office? Office { get; set; }
    }
}
