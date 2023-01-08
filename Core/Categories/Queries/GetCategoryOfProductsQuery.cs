using Core.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Categories.Queries
{
    public class GetCategoryOfProductsQuery : IRequest<IEnumerable<CategoryOfProductDto>>
    {
    }
}
