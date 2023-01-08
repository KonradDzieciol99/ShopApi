using Core.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Products.Commands
{
    public class CreateProductCommand :IRequest<ProductDto>
    {
        public CreateProductCommand(CreateProductDto createProductDto)
        {
            CreateProductDto = createProductDto;
        }

        public CreateProductDto CreateProductDto { get; }
    }
}
