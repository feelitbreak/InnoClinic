using InnoClinic.Domain.Entities;

namespace InnoClinic.Services.Abstractions
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
