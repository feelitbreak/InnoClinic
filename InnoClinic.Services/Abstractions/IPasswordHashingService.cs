using InnoClinic.Domain.Models;

namespace InnoClinic.Services.Abstractions
{
    public interface IPasswordHashingService
    {
        PasswordModel EncodePassword(string password);

        bool IsValidPassword(string password, PasswordModel hashedPassword);
    }
}
