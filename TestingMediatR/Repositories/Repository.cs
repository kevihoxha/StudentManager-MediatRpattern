
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TestingMediatR.Data;

namespace TestingMediatR.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        protected readonly DbContextClass _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(DbContextClass dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public IQueryable<TEntity> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<bool> Exists(Expression<Func<TEntity, bool>> predicate)
        {
            var found = await _dbSet.Where(predicate).AnyAsync();
            return found;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
            {
                return false;
            }

            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<TEntity> GetStudent(Expression<Func<TEntity, bool>> predicate)
        {
            var result = await _dbSet.FirstOrDefaultAsync(predicate);
            return result;
        }
    }
}
