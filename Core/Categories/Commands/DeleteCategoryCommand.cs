using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Categories.Commands
{
    public class DeleteCategoryCommand : IRequest<Unit>
    {
        public DeleteCategoryCommand(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
