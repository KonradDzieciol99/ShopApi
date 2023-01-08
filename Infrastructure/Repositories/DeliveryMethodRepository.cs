

using Core.Entities;
using Core.Interfaces.IRepositories;
using Infrastructure.DataAccess;

namespace Infrastructure.Repositories
{
    public class DeliveryMethodRepository : Repository<DeliveryMethod>, IDeliveryMethodRepository
    {
        public DeliveryMethodRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
