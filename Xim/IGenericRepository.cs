using System.Collections.Generic;

namespace Xim
{
    public interface IGenericRepository<T> where T : class 
    {
        IEnumerable<T> GetAll();
        T Get(params object[] keys);
        T Save(T entity);
        T Update(T entity);
        bool Delete(T entity);
    }
}
