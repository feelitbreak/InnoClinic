using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Domain.Exceptions
{
    public sealed class UserEmailNotFoundException : NotFoundException
    {
        public UserEmailNotFoundException(string email) : base($"The user with the email {email} wasn't found.")
        {

        }
    }
}
