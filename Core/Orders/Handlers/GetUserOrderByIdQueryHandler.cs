using AutoMapper;
using Core.Dtos;
using Core.Exceptions;
using Core.Interfaces.IRepositories;
using Core.Orders.Commands;
using Core.Orders.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Orders.Handlers
{
    public class GetUserOrderByIdQueryHandler : IRequestHandler<GetUserOrderByIdQuery, OrderToReturnDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetUserOrderByIdQueryHandler(IMapper mapper,IUnitOfWork unitOfWork)
        {
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
        }

        public async Task<OrderToReturnDto> Handle(GetUserOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.OrderRepository.FindOneAndIncludeX2Async(x => x.BuyerEmail == request.Email && x.Id == request.Id, x => x.OrderItems, x => x.DeliveryMethod);
            if (order == null) { throw new BadRequestException("Zamówienie nie istnieje"); };
            return _mapper.Map<OrderToReturnDto>(order);
        }
    }
}
