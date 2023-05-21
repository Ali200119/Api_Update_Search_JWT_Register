using System.Linq.Expressions;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> entities;

        public Repository(AppDbContext context)
        {
            _context = context;
            entities = _context.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            if(entity == null) {  throw new ArgumentNullException(nameof(entity)); }
            await entities.AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity == null) { throw new ArgumentNullException(nameof(entity)); }

            entities.Remove(entity);

            await SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync(int? id)
        {
            if(id == null) throw new ArgumentNullException(); 

            T entity = await entities.FindAsync(id);

            if(entity == null) throw new NullReferenceException("Notfound data");

            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            entities.Update(entity);
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync() => _context.SaveChangesAsync();

        public async Task SoftDeleteAsync(T entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            entity.SoftDelete = true;

            await SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> expression = null) => expression == null ? await entities.ToListAsync() : await entities.Where(expression).ToListAsync();
    }
}