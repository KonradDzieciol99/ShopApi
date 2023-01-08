using Core.Dtos;
using Core.Entities;
using Core.Extensions;
using Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Reflection.Metadata.Ecma335;
using Core.Orders.Commands;
using Core.Orders.Queries;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            return await _mediator.Send(new CreateOrderCommand(User.GetUserEmailFromClaims(), orderDto));
        }
        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<OrderToReturnDto>> GetUserOrders()
        { 
            return await _mediator.Send(new GetUserOrdersQuery(User.GetUserEmailFromClaims()));
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetUserOrderById(int id)
        {
            return await _mediator.Send(new GetUserOrderByIdQuery(User.GetUserEmailFromClaims(),id));
        }
    }
}
