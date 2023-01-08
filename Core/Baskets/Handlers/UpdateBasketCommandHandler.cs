using AutoMapper;
using Core.Baskets.Commands;
using Core.Dtos;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Core.Interfaces.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Baskets.Handlers
{
    public class UpdateBasketCommandHandler : IRequestHandler<UpdateBasketCommand, CustomerBasketDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidateBasketService _validateBasketService;

        public UpdateBasketCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IValidateBasketService validateBasketService)
        {
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
            this._validateBasketService = validateBasketService;
        }
        public async Task<CustomerBasketDto> Handle(UpdateBasketCommand request, CancellationToken cancellationToken)
        {
            var userId = request.UserId;

            var customerBasket = _mapper.Map<CustomerBasket>(request.CustomerBasketDto);
            customerBasket.AppUserId = userId;

            var ValidatedBasket = await _validateBasketService.ValidateBasketAsync(customerBasket);

            var basketFromDb = await _unitOfWork.CustomerBasketRepository.FindOneAndIncludeAsync(x => x.AppUserId == userId, z => z.BasketItems);
            if (basketFromDb == null)
            {
                _unitOfWork.CustomerBasketRepository.Add(ValidatedBasket);
                basketFromDb = ValidatedBasket;
            }
            else
            {
                basketFromDb.BasketItems = ValidatedBasket.BasketItems;
                basketFromDb.DeliveryMethodId = customerBasket.DeliveryMethodId;
            }

            if (_unitOfWork.HasChanges())
            {
                if (await _unitOfWork.Complete()) { return _mapper.Map<CustomerBasketDto>(basketFromDb); }
                throw new BadRequestException("Maksymalna ilość danego produktu");
            }
            else
            {
                return _mapper.Map<CustomerBasketDto>(basketFromDb);
            }
        }
    }
}
