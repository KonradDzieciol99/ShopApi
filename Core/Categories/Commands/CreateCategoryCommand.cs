using Core.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Categories.Commands
{
    public class CreateCategoryCommand :IRequest<CategoryOfProductDto>
    {
        public CreateCategoryCommand(string name)
        {
            Name = name;
        }
        public string Name { get; }
    }
}
