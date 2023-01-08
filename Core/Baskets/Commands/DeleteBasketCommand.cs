using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Baskets.Commands
{
    public class DeleteBasketCommand:IRequest<Unit>
    {
        public DeleteBasketCommand(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; }
    }
}
