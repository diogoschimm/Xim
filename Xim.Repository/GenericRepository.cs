using Xim;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Xim.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> DbSet;

        public GenericRepository(DbContext context)
        {
            this._context = context;
            this.DbSet = this._context.Set<T>();
        }

        public IEnumerable<T> GetAll() => this.DbSet;

        public T Get(params object[] keys) => this.DbSet.Find(keys);

        public T Save(T entity)
        {
            this.DbSet.Add(entity);
            this._context.SaveChanges();

            return entity;
        }

        public T Update(T entity)
        {
            this.DbSet.Update(entity);
            this._context.SaveChanges();

            return entity;
        }

        public bool Delete(T entity)
        {
            this.DbSet.Remove(entity);
            return this._context.SaveChanges() > 0;
        }
    }
}
