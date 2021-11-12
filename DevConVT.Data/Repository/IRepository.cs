using System.Linq;

namespace DevConVT.Data.Repository
{
    public interface IRepository
    {
        void Add<T>(T entity);
               
        IQueryable<T> Set<T>() where T : class;

    }
}
