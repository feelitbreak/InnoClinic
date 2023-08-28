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
        PasswordModel Encode(string password);

        bool IsValid(string password, PasswordModel hashedPassword);
    }
}
