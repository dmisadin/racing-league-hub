using F1StatsServer.Data;
using F1StatsServer.Dto;
using F1StatsServer.Interface;
using F1StatsServer.Model;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace F1StatsServer.Repository
{
    public class UserRepository : GenericRepository<User>, IGenericRepository<User>, IUserRepository
    {
        private readonly AdventureContext _context;

        public UserRepository(AdventureContext context) : base(context)
        {
            _context = context;
        }

        public bool CheckCredentials(string email, string password)
        {
            return _context.Users.Any(o => o.Email == email && o.Password == password);
        }

        public bool RegisterUser(RegisterDto data)
        {
            bool result = CreateItem(data);

            if (!result)
                return false;
            return true;
        }

        public bool CreateItem(RegisterDto data)
        {
            User user = new()
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

        public User CheckRole(string email, string password)
        {
            return _context.Users.Where((c) => c.Email == email && c.Password == password).FirstOrDefault();
        }
    }
}
