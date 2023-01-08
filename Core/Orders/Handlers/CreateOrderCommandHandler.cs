using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Core.Interfaces.IRepositories;
using Core.Models;
using Core.Orders.Commands;
using Core.Orders.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Core.Orders.Handlers
{
    public class CreateOrderCommandHandler :IRequestHandler<CreateOrderCommand, OrderToReturnDto>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IValidateBasketService _validateBasketService;

        public CreateOrderCommandHandler(IMapper mapper, IUnitOfWork unitOfWork,IMediator mediator,IValidateBasketService validateBasketService)
        {
            this._mapper = mapper;
            _unitOfWork = unitOfWork;
            this._mediator = mediator;
            this._validateBasketService = validateBasketService;
        }

        public IUnitOfWork _unitOfWork { get; }

        public async Task<OrderToReturnDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderDto = request.OrderDto;
            var address = _mapper.Map<AddressDto, OrderAddress>(request.OrderDto.ShipToAddress);
            var basket = await _unitOfWork.CustomerBasketRepository.FindOneAndIncludeAsync(x => x.Id == orderDto.BasketId, x => x.BasketItems);
            
            if (basket==null){throw new BadRequestException("The basket does not exist");}

            basket=await _validateBasketService.CheckBasketAsync(basket);

            var items = new List<OrderItem>();
            foreach (var item in basket.BasketItems)
            {

                var productItem = await _unitOfWork.ProductRepository.FindOneAndIncludeAsync(x=>x.Id==item.ProductId,x=>x.Photos);
                if (productItem == null) { throw new BadRequestException("The product does not exist"); }
                var photo = productItem.Photos.Where(x => x.IsMain == true).FirstOrDefault();
                var orderPrice = productItem.cutPrice ?? productItem.Price;
                var orderPhoto = photo?.Url ?? "";
                var orderItem = new OrderItem(productItem.ProductName, orderPhoto, orderPrice, item.Quantity, productItem.Id);
                items.Add(orderItem);
                productItem.Quantity = productItem.Quantity - item.Quantity;
                _unitOfWork.ProductRepository.Update(productItem);
            }

            var deliveryMethod = await _unitOfWork.DeliveryMethodRepository.GetOneAsync(orderDto.DeliveryMethodId);
            if (deliveryMethod == null) { throw new BadRequestException("This shipping option does not exist"); }

            var subtotal = items.Sum(item => item.Price * item.Quantity);

            var existingOrder = await _unitOfWork.OrderRepository.FindOneAsync(x => x.PaymentIntentId == basket.PaymentIntentId);

            var order = new Order(request.Email, address, deliveryMethod, subtotal, basket.PaymentIntentId, items);

            _unitOfWork.OrderRepository.Add(order);

            if (existingOrder != null)
            {
                _unitOfWork.OrderRepository.Remove(existingOrder);
            }

            if (_unitOfWork.HasChanges())
            {
                if (!await _unitOfWork.Complete()) {throw new BadRequestException("Order creation failed");}
            }
            if (order == null) { throw new BadRequestException("Order creation failed"); };

            await _mediator.Publish(new OrderCreatedEvent(order));

            return _mapper.Map<Order, OrderToReturnDto>(order);       
        }
    }
}
