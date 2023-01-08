
using Core.Entities;
using Core.Interfaces.IRepositories;
using Infrastructure.DataAccess;

namespace Infrastructure.Repositories
{
    public class CategoryOfProductRepository : Repository<CategoryOfProduct>, ICategoryOfProductRepository
    {
        public CategoryOfProductRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
