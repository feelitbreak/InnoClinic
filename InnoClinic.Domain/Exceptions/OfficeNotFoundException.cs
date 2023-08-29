using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InnoClinic.Domain.Entities;

namespace InnoClinic.Domain.Exceptions
{
    public sealed class OfficeNotFoundException : NotFoundException
    {
        public OfficeNotFoundException(int officeId) : base($"The office with the identifier {officeId} wasn't found.")
        {

        }
    }
}
