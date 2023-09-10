using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Domain.Entities
{
    public class Patient : BaseProfile
    {
        public DateTime DateOfBirth { get; set; }

        public bool IsLinkedToAccount { get; set; }
    }
}
