using InnoClinic.Domain.Models;

namespace InnoClinic.Services.Abstractions
{
    public interface IEncryptionService
    {
        PasswordModel EncodePassword(string password);

        bool IsValidPassword(string password, PasswordModel hashedPassword);
    }
}
