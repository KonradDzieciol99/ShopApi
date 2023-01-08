using Core.Dtos;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Core.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ValidateBasketService : IValidateBasketService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ValidateBasketService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomerBasketDto> ValidateBasketDtoAsync(CustomerBasketDto basketDto)
        {
            var validatedBasketItemsDto = new List<BasketItemDto>();
            foreach (var basketItemDto in basketDto.BasketItems)
            {
                var product = await _unitOfWork.ProductRepository.GetOneAsync(basketItemDto.ProductId);
                if (product == null || product.Quantity == 0) { continue; }

                if (basketItemDto.Quantity > product.Quantity)
                {
                    basketItemDto.Quantity = product.Quantity;
                }
                basketItemDto.Price = product.cutPrice ?? product.Price;

                validatedBasketItemsDto.Add(basketItemDto);
            }
            basketDto.BasketItems = validatedBasketItemsDto;
            return basketDto;
        }
        public async Task<CustomerBasket> ValidateBasketAsync(CustomerBasket basket)//include child basket items
        {
            var validatedBasketItems = new List<BasketItem>();
            foreach (var basketItem in basket.BasketItems)
            {
                var product = await _unitOfWork.ProductRepository.GetOneAsync(basketItem.ProductId);
                if (product == null || product.Quantity == 0) { continue; }

                if (basketItem.Quantity > product.Quantity)
                {
                    basketItem.Quantity = product.Quantity;
                }
                basketItem.Price = product.cutPrice ?? product.Price;

                validatedBasketItems.Add(basketItem);
            }
            basket.BasketItems = validatedBasketItems;

            return basket;
            //wywołać savechanges bo zmieniamy coś z sql i bez sensu znowu to va
        }
        public async Task<CustomerBasket> CheckBasketAsync(CustomerBasket basket)//include child basket items
        {
            var validatedBasketItems = new List<BasketItem>();
            foreach (var basketItem in basket.BasketItems)
            {
                var product = await _unitOfWork.ProductRepository.GetOneAsync(basketItem.ProductId);
                if (product == null || product.Quantity == 0) { throw new BadRequestException("Któraś z wartości jest niepoprawna"); }

                if (basketItem.Quantity > product.Quantity){  throw new BadRequestException("Któraś z wartości jest niepoprawna"); }

                basketItem.Price = product.cutPrice ?? product.Price;

                validatedBasketItems.Add(basketItem);
            }
            basket.BasketItems = validatedBasketItems;

            return basket;
            //wywołać savechanges bo zmieniamy coś z sql i bez sensu znowu to va
        }
    }
}
