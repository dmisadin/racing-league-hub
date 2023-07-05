using F1StatsServer.Data;
using F1StatsServer.Dto;
using F1StatsServer.Interface;
using F1StatsServer.Model;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace F1StatsServer.Repository
{
    public class UserRepository : IGenericRepository<User>, IUserRepository
    {
        private readonly AdventureContext _context;

        public UserRepository(AdventureContext context)
        {
            _context = context;
        }

        public bool CheckCredentials(string name,string password)
        {
            return _context.Users.Any(o => o.Username == name && o.Password == password);
        }

        public List<User> Get()
        {
            return _context.Users.OrderBy(o => o.PkUserId).ToList();

        }

        public User GetById(int id)
        {
            return _context.Users.Where(o => o.PkUserId == id).FirstOrDefault();
        }

        public bool Has(int id)
        {
            return _context.Users.Any(c => c.PkUserId == id);
        }

        public bool RegisterUser(RegisterDto data)
        {
            bool result = CreateItem(data);

            if(!result)
                return false;
            return true;
        }

        public bool CreateItem(RegisterDto data)
        {
            User user = new User
            {
                Username = data.Username,
                Password = data.Password,
                Email = data.Email,
                IsAdmin = false
            };
            if (_context.Users.Contains(user))
                return false;
            _context.Add(user);

            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
