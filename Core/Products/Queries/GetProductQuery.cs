using Core.Dtos;
using Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Core.Products.Queries
{
    public class GetProductQuery : IRequest<ProductDto>
    {
        public GetProductQuery(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
