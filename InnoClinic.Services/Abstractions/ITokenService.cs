using InnoClinic.Domain.Entities;
using InnoClinic.Domain.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Services.Abstractions
{
    public interface ITokenService
    {
        public string GenerateToken(JwtOptions _jwtOptions, User user, string role);
    }
}
