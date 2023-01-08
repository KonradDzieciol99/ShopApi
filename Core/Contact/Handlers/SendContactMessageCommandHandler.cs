using Core.Contact.Commands;
using Core.Interfaces;
using Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contact.Handlers
{
    internal class SendContactMessageCommandHandler : IRequestHandler<SendContactMessageCommand, Unit>
    {
        private readonly IEmailService _emailService;

        public SendContactMessageCommandHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task<Unit> Handle(SendContactMessageCommand request, CancellationToken cancellationToken)
        {

            var message = new Message(new List<string>() { request.ContactMessageDto.Email }, request.ContactMessageDto.Subject, request.ContactMessageDto.Message);
            await _emailService.SendContactMessage(message);
            return Unit.Value;        
        }
    }
}
