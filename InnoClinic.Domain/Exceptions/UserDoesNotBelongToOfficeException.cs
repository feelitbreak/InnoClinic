using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Domain.Exceptions
{
    public sealed class UserDoesNotBelongToOfficeException : BadRequestException
    {
        public UserDoesNotBelongToOfficeException(int userId, int officeId) : base(
            $"The user with the identifier {userId} does not belong to the office with the identifier {officeId}.")
        {

        }
    }
}
