using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Domain.Options
{
    public class AesCipherKeyOptions
    {
        public string Key { get; init; }

        public string Vector { get; init; }
    }
}
