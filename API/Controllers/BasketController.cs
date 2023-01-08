using AutoMapper;
using Core.Baskets.Commands;
using Core.Baskets.Queries;
using Core.Dtos;
using Core.Entities;
using Core.Extensions;
using Core.Interfaces.IRepositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        public IMediator _mediator { get; }

        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> UpdateBasket(CustomerBasketDto basket)
        {
            return await _mediator.Send(new UpdateBasketCommand(basket, User.GetUserIdFromClaims()));
        }

        [Authorize]
        [HttpPut("difference-resolver")]
        public async Task<ActionResult<CustomerBasketDto>> ResolveDifferences(CustomerBasketDto basket)
        {
            return await _mediator.Send(new ResolveDifferencesCommand(basket, User.GetUserIdFromClaims()));
        }
        
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<CustomerBasketDto>> GetBasketById()
        {
            return await _mediator.Send(new GetBasketByIdQuery(User.GetUserIdFromClaims()));

        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteBasket()
        { 
            await _mediator.Send(new DeleteBasketCommand(User.GetUserIdFromClaims()));
            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("ValidateBasket")]
        public async Task<ActionResult<CustomerBasketDto>> ValidateBasket(CustomerBasketDto basket)
        {
            return await _mediator.Send(new ValidateBasketCommand(basket));
        }
    }
}
