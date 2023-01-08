

using Core.Entities;
using Core.Interfaces.IRepositories;
using Infrastructure.DataAccess;

namespace Infrastructure.Repositories
{
    public class BrandOfProductRepository : Repository<BrandOfProduct>, IBrandOfProductRepository
    {
        public BrandOfProductRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
