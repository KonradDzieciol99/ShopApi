using Core.Accounts.Commands;
using Core.Contact.Commands;
using Core.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        public IMediator _mediator { get; }

        public ContactController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<UserDto>> SendContactMessage(ContactMessageDto contactMessageDto)
        {
            await _mediator.Send(new SendContactMessageCommand(contactMessageDto));

            return NoContent();
        }
    }
}
