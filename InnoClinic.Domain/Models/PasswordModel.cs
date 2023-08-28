using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Domain.Models
{
    public class PasswordModel
    {
        public byte[] Key { get; set; }

        public byte[] Salt { get; set; }
    }
}
