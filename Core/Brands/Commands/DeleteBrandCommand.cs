using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Brands.Commands
{
    public class DeleteBrandCommand : IRequest<Unit>
    {
        public DeleteBrandCommand(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
