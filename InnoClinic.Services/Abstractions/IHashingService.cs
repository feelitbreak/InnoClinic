using InnoClinic.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Services.Abstractions
{
    public interface IHashingService
    {
        PasswordModel EncodePassword(string password);

        bool IsValidPassword(string password, PasswordModel hashedPassword);
    }
}
