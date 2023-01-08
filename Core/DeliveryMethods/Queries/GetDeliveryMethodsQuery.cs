using Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DeliveryMethods.Queries
{
    public class GetDeliveryMethodsQuery : IRequest<IEnumerable<DeliveryMethod>>
    {
    }
}
