using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
