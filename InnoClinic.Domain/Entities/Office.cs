using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InnoClinic.Domain.Enums;

namespace InnoClinic.Domain.Entities
{
    public class Office
    {
        public int Id { get; set; }

        public byte[]? Photo { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        [Column("House number")]
        public string HouseNumber { get; set; }

        [Column("Office number")]
        public string OfficeNumber { get; set; }

        [Column("Registry phone number")]
        public long RegistryPhoneNumber { get; set; }

        public Status Status { get; set; }
    }
}
