using InnoClinic.Domain.Entities;

namespace InnoClinic.Domain.Repositories
{
    internal interface IUserRepository
    {
        public List<User> GetUserDetails();

        public User GetUserDetails(int id);

        public void AddUser(User user);

        public bool CheckUser(int id);
    }
}
