using Core.Baskets.Queries;
using Core.Dtos;
using Core.Interfaces.IRepositories;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.Exceptions;

namespace Core.Baskets.Handlers
{
    public class GetBasketByIdQueryHandler:IRequestHandler<GetBasketByIdQuery,CustomerBasketDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidateBasketService _validateBasketService;
        private readonly IMapper _mapper;

        public GetBasketByIdQueryHandler(IUnitOfWork unitOfWork, IValidateBasketService validateBasketService,IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._validateBasketService = validateBasketService;
            this._mapper = mapper;
        }

        public async Task<CustomerBasketDto> Handle(GetBasketByIdQuery request, CancellationToken cancellationToken)
        {
            var userId = request.UserId;
            var customerBasket = await _unitOfWork.CustomerBasketRepository.FindOneAndIncludeAsync(x => x.AppUserId == userId, z => z.BasketItems);
            if (customerBasket == null) { return new CustomerBasketDto(); }
            var ValidatedBasket = await _validateBasketService.ValidateBasketAsync(customerBasket);

            if (_unitOfWork.HasChanges())
            {
                if (await _unitOfWork.Complete()) { return _mapper.Map<CustomerBasketDto>(ValidatedBasket); }
                throw new BadRequestException("failed to fetch basket");
            }
            else
            {
               return _mapper.Map<CustomerBasketDto>(ValidatedBasket);
            }
        }
    }
}
