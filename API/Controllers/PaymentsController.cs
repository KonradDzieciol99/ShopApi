using Core.Dtos;
using Core.Entities;
using Core.Payments.Commands;
using Core.Payments.Handlers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PaymentsController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [Authorize]
        [HttpPost("{basketId}")]
        public async Task<CustomerBasketDto> CreateOrUpdatePaymentIntent(int basketId)//może docelowo go zwróimy
        {
            return await _mediator.Send(new CreateOrUpdatePaymentIntentCommand(basketId));
        }
        [AllowAnonymous]
        [HttpPost("webhook")]
        public async Task<ActionResult> ConfirmOrRejectPaymentWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var stripeSignature = Request.Headers["Stripe-Signature"];

            await _mediator.Send(new ConfirmOrRejectPaymentWebhookCommand(json, stripeSignature));

            return Ok();
        }
    }
}

