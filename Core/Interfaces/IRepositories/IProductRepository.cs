using Core.Entities;
using Core.Models;
using Core.Specifications;

namespace Core.Interfaces.IRepositories
{
    public interface IProductRepository :IRepository<Product>
    {
        Task<Product> GetProductWithPhotosAsync(int id);
        //Task<PagedList<Product>> GetAllProductWithPhotosAsync(ProductParamsRequest productParams);
        Task<PagedList<Product>> GetAllProductWithPhotosAsync(ProductSpecification productSpecification);
        void Update(Product product);
        //Task<PagedList<Product>> GetDiscountedProductWithPhotosAsync(ProductParamsRequest productParams);
        Task<PagedList<Product>> GetDiscountedProductWithPhotosAsync(ProductSpecification productSpecification);
        
    }
}
