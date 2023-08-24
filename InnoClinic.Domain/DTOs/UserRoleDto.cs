using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InnoClinic.Domain.Enums;

namespace InnoClinic.Domain.DTOs
{
    public class UserRoleDto
    {
        public string UserEmail { get; set; }

        public Role Role { get; set; }
    }
}
