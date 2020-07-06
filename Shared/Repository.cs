using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Shared
{
    public class Repository<T> where T : class
    {
        private readonly DbSet<T> items;

        public ValueTask<EntityEntry<T>> AddAsync(T item)
        {
            return this.items.AddAsync(item);
        }

        public void Remove(T item)
        {
            this.items.Remove(item);
        }

        public IQueryable<T> Find(Specification<T> specification)
        {
            return this.items.Where(specification.IsSatisfiedBy).AsQueryable();
        }

        public IQueryable<T> GetAll()
        {
            return this.items.AsQueryable();
        }

        public Repository(DbSet<T> items)
        {
            this.items = items ?? throw new ArgumentNullException(nameof(items));
        }
    }
}