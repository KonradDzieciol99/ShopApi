using Core.Dtos;
using Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Brands.Queries
{
    public class GetBrandOfProductsQuery : IRequest<IEnumerable<BrandOfProductDto>>
    {
    }
}
