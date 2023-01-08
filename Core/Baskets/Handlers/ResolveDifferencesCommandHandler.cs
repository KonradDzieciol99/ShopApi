using AutoMapper;
using Core.Interfaces.IRepositories;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Core.Baskets.Commands;
using Core.Dtos;
using Core.Entities;
using Core.Exceptions;
using Stripe;

namespace Core.Baskets.Handlers
{
    internal class ResolveDifferencesCommandHandler:IRequestHandler<ResolveDifferencesCommand,CustomerBasketDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidateBasketService _validateBasketService;

        public ResolveDifferencesCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IValidateBasketService validateBasketService)
        {
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
            this._validateBasketService = validateBasketService;
        }

        public async Task<CustomerBasketDto> Handle(ResolveDifferencesCommand request, CancellationToken cancellationToken)
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
                basketFromDb.BasketItems = await CompareItemsAndSum(basketFromDb.BasketItems, ValidatedBasket.BasketItems);
                basketFromDb.DeliveryMethodId = customerBasket.DeliveryMethodId;
            }

            if (_unitOfWork.HasChanges())
            {
                if (await _unitOfWork.Complete()) { return _mapper.Map<CustomerBasketDto>(basketFromDb); }
                throw new BadRequestException("hmmmmm");
            }
            else
            {
                return _mapper.Map<CustomerBasketDto>(basketFromDb);
            }
        }
        private async Task<List<BasketItem>> CompareItemsAndSum(ICollection<BasketItem> itemsFromDb, ICollection<BasketItem> items)
        {
            var ConcatList = itemsFromDb.Concat(items).ToList();

            var duplicatedProductId = ConcatList
                .GroupBy(x => x.ProductId)
                .Where(x => x.Count() > 1)
                .Select(x => x.Key)
                .ToList();
            foreach (var item in duplicatedProductId)
            {
                var fromClient = items.Where(x => x.ProductId == item).FirstOrDefault();
                ConcatList.RemoveAll(x => x.ProductId == item);
                ConcatList.Add(fromClient);
            }

            return ConcatList;
        }
    }
}
