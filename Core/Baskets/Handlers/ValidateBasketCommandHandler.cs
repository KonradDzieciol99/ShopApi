using Core.Baskets.Commands;
using Core.Dtos;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Baskets.Handlers
{
    public class ValidateBasketCommandHandler : IRequestHandler<ValidateBasketCommand, CustomerBasketDto>
    {
        private readonly IValidateBasketService _validateBasketService;

        public ValidateBasketCommandHandler(IValidateBasketService validateBasketService)
        {
            this._validateBasketService = validateBasketService;
        }
        public async Task<CustomerBasketDto> Handle(ValidateBasketCommand request, CancellationToken cancellationToken)
        {
            return await _validateBasketService.ValidateBasketDtoAsync(request.Basket);
        }
    }
}
