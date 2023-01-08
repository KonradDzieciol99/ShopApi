using AutoMapper;
using Core.Dtos;
using Core.Extensions;
using Core.Interfaces.IRepositories;
using Core.Models;
using Core.Products.Queries;
using Core.Specifications;
using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Products.Handlers
{
    internal class GetPagedProductsQueryHandler : IRequestHandler<GetPagedProductsQuery, PagedList<ProductDto>>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPagedProductsQueryHandler(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.httpContextAccessor = httpContextAccessor;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<PagedList<ProductDto>> Handle(GetPagedProductsQuery request, CancellationToken cancellationToken)
        {
            List<int>? brandsIds = null;
            List<int>? categoriesIds = null;
            var productParams = request.ProductParamsRequest;

            if (productParams.Brands != null)
            {
                brandsIds = JsonConvert.DeserializeObject<List<int>>(productParams.Brands);
                if (brandsIds==null || brandsIds.Count() == 0) { brandsIds = null; }
            }
            if (productParams.Categories != null)
            {
                categoriesIds = JsonConvert.DeserializeObject<List<int>>(productParams.Categories);
                if (categoriesIds == null||categoriesIds.Count() == 0) { categoriesIds = null; }
            }

            var productSpecification = new ProductSpecification(productParams.PageNumber, productParams.PageSize, productParams.MinPrice, productParams.MaxPrice, brandsIds, categoriesIds, productParams.OrderByfield, productParams.OrderAscending, productParams.Search);

            //var productSpecification = new ProductSpecification() {
            //    PageNumber= productParams.PageNumber,
            //    PageSize=productParams.PageSize,
            //    MinPrice=productParams.MinPrice,
            //    MaxPrice=productParams.MaxPrice,
            //    brandsIds=brandsIds,
            //    categoriesIds=categoriesIds,
            //    OrderByfield=productParams.OrderByfield,
            //    OrderAscending=productParams.OrderAscending
            //};

            var PagedProductsList = await _unitOfWork.ProductRepository.GetAllProductWithPhotosAsync(productSpecification);
            //PagedList<ProductDto> = new PagedList<ProductDto> 
            var PagedProductsListDto = new PagedList<ProductDto>()
            {
                TotalCount = PagedProductsList.TotalCount,
                PageSize = PagedProductsList.PageSize,
                TotalPages = PagedProductsList.TotalPages,
                CurrentPage = PagedProductsList.CurrentPage,
                List = _mapper.Map<List<ProductDto>>(PagedProductsList.List)
            };
            
            //products.li_mapper.Map<IEnumerable<ProductDto>>(products);
            //httpContextAccessor.HttpContext.Response.AddPaginationHeaderResponse(PagedProductsList.CurrentPage,
            //PagedProductsList.PageSize, PagedProductsList.TotalCount, PagedProductsList.TotalPages);
            return PagedProductsListDto;

        }
    }
}
