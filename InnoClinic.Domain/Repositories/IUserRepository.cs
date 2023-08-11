using InnoClinic.Domain.Entities;

namespace InnoClinic.Domain.Repositories
{
    public interface IUserRepository
    {
        public List<User> GetAllUsers();

        public User? GetUserByEmail(string email);

        public void AddUser(User user);

        public bool CheckUser(string email);
    }
}
