using InnoClinic.Domain.Models;
using InnoClinic.Domain.Options;
using InnoClinic.Services.Abstractions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Services.Implementation
{
    public class PasswordHashingService : IPasswordHashingService
    {
        private readonly int saltSize = 32;

        public PasswordModel EncodePassword(string password)
        {
            var passwordModel = new PasswordModel();

            using (var deriveBytes = new Rfc2898DeriveBytes(password, saltSize))
            {
                passwordModel.Salt = deriveBytes.Salt;
                passwordModel.Key = deriveBytes.GetBytes(saltSize);
            }

            return passwordModel;
        }

        public bool IsValidPassword(string password, PasswordModel hashedPassword)
        {
            using var deriveBytes = new Rfc2898DeriveBytes(password, hashedPassword.Salt);
            byte[] newKey = deriveBytes.GetBytes(saltSize);

            return newKey.SequenceEqual(hashedPassword.Key);
        }
    }
}
