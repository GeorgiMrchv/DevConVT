
using System.Linq;

namespace DevConVT.Data.Repository
{
    public class Repository : IRepository
    {
        protected readonly DevConVTDbContext _context;

        public Repository(DevConVTDbContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity)
        {
            //_context.Add(entity);
            _context.SaveChanges();
        }

        public IQueryable<T> Set<T>() where T : class
        {
            return _context.Set<T>();
        }
    }
}