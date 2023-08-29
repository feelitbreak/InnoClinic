using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Domain.Exceptions
{
    public sealed class InvalidPasswordException : BadRequestException
    {
        public InvalidPasswordException() : base("The password you've entered is incorrect.")
        {

        }
    }
}
