
using Core.Interfaces.IRepositories;
using Infrastructure.DataAccess;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }

        public IProductRepository ProductRepository => new ProductRepository(_applicationDbContext);
        public ICustomerBasketRepository CustomerBasketRepository => new CustomerBasketRepository(_applicationDbContext);
        public IDeliveryMethodRepository DeliveryMethodRepository =>new DeliveryMethodRepository(_applicationDbContext);
        public IOrderRepository OrderRepository => new OrderRepository(_applicationDbContext);
        public IBasketItemRepository BasketItemRepository => new BasketItemRepository(_applicationDbContext);
        public IBrandOfProductRepository BrandOfProductRepository => new BrandOfProductRepository(_applicationDbContext);
        public ICategoryOfProductRepository CategoryOfProductRepository => new CategoryOfProductRepository(_applicationDbContext);

        public async Task<bool> Complete()
        {
            return (await _applicationDbContext.SaveChangesAsync() > 0);
        }
        public bool HasChanges()
        {
            _applicationDbContext.ChangeTracker.DetectChanges();
            var changes = _applicationDbContext.ChangeTracker.HasChanges();

            return changes;
        }
        public void Dispose()
        {
            _applicationDbContext.Dispose();
        }

    }
}
