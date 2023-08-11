using InnoClinic.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Services.Abstractions
{
    public interface ITokenService
    {
        public string GenerateToken(IConfiguration _config, User user);
    }
}
