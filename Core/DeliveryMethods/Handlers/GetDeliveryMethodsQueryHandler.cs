using AutoMapper;
using Core.Dtos;
using Core.Interfaces.IRepositories;
using Core.Interfaces;
using Core.Products.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DeliveryMethods.Queries;
using Core.Entities;

namespace Core.DeliveryMethods.Handlers
{
    public class GetDeliveryMethodsQueryHandler : IRequestHandler<GetDeliveryMethodsQuery, IEnumerable<DeliveryMethod>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetDeliveryMethodsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<DeliveryMethod>> Handle(GetDeliveryMethodsQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.DeliveryMethodRepository.GetAllAsync();
        }
    }
}
