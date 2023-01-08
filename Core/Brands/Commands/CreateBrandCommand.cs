using Core.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Brands.Commands
{
    public class CreateBrandCommand:IRequest<BrandOfProductDto>
    {
        public CreateBrandCommand(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
