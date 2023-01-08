using CloudinaryDotNet.Actions;
using Core.Entities;
using Core.Interfaces.IRepositories;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    public class CustomerBasketRepository : Repository<CustomerBasket>, ICustomerBasketRepository
    {
        public ApplicationDbContext _DbContext { get; }

        public CustomerBasketRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _DbContext = applicationDbContext;
        }


        public void Update(CustomerBasket entity)
        {
            _DbContext.Update(entity);
            //_DbContext.Entry(entity).CurrentValues.SetValues(entity);
        }
        public async Task GetUserBasketWithItemsAndPhotos(CustomerBasket entity)
        {
            _DbContext.CustomerBaskets.Where(x => x.AppUserId == entity.AppUserId).Include(z => z.BasketItems).ThenInclude(x=>x.PictureUrl);
        }

        //public async Task<TEntity?> FindOneAndIncludeAsync<TProperty>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProperty>> navigationPropertyPath)
        //{
        //    return await _dbContext.Set<TEntity>().Where(predicate).Include(navigationPropertyPath).FirstOrDefaultAsync();
        //}
    }
}
