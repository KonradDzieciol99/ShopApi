

using Core.Entities;
using Core.Interfaces.IRepositories;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public OrderRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public async Task<Order?> GetOrderByIntendIdWithDeliveryMethodAndOrderItemsAndPhotos(string intendId)
        {
            return await applicationDbContext.Orders.Where(x => x.PaymentIntentId == intendId).Include(x => x.OrderItems).ThenInclude(x=>x.Product.Photos).Include(x => x.DeliveryMethod).FirstOrDefaultAsync();
        }
    }
}
