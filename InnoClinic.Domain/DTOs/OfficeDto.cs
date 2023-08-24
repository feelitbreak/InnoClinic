using InnoClinic.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Domain.DTOs
{
    public class OfficeDto
    {
        public byte[]? Photo { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string HouseNumber { get; set; }

        public string OfficeNumber { get; set; }

        public long RegistryPhoneNumber { get; set; }

        public Status Status { get; set; }
    }
}
