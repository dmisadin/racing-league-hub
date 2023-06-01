using F1StatsServer.Data;
using F1StatsServer.Interface;
using F1StatsServer.Model;

namespace F1StatsServer.Repository
{
    public class DriverRepository : IGenericRepository<Driver>
    {
        private readonly AdventureContext _context;

        public DriverRepository(AdventureContext context)
        {
            _context = context;
        }

        //public List<Driver> CreateItem(Driver item)
        //{
        //    throw new NotImplementedException();
        //}

        //public List<Driver> DeleteItem(int id)
        //{
        //    throw new NotImplementedException();
        //}

        public List<Driver> Get()
        {
            return _context.Drivers.OrderBy(o => o.PkDriverId).ToList();
        }

        public Driver GetById(int id)
        {
            return _context.Drivers.Where(c => c.PkDriverId == id).FirstOrDefault();
        }

        //public bool Has(int id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
