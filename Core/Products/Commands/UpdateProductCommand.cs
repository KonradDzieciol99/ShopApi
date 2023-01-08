using Core.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Products.Commands
{
    public class UpdateProductCommand:IRequest<Unit>
    {
        public UpdateProductCommand(ProductDto produktDto)
        {
            ProduktDto = produktDto;
        }

        public ProductDto ProduktDto { get; }
    }
}
