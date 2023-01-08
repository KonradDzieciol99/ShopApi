

using Core.Entities;
using Core.Interfaces.IRepositories;
using Infrastructure.DataAccess;

namespace Infrastructure.Repositories
{
    public class BasketItemRepository : Repository<BasketItem>, IBasketItemRepository
    {
        public BasketItemRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
