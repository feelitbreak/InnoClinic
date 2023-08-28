using InnoClinic.Domain.Models;
using InnoClinic.Services.Abstractions;
using System.Security.Cryptography;

namespace InnoClinic.Services.Implementation
{
    public class PasswordHashingService : IPasswordHashingService
    {
        private const int SaltSize = 32;

        public PasswordModel EncodePassword(string password)
        {
            var passwordModel = new PasswordModel();

            using var deriveBytes = new Rfc2898DeriveBytes(password, SaltSize);
            passwordModel.Salt = deriveBytes.Salt;
            passwordModel.Key = deriveBytes.GetBytes(SaltSize);

            return passwordModel;
        }

        public bool IsValidPassword(string password, PasswordModel hashedPassword)
        {
            using var deriveBytes = new Rfc2898DeriveBytes(password, hashedPassword.Salt);
            var newKey = deriveBytes.GetBytes(SaltSize);

            return newKey.SequenceEqual(hashedPassword.Key);
        }
    }
}
