using AutoMapper;
using CloudinaryDotNet;
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
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Products.Handlers
{
    internal class GetDiscountedPagedProductsQueryHandler: IRequestHandler<GetDiscountedPagedProductsQuery, PagedList<ProductDto>>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetDiscountedPagedProductsQueryHandler(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.httpContextAccessor = httpContextAccessor;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<PagedList<ProductDto>> Handle(GetDiscountedPagedProductsQuery request, CancellationToken cancellationToken)
        {
            List<int>? brandsIds = null;
            List<int>? categoriesIds = null;
            var productParams = request.ProductParamsRequest;

            if (productParams.Brands != null)
            {
                brandsIds = JsonConvert.DeserializeObject<List<int>>(productParams.Brands);
                if (brandsIds == null || brandsIds.Count() == 0) { brandsIds = null; }
            }
            if (productParams.Categories != null)
            {
                categoriesIds = JsonConvert.DeserializeObject<List<int>>(productParams.Categories);
                if (categoriesIds == null || categoriesIds.Count() == 0) { categoriesIds = null; }
            }

            ////////////////////TO DO przekopiować to z dołu do konstrukora!
            var productSpecification = new ProductSpecification(productParams.PageNumber, productParams.PageSize, productParams.MinPrice, productParams.MaxPrice,brandsIds, categoriesIds, productParams.OrderByfield, productParams.OrderAscending, productParams.Search);
            var pagedProductsList = await _unitOfWork.ProductRepository.GetDiscountedProductWithPhotosAsync(productSpecification);
            var pagedProductsListDto = new PagedList<ProductDto>()
            {
                TotalCount = pagedProductsList.TotalCount,
                PageSize = pagedProductsList.PageSize,
                TotalPages = pagedProductsList.TotalPages,
                CurrentPage = pagedProductsList.CurrentPage,
                List = _mapper.Map<List<ProductDto>>(pagedProductsList.List)
            };
            return pagedProductsListDto;
        }
    }
}
