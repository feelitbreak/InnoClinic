using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Domain.Enums
{
    public enum ExceptionCode
    {
        UserNotFoundException,
        UserNotIdentifiedException,
        InvalidPasswordException,
        OfficeNotFoundException,
        UserIsLinkedToOfficeException,
        PatientNotFoundException,
        UserIsLinkedToProfileException,
    }
}
