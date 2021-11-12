using System;
using System.Data.Entity;
using System.Linq;

namespace DevConVT.Data.Repository
{
    public class Kiwi : IRepository /* where T : class, IRepository*/
    {
        protected readonly DevConVTDbContext _context;

       // private DbSet<T> table  = null;
        public Kiwi(DevConVTDbContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity)
        {
            throw new NotImplementedException();
        }

        //public void Add<T>(T entity)
        //{
        //    _context.Add(entity);
        //    _context.SaveChanges();
        //}

        //public void Add(T entity)
        //{
        //    table.Add(entity);
        //    _context.SaveChanges();
        //}

        public IQueryable<T> Set<T>() where T : class
        {
            return _context.Set<T>();
        }
    }
}