using UnitTestProject.Interfaces;
using UnitTestProject.Models;

namespace UnitTestProject.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UnitTestContext _context;
        public UserRepository(UnitTestContext context)
        {
            _context = context;
        }
        public User CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser(short id)
        {
            throw new NotImplementedException();
        }

        public User GetUserByID(short id)
        {
            return _context.Users.FirstOrDefault(u=>u.Id==id);
        }

        public ICollection<User> GetUserList()
        {
            return _context.Users.ToList();
        }

        public bool UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
