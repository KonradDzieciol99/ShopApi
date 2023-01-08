using AutoMapper;
using Core.Dtos;
using Core.Interfaces.IRepositories;
using Core.Orders.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Orders.Handlers
{
    public class GetUserOrdersQueryHandler : IRequestHandler<GetUserOrdersQuery, IEnumerable<OrderToReturnDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserOrdersQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<OrderToReturnDto>> Handle(GetUserOrdersQuery request, CancellationToken cancellationToken)
        {
            var email = request.Email;

            var orders = await _unitOfWork.OrderRepository.FindAndIncludeX2Async(x => x.BuyerEmail == email, x => x.OrderItems, x => x.DeliveryMethod);

            return _mapper.Map<IEnumerable<OrderToReturnDto>>(orders);
        }
    }
}
