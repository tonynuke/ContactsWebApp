using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Shared
{
    public class UnitOfWork : IDisposable
    {
        private readonly DbContext context;

        public Repository<T> GetRepository<T>() where T : class
        {
            var dbSet = this.context.Set<T>();
            return new Repository<T>(dbSet);
        }

        public Task Save()
        {
            return this.context.SaveChangesAsync();
        }

        public UnitOfWork(DbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Dispose()
        {
            context?.Dispose();
        }
    }
}