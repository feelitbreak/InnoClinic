using InnoClinic.Domain.Enums;

namespace InnoClinic.Domain.DTOs
{
    public class UserRoleDto
    {
        public string UserEmail { get; set; }

        public Role Role { get; set; }
    }
}
