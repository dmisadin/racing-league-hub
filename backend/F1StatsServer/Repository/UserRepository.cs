using F1StatsServer.Data;
using F1StatsServer.Interface;
using F1StatsServer.Model;

namespace F1StatsServer.Repository
{
    public class UserRepository : IGenericRepository<User>, IUserRepository
    {
        private readonly AdventureContext _context;

        public UserRepository(AdventureContext context)
        {
            _context = context;
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

        public bool Has(string name)
        {
            return _context.Users.Any(c => c.Username == name);
        }
    }
}
