using Core.DeliveryMethods.Queries;
using Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryMethodsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DeliveryMethodsController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeliveryMethod>>> GetDeliveryMethods()
        {
            return Ok(await _mediator.Send(new GetDeliveryMethodsQuery()));
        }


    }
}
