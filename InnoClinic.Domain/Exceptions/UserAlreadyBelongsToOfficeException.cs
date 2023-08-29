using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Domain.Exceptions
{
    public sealed class UserAlreadyBelongsToOfficeException : BadRequestException
    {
        public UserAlreadyBelongsToOfficeException(int userId, int officeId) : base(
            $"The user with the identifier {userId} already belongs to the office with the identifier {officeId}.")
        {

        }
    }
}
