using AutoMapper;
using Core.Dtos;
using Core.Exceptions;
using Core.Interfaces.IRepositories;
using Core.Payments.Commands;
using Core.Products.Commands;
using MediatR;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Payments.Handlers
{
    public class CreateOrUpdatePaymentIntentCommandHandler : IRequestHandler<CreateOrUpdatePaymentIntentCommand, CustomerBasketDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;


        public CreateOrUpdatePaymentIntentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            _config = config;
        }

        public async Task<CustomerBasketDto> Handle(CreateOrUpdatePaymentIntentCommand request, CancellationToken cancellationToken)
        {
            StripeConfiguration.ApiKey = _config["StripeSettings:SecretKey"];

            var basket = await _unitOfWork.CustomerBasketRepository.FindOneAndIncludeAsync(x => x.Id == request.Id, x => x.BasketItems);

            if (basket == null) { throw new Exception("PaymentServiceException"); };

            var shippingPrice = 0m;

            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.DeliveryMethodRepository.GetOneAsync((int)basket.DeliveryMethodId);
                if (deliveryMethod == null) { throw new BadRequestException("ta opcja dostawy jest niedostępna"); }
                shippingPrice = deliveryMethod.Price;
            }

            foreach (var basketItem in basket.BasketItems)
            {
                var productItem = await _unitOfWork.ProductRepository.FindOneAsync(x=>x.Id==basketItem.ProductId && x.Quantity >= 1);
                if (productItem==null) { throw new BadRequestException("Produkt jest niedostępny"); }
                var actualPrice = productItem.cutPrice ?? productItem.Price;
                if (basketItem.Price != actualPrice)
                {
                    basketItem.Price = actualPrice;
                }
            }

            var service = new PaymentIntentService();//stripe nuget

            PaymentIntent intent;//stripe nuget

            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions //stripe nuget
                {
                    Amount = (long)basket.BasketItems.Sum(i => i.Quantity * (i.Price * 100)) + ((long)shippingPrice * 100),
                    Currency = "PLN",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                intent = await service.CreateAsync(options);
                basket.PaymentIntentId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)basket.BasketItems.Sum(i => (i.Quantity * (i.Price * 100))) + (long)(shippingPrice * 100)
                };
                await service.UpdateAsync(basket.PaymentIntentId, options);
            }

            _unitOfWork.CustomerBasketRepository.Update(basket);

            if (_unitOfWork.HasChanges())
            {
                if (await _unitOfWork.Complete()) { return _mapper.Map<CustomerBasketDto>(basket); }
                throw new BadRequestException("nie udało się zaktualizować PaymentIntent ");
            }

            return _mapper.Map<CustomerBasketDto>(basket);
        }
    }
}
