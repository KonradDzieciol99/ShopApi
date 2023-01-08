using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Newtonsoft.Json;
using ShopApi.Helpers;
using Core.Entities;
using Core.Interfaces.IRepositories;
using Infrastructure.DataAccess;
using Core.Models;
using Core.Extensions;
using Infrastructure.Common;
using Core.Specifications;
//using Expression = System.Linq.Expressions.Expression;

namespace Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ProductRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }
        //https://www.youtube.com/watch?v=rtXpYpZdOzM&ab_channel=ProgrammingwithMosh

        public async Task<Product> GetProductWithPhotosAsync(int id)
        {
           return await _applicationDbContext.Product.Include(p => p.Photos).FirstOrDefaultAsync(p=>p.Id==id);
        }
        public async Task<PagedList<Product>> GetAllProductWithPhotosAsync(ProductSpecification productSpecification)
        {
            var products = _applicationDbContext.Product.AsQueryable();

            Expression<Func<Product, bool>> TESTwhereExpression = (p => p.cutPrice != null ? (p.cutPrice >= productSpecification.MinPrice && p.cutPrice <= productSpecification.MaxPrice) : ( p.Price >= productSpecification.MinPrice && p.Price <= productSpecification.MaxPrice ) && p.Quantity  >= 1 );

            AddConditions(ref TESTwhereExpression, productSpecification.brandsIds, productSpecification.categoriesIds, productSpecification.Search);

            var count = await products.Where(TESTwhereExpression).CountAsync();

            var list = await products.Include(p => p.Photos)
                                 .Include(p => p.BrandOfProduct)
                                 .Include(p => p.CategoryOfProduct)
                                 .Where(TESTwhereExpression)
                                 .OrderByPropertyOrField(productSpecification.OrderByfield, productSpecification.OrderAscending)
                                 .Skip((productSpecification.PageNumber - 1) * productSpecification.PageSize)
                                 .Take(productSpecification.PageSize)
                                 .ToListAsync();
            PagedList<Product> PagedList = new PagedList<Product>();
            PagedList.List.AddRange(list);
            PagedList.CurrentPage = productSpecification.PageNumber;
            PagedList.TotalPages = (int)Math.Ceiling(count / (double)productSpecification.PageSize);
            PagedList.PageSize = productSpecification.PageSize;
            PagedList.TotalCount = count;

            return PagedList;
        }
        public async Task<PagedList<Product>> GetDiscountedProductWithPhotosAsync(ProductSpecification productSpecification)
        {
            var products = _applicationDbContext.Product.AsQueryable();
            Expression<Func<Product, bool>> TESTwhereExpression = (p => p.cutPrice != null && p.cutPrice >= productSpecification.MinPrice && p.cutPrice <= productSpecification.MaxPrice && p.Quantity >= 1);

            AddConditions(ref TESTwhereExpression, productSpecification.brandsIds, productSpecification.categoriesIds, productSpecification.Search);

            var count = await products.Where(TESTwhereExpression).CountAsync();

            var list = await products.Include(p => p.Photos)
                                 .Include(p => p.BrandOfProduct)
                                 .Include(p => p.CategoryOfProduct)
                                 .Where(TESTwhereExpression)//ToLower()
                                 .OrderByPropertyOrField(productSpecification.OrderByfield, productSpecification.OrderAscending)
                                 .Skip((productSpecification.PageNumber - 1) * productSpecification.PageSize)
                                 .Take(productSpecification.PageSize)
                                 .ToListAsync();

            PagedList<Product> PagedList = new PagedList<Product>();
            PagedList.List.AddRange(list);
            PagedList.CurrentPage = productSpecification.PageNumber;
            PagedList.TotalPages = (int)Math.Ceiling(count / (double)productSpecification.PageSize);
            PagedList.PageSize = productSpecification.PageSize;
            PagedList.TotalCount = count;

            return PagedList;
        }

        private void AddConditions(ref Expression<Func<Product, bool>> tESTwhereExpression, List<int>? brandsIds, List<int>? categoriesIds, string? search)
        {
            if (brandsIds != null)
            {
                Expression<Func<Product, bool>> brandsExpression = x => brandsIds.Contains(x.BrandOfProductId);
                tESTwhereExpression = tESTwhereExpression.And(brandsExpression);
            }
            if (categoriesIds != null)
            {
                Expression<Func<Product, bool>> categoriesExpression = x => categoriesIds.Contains(x.CategoryOfProductId);
                tESTwhereExpression = tESTwhereExpression.And(categoriesExpression);
            }
            if (!string.IsNullOrEmpty(search))
            {
                Expression<Func<Product, bool>> productNameExpression = x => x.ProductName.Contains(search);
                tESTwhereExpression = tESTwhereExpression.And(productNameExpression);
            }
        }

        public void Update(Product product)
        {
            _applicationDbContext.Entry(product).State = EntityState.Modified;
        }

    }
}
