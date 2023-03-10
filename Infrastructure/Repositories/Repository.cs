
using Core.Interfaces.IRepositories;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _dbContext;

        public Repository(ApplicationDbContext applicationDbContext)
        {
            this._dbContext = applicationDbContext;
        }

        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }
        public void AddRange(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().AddRange(entities);
        }
        public async Task<int> FindAndCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().Where(predicate).CountAsync();
        }
        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().Where(predicate).ToListAsync();
        }
        public async Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().Where(predicate).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<TResoult>> FindAndGroupByAsync<TKey, TResoult>(Expression<Func<TEntity, bool>> predicate,
                                Expression<Func<TEntity, TKey>> keySelector, Expression<Func<IGrouping<TKey, TEntity>, TResoult>> selector)
        {
            return await _dbContext.Set<TEntity>().Where(predicate).GroupBy(keySelector).Select(selector).ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> FindAndOrderByAsync<TKey, TResoult>(Expression<Func<TEntity, bool>> predicate,
                        Expression<Func<TEntity, TKey>> keySelector)
        {
            return await _dbContext.Set<TEntity>().Where(predicate).OrderBy(keySelector).ToListAsync();
        }
        public async Task<TEntity?> GetOneAsync(int entityId)
        {
            return await _dbContext.Set<TEntity>().FindAsync(entityId);
            //return await _dbContext.Set<TEntity>().FirstAsync;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }
        public void Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().RemoveRange(entities);
        }

        public async Task<TEntity?> FindOneAndIncludeAsync<TProperty>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProperty>> navigationPropertyPath)
        {
            return await _dbContext.Set<TEntity>().Where(predicate).Include(navigationPropertyPath).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<TEntity>> FindAndIncludeAsync<TProperty>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProperty>> navigationPropertyPath)
        {
            return await _dbContext.Set<TEntity>().Where(predicate).Include(navigationPropertyPath).ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> FindAndIncludeX2Async<TProperty, TPropertyX2>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProperty>> navigationPropertyPath, Expression<Func<TEntity, TPropertyX2>> navigationPropertyPathX2)
        {
            return await _dbContext.Set<TEntity>().Where(predicate).Include(navigationPropertyPath).Include(navigationPropertyPathX2).ToListAsync();
        }
        public async Task<TEntity?> FindOneAndIncludeX2Async<TProperty, TPropertyX2>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProperty>> navigationPropertyPath, Expression<Func<TEntity, TPropertyX2>> navigationPropertyPathX2)
        {
            return await _dbContext.Set<TEntity>().Where(predicate).Include(navigationPropertyPath).Include(navigationPropertyPathX2).FirstOrDefaultAsync();
        }
    }
}
