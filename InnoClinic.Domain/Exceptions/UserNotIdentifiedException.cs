using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Domain.Exceptions
{
    public sealed class UserNotIdentifiedException : BadRequestException
    {
        public UserNotIdentifiedException() : base("Couldn't identify current user.")
        {

        }
    }
}
