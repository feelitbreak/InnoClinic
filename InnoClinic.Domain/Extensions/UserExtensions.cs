using InnoClinic.Domain.Options;
using InnoClinic.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Domain.Extensions
{
    public static class UserExtensions
    {
        public static bool HasPassword(this User user, string passwordToCheck)
        {
            return user.Password.Equals(passwordToCheck);
        }
    }
}
