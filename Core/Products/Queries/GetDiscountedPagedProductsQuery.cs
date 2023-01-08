using Core.Dtos;
using Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Products.Queries
{
    public class GetDiscountedPagedProductsQuery : IRequest<PagedList<ProductDto>>
    {
        public GetDiscountedPagedProductsQuery(ProductParamsRequest productParamsRequest)
        {
            ProductParamsRequest = productParamsRequest;
        }

        public ProductParamsRequest ProductParamsRequest { get; }
    }
}
